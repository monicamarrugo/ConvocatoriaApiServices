using ConvocatoriaServices.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("detalle_evaluacion")]
    public class Detalle_Evaluacion
    {
        [Key]
        public int? id { get; set; }
        public int? id_evaluacionCompetencia { get; set; }
        public Evaluacion_Competencia Evaluacion_Competencia { get; set; }
        public int id_competencia { get; set; }

        public CompentenciasAcademicas CompentenciasAcademicas { get; set; }
        public int calificacionObtenida { get; set; }
        public int calificacionPonderada { get; set; }

        
    }
}
