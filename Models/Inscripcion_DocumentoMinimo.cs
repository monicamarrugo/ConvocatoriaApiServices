using ConvocatoriaServices.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("inscripcion_documentomin")]
    public class Inscripcion_DocumentoMinimo
    {
        [Key]
        public int id { get; set; }
        public string codigo_inscripcion { get; set; }

        public Inscripcion_Convocatoria Inscripcion_Convocatoria { get; set; }
        public string codigo_documento { get; set; }

        public Tipo_DocumentoMinimo Tipo_DocumentoMinimo { get; set; }

        public bool cumplido { get; set; }
        public bool no_cumplido { get; set; }
        

        public DateTime fecha_cumplido { get; set; }

        public string? observacion { get; set; }
    }
}
