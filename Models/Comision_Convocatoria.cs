using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConvocatoriaApiServices.Models
{
    [Table("comision_convocatoria")]
    public class Comision_Convocatoria
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }  

        public string facultad { get; set; }

        public string usuario { get; set;}

        public string password { get; set;}
    }
}
