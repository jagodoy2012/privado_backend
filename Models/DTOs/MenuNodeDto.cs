namespace privado_backend.Models.DTOs
{
    public class MenuNodeDto
    {
        public int id { get; set; }
        public string label { get; set; } = "";
        public string? path { get; set; }
        public int sort_order { get; set; }
        public List<MenuNodeDto> children { get; set; } = new();
    }
}
