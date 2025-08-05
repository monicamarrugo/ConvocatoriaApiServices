using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Models.Dtos
{
    public class DocumentoResponseDto
    {
        public int idDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public string? subtipoDocumento { get; set; }
        public string? nombreSubtipoDocumento { get; set; }
        public string descTipodocumento { get; set; }
        public string contenido { get; set; }


    }
}
