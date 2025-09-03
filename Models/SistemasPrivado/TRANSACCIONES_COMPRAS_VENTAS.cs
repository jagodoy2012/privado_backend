namespace privado_backend.Models.SistemasPrivado
{
    public class TRANSACCIONES_COMPRAS_VENTAS
    {
        public int id { get; set; }
        public int? id_usuario { get; set; }
        public int? id_cliente_proveedor { get; set; }
        public int? id_transaccion_tipo { get; set; }
        public int? id_transaccion_movimiento { get; set; }
        public decimal? total { get; set; }
        public DateTime? fecha { get; set; }
        public int? id_tienda { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha_actualiza { get; set; }
        public int? id_usuario_actualiza { get; set; }
    }
}
