using System.ComponentModel.DataAnnotations.Schema;

[Table("cuenta_terceros")]
public class cuentaterceros
{
    public int id { get; set; }
    public int? id_usuario_prim { get; set; }
    public int? id_producto_bancario_usuario { get; set; }
    public string? alias { get; set; }
    public int? estado { get; set; }
    public DateTime? fecha { get; set; }
    public int? id_usuario { get; set; }
}
