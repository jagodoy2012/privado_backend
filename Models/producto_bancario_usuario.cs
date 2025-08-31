public class producto_bancario_usuario
{
    public int id { get; set; }
    public int? id_usuario_producto { get; set; }
    public decimal? monto { get; set; }
    public decimal? disponible { get; set; }
    public int? id_producto_bancario_asignado { get; set; }
    public int? id_moneda_tipo { get; set; }
    public int? estado { get; set; }
    public DateTime? fecha { get; set; }
    public int? id_usuario { get; set; }
    public DateOnly? fecha_ultimo_corte { get; set; }
}
