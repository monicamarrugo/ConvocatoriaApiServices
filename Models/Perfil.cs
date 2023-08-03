using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaServices.Models
{
    [Table("perfil")]
    public class Perfil
    {
        [Key]
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public Inscripcion_Convocatoria Inscripcion_Convocatoria { get; set; }
    }
}
