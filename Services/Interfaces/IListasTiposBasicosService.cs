using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IListasTiposBasicosService
    {
        public List<Perfil> GetAllPerfiles();
        public List<Tipo_Identificacion> GetAllTiposIdentificacion();
    }
}
