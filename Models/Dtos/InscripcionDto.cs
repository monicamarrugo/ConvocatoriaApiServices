using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Models.Dtos
{
    public class InscripcionDto
    {
        public Int32 codigoConvocatoria { get; set;}
        public string codigoInscripcion { get; set; }
        public string codigoPerfil { get; set; }
        public String tipoIdentificacion { get; set; }
        public String identificacion { get; set; }
        public String nombres { get; set; }
        public String apellidos { get; set; }
        public String email { get; set; }
        public String telefono { get; set; }
    }
}
