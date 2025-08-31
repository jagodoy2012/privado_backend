namespace privado_backend.Models
{
    public class usuario_tipo_menu
    {
        public int id { get; set; }
        public int id_usuario_tipo { get; set; }
        public int id_menu { get; set; }
        public bool? can_view { get; set; }
        public bool? include_ancestors { get; set; }
        public bool? include_descendants { get; set; }
        public int? estado { get; set; }
        public DateTime?fecha { get; set; }
        public int? id_usuario { get; set; }
    }
}
