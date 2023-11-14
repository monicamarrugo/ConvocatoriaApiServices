using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IInscripcionService
    {
        public RtaTransaccion SaveInscripcion(InscripcionDto datosInscripcion);
        public Inscripcion_Convocatoria FindInscripcion(String codigoInscripcion);

        public Boolean ExistsInscripcion(string codigoInscripcion);

        public string CodeGenerate(string cadena);

        public List<Inscripcion_Convocatoria> GetInscripcionByPerfil(String codigoPerfil);

        public List<DtoDocumentoMinimo> GetDocumentosMinimos(string codigoInscripcion);

        public RtaTransaccion SaveRequerimientoMin(List<DtoDocumentoMinimo> documentoMinimo);

        public bool GetEvaluado(string codigoInscripcion);
        public List<EvaluadoDto> GetInscripcionDocumentoMinimoByPerfil(String codigoPerfil);

        public RtaTransaccion SaveVerificacionHV(Verificacion_HV datosHV);

        public List<InscripcionDto> GetAdmitidosHvByPerfil(String codigoPerfil);
    }
}
