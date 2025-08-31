// Program.cs — versión mínima y estable (limpia)

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using privado_backend.Data;
using privado_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// ========= DB =========
builder.Services.AddDbContext<privado_backendContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("privado_backendContext")!));

// ========= CORS =========
builder.Services.AddCors(o =>
{
    o.AddPolicy("front", p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddControllers();

// ========= JWT =========
var jwt = builder.Configuration.GetSection("Jwt");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

string? lastTokenSeen = null;
string? lastTokenHex = null;

builder.Services
  .AddAuthentication(o =>
  {
      o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(o =>
  {
      o.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = signingKey,

          ValidateIssuer = true,
          ValidIssuer = "https://privado-cvbggdesdsafgxfu.canadacentral-01.azurewebsites.net",   // <— que coincida con el que firmas

          ValidateAudience = true,
          ValidAudience = "https://privado-cvbggdesdsafgxfu.canadacentral-01.azurewebsites.net",  // <— usa http o https según tu front

          ValidateLifetime = true,                 // en prod: true
          ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
          ClockSkew = TimeSpan.FromMinutes(1)
      };

      o.Events = new JwtBearerEvents
      {
          OnMessageReceived = ctx =>
          {
              // 1) Query ?auth=... o Header Authorization
              var raw = ctx.Request.Query["auth"].ToString();
              if (string.IsNullOrWhiteSpace(raw))
                  raw = ctx.Request.Headers["Authorization"].ToString();

              // 2) Si viene con "Bearer ", quita el prefijo
              if (!string.IsNullOrWhiteSpace(raw) &&
                  raw.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                  raw = raw[7..];

              // 3) Sanea (CR/LF, comillas, espacios invisibles comunes)
              raw = (raw ?? string.Empty)
                  .Replace("\r", "").Replace("\n", "")
                  .Trim('"', '\'', ' ', '\u00A0', '\u200B', '\u200C', '\u200D', '\uFEFF');

              // 4) Guarda trazas útiles para depurar
              lastTokenHex = string.Join(" ", raw.Select(ch => ((int)ch).ToString("X2")));
              lastTokenSeen = raw;

              // 5) Valida estructura básica (3 partes)
              var parts = raw.Split('.');
              if (parts.Length != 3 || parts.Any(p => string.IsNullOrEmpty(p)))
              {
                  ctx.NoResult();
                  return Task.CompletedTask;
              }

              // 6) Asigna el token a la autenticación
              ctx.Token = raw;
              return Task.CompletedTask;
          },

          OnAuthenticationFailed = ctx =>
          {
              if (ctx.Exception is SecurityTokenExpiredException expEx)
              {
                  ctx.Response.Headers["x-auth-error"] = "token_expired";
                  ctx.Response.Headers["x-auth-expired"] = expEx.Expires.ToUniversalTime().ToString("o");
              }
              else
              {
                  var name = ctx.Exception.GetType().Name;
                  var msg = new string((ctx.Exception.Message ?? "")
                      .Replace('\r', ' ')
                      .Replace('\n', ' ')
                      .Where(ch => !char.IsControl(ch)).ToArray());

                  ctx.Response.Headers["x-auth-error"] = name;
                  ctx.Response.Headers["x-auth-error-msg"] = msg;
                  ctx.Response.Headers["x-auth-token"] = lastTokenSeen ?? "<null>";
                  ctx.Response.Headers["x-auth-dots"] = string.IsNullOrEmpty(lastTokenSeen) ? "0"
                        : lastTokenSeen.Count(c => c == '.').ToString();
                  ctx.Response.Headers["x-auth-len"] = (lastTokenSeen ?? "").Length.ToString();
                  ctx.Response.Headers["x-auth-hex"] = (lastTokenHex ?? "<null>");
              }
           

              return Task.CompletedTask;
          }
      };

      // (Opcional) Guarda el token en HttpContext.User bootstrap
      o.SaveToken = true;
  });

builder.Services.AddAuthorization();

// ========= Swagger =========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
    var scheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    c.AddSecurityDefinition("Bearer", scheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [scheme] = Array.Empty<string>()
    });
});

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("front");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
