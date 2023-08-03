using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaServices.Models
{
    [Table("tipo_documento")]
    public class Tipo_Documento
    {
        
        public int id_tipo_documento { get; set; }

        [Key]
        public string codigo { get; set; }
        public string descripcion { get; set; }

        public Documento Documento { get; set; }

    }
}
