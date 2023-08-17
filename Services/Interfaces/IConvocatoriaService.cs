using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IConvocatoriaService
    {
        public List<Convocatoria> GetAllConvocatorias();
        public ConvocatoriaDto EstadoConvocatoria(int IdConvocatoria);
    }
}
