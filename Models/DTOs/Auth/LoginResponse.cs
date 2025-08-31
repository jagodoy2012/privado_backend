namespace privado_backend.Models.DTOs.Auth
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public int? IdUsuarioTipo { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expira { get; set; }
    }
}
