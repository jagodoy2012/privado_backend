namespace privado_backend.Models.SistemasPrivado
{
    public class INVENTARIO
    {
        public int id { get; set; }
        public int? id_producto { get; set; }
        public int? cantidad { get; set; }
        public int? id_tienda { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha_actualiza { get; set; }
        public int? id_usuario_actualiza { get; set; }
    }
}
