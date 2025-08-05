using ConvocatoriaServices.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConvocatoriaApiServices.Models
{
    [Table("subtipo_documento")]
    public class SubTipo_Documento
    {
        public int id_subtipo_documento { get; set; }

        [Key]
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }

        public string codigo_tipo_documento { get; set; }
        public Tipo_Documento TipoDocumento { get; set; }

    }
}
