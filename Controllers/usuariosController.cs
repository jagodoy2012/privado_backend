using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using privado_backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using privado_backend.Models.DTOs.Auth;

namespace privado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly privado_backendContext _context;
        private readonly IConfiguration _config;

        public usuariosController(privado_backendContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ====================== Utilidades ======================
        private static byte[] HashFromPlain(string plain)
        {
            if (string.IsNullOrWhiteSpace(plain))
                throw new ArgumentException("Texto vacío para hash.");

            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(plain.Trim()));
        }

        /// <summary>
        /// Genera un JWT con exp, iss y aud (si están configurados) y retorna (token, expiraUtc).
        /// </summary>
        private (string token, DateTime expiraUtc) GenerarJwt(usuario u)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var days = double.TryParse(jwt["ExpiresDays"], out var d) ? d : 1d;
            var expira = DateTime.UtcNow.AddDays(days);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, u.id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, u.correo ?? ""),
        new Claim("nombres", u.nombres ?? ""),
        new Claim("apellidos", u.apellidos ?? ""),
        new Claim("id_usuario_tipo", u.id_usuario_tipo?.ToString() ?? "")
    };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = expira,
                Issuer = jwt["Issuer"],
                Audience = jwt["Audience"],
                SigningCredentials = creds
            };

            var handler = new JwtSecurityTokenHandler();
            var secToken = handler.CreateToken(descriptor);
            var tokenStr = handler.WriteToken(secToken);   // <-- devuelve el JWS con 2 puntos

            return (tokenStr, expira);
        }


    
        // POST: api/usuarios/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Correo) || string.IsNullOrWhiteSpace(req.Contrasena))
                return BadRequest("Correo y contraseña son obligatorios.");

            var u = await _context.usuario.FirstOrDefaultAsync(x => x.correo == req.Correo);
            if (u is null)
                return Unauthorized(new { ok = false, error = 901, message = "Credenciales inválidas." });

            var incoming = HashFromPlain(req.Contrasena);
            if (!incoming.SequenceEqual(u.contrasena ?? Array.Empty<byte>()))
            {
                // Tolerancia: si contraseña enviada coincide con 'nombres', corrige hash
                if (!string.IsNullOrWhiteSpace(u.nombres) && req.Contrasena.Trim() == u.nombres.Trim())
                {
                    u.contrasena = incoming;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Unauthorized(new { ok = false, error = 901, message = "Credenciales inválidas." });
                }
            }

            var (token, expiraUtc) = GenerarJwt(u);

            return Ok(new LoginResponse
            {
                Id = u.id,
                Nombres = u.nombres ?? "",
                Apellidos = u.apellidos ?? "",
                IdUsuarioTipo = u.id_usuario_tipo,
                Token = token,
                Expira = expiraUtc
            });
        }

        // GET: api/usuarios/verify (requiere JWT)

        [HttpGet("verify")]
        [Authorize]
        public IActionResult Verify()
        {
            var expClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp || c.Type == "exp")?.Value;
            DateTime? expUtc = null;
            if (long.TryParse(expClaim, out var expUnix))
                expUtc = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

            return Ok(new
            {
                ok = true,
                sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub),
                email = User.FindFirstValue(JwtRegisteredClaimNames.Email) ?? User.FindFirst("email")?.Value,
                nombres = User.FindFirst("nombres")?.Value,
                apellidos = User.FindFirst("apellidos")?.Value,
                id_usuario_tipo = User.FindFirst("id_usuario_tipo")?.Value,
                expira = expUtc
            });
        }

        // ======================== CRUD ==========================
        // GET: api/usuarios
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<usuario>>> Getusuario()
            => await _context.usuario.ToListAsync();

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<usuario>> Getusuario(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);
            return usuario is null ? NotFound() : usuario;
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putusuario(int id, usuario dto)
        {
            if (id != dto.id) return BadRequest("El id del path no coincide con el del cuerpo.");

            var entity = await _context.usuario.FindAsync(id);
            if (entity is null) return NotFound();

            entity.nombres = dto.nombres;
            entity.apellidos = dto.apellidos;
            entity.fecha_nacimiento = dto.fecha_nacimiento;
            entity.correo = dto.correo;
            entity.telefono = dto.telefono;
            entity.id_usuario_tipo = dto.id_usuario_tipo;
            entity.estado = dto.estado;
            entity.fecha = dto.fecha;

            // Regla: contraseña = SHA-256(nombres)
            entity.contrasena = HashFromPlain(entity.nombres ?? "");

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/usuarios
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<usuario>> Postusuario(usuario usuario)
        {
            usuario.contrasena = HashFromPlain(usuario.nombres ?? "");
            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getusuario), new { id = usuario.id }, usuario);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteusuario(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);
            if (usuario is null) return NotFound();

            _context.usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpPost("jwt-validate")]
        [AllowAnonymous]
        public IActionResult Validate([FromBody] string token, [FromServices] IConfiguration cfg)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!));
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var result = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
          ValidateIssuer = true,
          ValidIssuer = "https://privado-cvbggdesdsafgxfu.canadacentral-01.azurewebsites.net",
          ValidateAudience = true,
          ValidAudience = "https://privado-cvbggdesdsafgxfu.canadacentral-01.azurewebsites.net",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                }, out var validatedToken);

                return Ok(new
                {
                    valid = true,
                    claims = result.Claims.Select(c => new { c.Type, c.Value })
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { valid = false, error = ex.Message });
            }
        }
       
     

        

        private bool usuarioExists(int id) => _context.usuario.Any(e => e.id == id);
    }
}
