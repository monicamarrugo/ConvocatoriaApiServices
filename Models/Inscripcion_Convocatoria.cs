using ConvocatoriaApiServices.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaServices.Models
{
    [Table("inscripcion_convocatoria")]
    public class Inscripcion_Convocatoria
    {
        [Key]
        public string codigo { get; set; }

        public int codigo_convocatoria { get; set; }
        public Convocatoria Convocatoria { get; set; }
        public string codigo_perfil { get; set; }
        public Perfil Perfil { get; set; }
        public string identificacion_participante { get; set; }
        public Participante Participante { get; set; }

        public Documento Documento { get; set; }

        public Inscripcion_DocumentoMinimo Inscripcion_DocumentoMinimo { get; set; }
    }
}
