using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IDocumentoService
    {
        public RtaTransaccion SaveDocumento(DocumentoDto datosDocumento);
        public List<Documento> ListDocumento(string codigoInscripcion);
    }
}
