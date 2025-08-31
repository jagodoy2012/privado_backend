using System.ComponentModel.DataAnnotations.Schema;

[Table("moneda_tipo_cambio_api")]
public class monedatipocambioapi
{
    public int id { get; set; }
    public int? id_moneda_tipo { get; set; }
    public string? url { get; set; }
    public int? estado { get; set; }
    public DateTime? fecha { get; set; }
    public int? id_usuario { get; set; }
}
