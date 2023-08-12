namespace ConvocatoriaApiServices.Models.Dtos
{
    public class DocumentoUploadedDto
    {
        public string tipoDocumento { get; set; }
        public string codigoInscripcion { get; set; }
        public string ruta { get; set; }

        public IFormFile archivo { get; set; }
    }
}
