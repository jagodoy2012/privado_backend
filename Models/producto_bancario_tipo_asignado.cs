using System.ComponentModel.DataAnnotations.Schema;

[Table("PRODUCTO_BANCARIO_TIPO_ASIGNADO")]
public class producto_bancario_tipo_asignado
{
    public int Id { get; set; }

    [Column("id_producto_bancario")]
    public int? IdProductoBancario { get; set; }

    [Column("id_producto_bancario_tipo")]
    public int? IdProductoBancarioTipo { get; set; }

    public int? id_categoria { get; set; }

    [Column("estado")]
    public int? Estado { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("fecha")]
    public DateTime? Fecha { get; set; }


}

