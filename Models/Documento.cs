using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaServices.Models
{
    [Table("documento")]
    public class Documento
    {
        [Key]
        public int id_documento { get; set; }

        public string tipo_documento { get; set; }
        public Tipo_Documento TipoDocumento { get; set; }
        public string contenido { get; set; }
        public string codigo_inscripcion { get; set; }
        public Inscripcion_Convocatoria Inscripcion_Convocatoria { get; set; }

    }
}
