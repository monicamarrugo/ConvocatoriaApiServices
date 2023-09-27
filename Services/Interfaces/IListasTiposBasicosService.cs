using ConvocatoriaApiServices.Models;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IListasTiposBasicosService
    {
        public List<Perfil> GetAllPerfiles();
        public List<Tipo_Identificacion> GetAllTiposIdentificacion();
        public List<Tipo_Documento> GetAllTiposDocumento();

        public List<Tipo_DocumentoMinimo> GetAllTiposDocumentoMinimo();
        public List<FacultadPerfil> GetAllFacultadPerfil(int idComision);
        
    }
}
