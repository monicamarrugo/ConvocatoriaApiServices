using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaServices.Models
{
    [Table("tipo_identificacion")]
    public class Tipo_Identificacion
    {
        [Key]
        public string codigo { get; set; }
        public string descripcion { get; set; }

        public Participante Participante { get; set; }
    }
}
