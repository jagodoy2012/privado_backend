namespace privado_backend.Models.SistemasPrivado
{
    public class TRANSACCIONES
    {
        public int id { get; set; }
        public int? id_producto_bancario_usuario_envia { get; set; }
        public int? id_producto_bancario_usuario_recibe { get; set; }
        public int? id_operaciones { get; set; }
        public decimal? monto { get; set; }
        public int? id_moneda_tipo { get; set; }
        public decimal? cambio { get; set; }
        public string? nota { get; set; }
        public DateTime? fecha_realizado { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha { get; set; }
        public int? id_usuario { get; set; }
    }
}
