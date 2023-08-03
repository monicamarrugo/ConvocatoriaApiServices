using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IInscripcionService
    {
        public RtaTransaccion SaveInscripcion(InscripcionDto datosInscripcion);
        public Inscripcion_Convocatoria FindInscripcion(String codigoInscripcion);

        public string CodeGenerate(string cadena);
    }
}
