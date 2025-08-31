namespace privado_backend.Models
{
    public class OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO
    {
        public int id { get; set; }     

        public int? id_operaciones { get; set; }

        public int? id_producto_bancario { get; set; }

        public DateTime? fecha { get; set; }

        public int? estado { get; set; }

        public int? id_usuario { get; set; }
    }
}
