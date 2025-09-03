namespace privado_backend.Models.SistemasPrivado
{
    public class PRODUCTO
    {
        public int id { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public decimal? precio { get; set; }
        public int? id_producto_tipo { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha_actualiza { get; set; }
        public int? id_usuario_actualiza { get; set; }

    }
}
