using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Models.Queries
{
    public class CheckListDocumentos
    {

        public string identificacion { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }

        public string codigoInscripcion { get; set; }

        public List<Documento> listaDocumentos { get; set; }
    }
}
