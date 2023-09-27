using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("tipo_documentomin")]
    public class Tipo_DocumentoMinimo
    {
        [Key]
        public string codigo_documento { get; set; }
        public string nombre_documento { get;set; }

        public Inscripcion_DocumentoMinimo Inscripcion_DocumentoMinimo { get; set; }
    }
}
