namespace privado_backend.Models.DTOs
{
    public class RemesaDto
    {
        public int id { get; set; }
        public int? id_producto_bancario_usuario { get; set; }
        public int? id_usuario { get; set; }
        public int? id_moneda_tipo { get; set; }

        public string? nombre_receptor { get; set; }
        public string? nombre_remitente { get; set; }

        public decimal? monto { get; set; }
        public string? no_pago { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha { get; set; }
        public string tipo { get; set; } = ""; // "envia" | "recibe"
    }
}
