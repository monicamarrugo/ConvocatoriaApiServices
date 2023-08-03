using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaServices.Models
{
    [Table("participante")]
    public class Participante
    {
       
        public Int32 id_participante { get; set; }
        public string tipo_identificacion { get; set; }
        public Tipo_Identificacion TipoIdentificacion { get; set; }

        [Key, Required]
        public string identificacion { get; set; }
        public string nombres { get; set; } 
        public string apellidos { get; set; } 
        public string email { get; set; } 
        public string telefono { get; set; } 

        public Inscripcion_Convocatoria Inscripcion_Convocatoria { get; set; }

    }
}
