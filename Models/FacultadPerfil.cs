using ConvocatoriaServices.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConvocatoriaApiServices.Models
{
    [Table("facultadperfil")]
    public class FacultadPerfil
    {
        [Key]
        public int id { get; set; }
        public string facultad { get; set; }
        public string programa { get; set; }
        public string perfil { get; set; }
        public int id_comision { get; set; }

    }
}
