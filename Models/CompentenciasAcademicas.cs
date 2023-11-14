using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("compentencias_academicas")]
    public class CompentenciasAcademicas
    {
        [Key]
        public int id { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; } 
        public int inferior { get; set; } 
        public int medio { get; set; } 
        public int superior { get; set; }
        public int peso { get; set; }

        [JsonIgnore]
        public ICollection<Detalle_Evaluacion> Detalle_Evaluacion { get; set; }

    }
}
