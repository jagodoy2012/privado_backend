namespace privado_backend.Models
{
    // Models/menu.cs
    public class menu
    {
        public int id { get; set; }
        public int? id_padre { get; set; }
        public string label { get; set; } = "";
        public string? path { get; set; }
        public int sort_order { get; set; } = 10;
        public bool is_active { get; set; } = true;

        // navegación (opcional)

    }

 

}
