using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IConvocatoriaService
    {
        public List<Convocatoria> GetAllConvocatorias();
        public ConvocatoriaDto EstadoConvocatoria(int IdConvocatoria);

        public Verificacion_HV FindEvaluacionHojaVida(string codigoInscripcion);

        public List<ConsolidadoDto> GetEvaluacionesHojaVida(List<string> perfiles);
        public EvaluacionDto GetCompentenciasAcademicas();

        public RtaTransaccion SaveEvalucionCompetencia(EvaluacionDto dtoEvaluacion);

        public EvaluacionDto GetCompentenciasPonderadas(string codigoInscripcion);

        public RtaTransaccion SavePromedioCompetencia(ConsolidadoCompetenciaDto dtoConsolidado);

        public List<EvaluacionDto> GetEvalCompentenciasInscripcion(string codigoInscripcion);

        public List<ConsolidadoCompetenciaDto> GetCompentenciasConsolidado(List<string> perfiles);
    }
}
