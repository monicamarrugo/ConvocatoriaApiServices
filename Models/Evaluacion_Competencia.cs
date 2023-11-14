using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("evaluacion_competencia")]
    public class Evaluacion_Competencia
    {
        [Key]
        public int?  id { get; set; }
        public string codigo_inscripcion { get; set; }
        public string identificacion_evaluador { get; set; }
        public string nombre_evaluador { get; set; }
        public int  total { get; set; }
        public string observacion { get; set; }
        public double? total_ponderado { get; set; }
        public bool elegible { get; set; }

        [JsonIgnore]
        public ICollection<Detalle_Evaluacion> Detalle_Evaluacion { get; set; }
    }
}
