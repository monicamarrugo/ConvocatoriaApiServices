using ConvocatoriaApiServices.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConvocatoriaServices.Models
{
    [Table("tipo_documento")]
    public class Tipo_Documento
    {
        
        public int id_tipo_documento { get; set; }

        [Key]
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }

        [JsonIgnore]
        public ICollection<Documento> Documentos { get; set; }

        // Relación uno a muchos
        [JsonIgnore]
        public ICollection<SubTipo_Documento> Subtipos { get; set; }

    }
}
