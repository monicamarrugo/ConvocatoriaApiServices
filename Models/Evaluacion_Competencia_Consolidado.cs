using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("evaluacion_competencia_consolidado")]
    public class Evaluacion_Competencia_Consolidado
    {
        [Key]
        public int id { get; set; }
        public string codigo_inscripcion { get; set; }
        public double? total_ponderado { get; set; }
        public bool admitido { get; set; }

        public string codigo_perfil { get; set; }
    }
}
