namespace privado_backend.Models.SistemasPrivado
{
    public class TRANSACCION_COMPRAS_VENTAS_DETALLE
    {
        public int id { get; set; }
        public int? id_transsacciones_compras_ventas { get; set; }
        public int? id_producto { get; set; }
        public decimal? cantidad { get; set; }
        public decimal? precio { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha_actualiza { get; set; }
        public int? id_usuario_actualiza { get; set; }
    }
}
