using System.ComponentModel.DataAnnotations.Schema;

[Table("cuenta_tipo")]
public class cuentatipo
{
    public int id { get; set; }
    public string? titulo { get; set; }
    public string? descripcion { get; set; }
    public int? estado { get; set; }
    public DateTime? fecha { get; set; }
    public int? id_usuario { get; set; }
}
