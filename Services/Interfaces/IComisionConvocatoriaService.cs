using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IComisionConvocatoriaService
    {
        public List<Comision_Convocatoria> GetAllComisionConvocatoria();
        public ComisionDto ExistComisionConvocatoria(UsuarioDto usuario);
    }
}
