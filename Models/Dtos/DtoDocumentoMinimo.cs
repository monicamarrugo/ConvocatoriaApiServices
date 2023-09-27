namespace ConvocatoriaApiServices.Models.Dtos
{
    public class DtoDocumentoMinimo
    {
        public string codigoInscripcion { get; set; }

        public string codigoTipoDocumento { get; set; }

        public string nombreTipoDocumento { get; set; }


        public bool cumplido { get; set; }

        public bool no_cumplido { get; set; }

        public DateTime fecha_cumplido { get; set; }

        public string observacion { get; set; }
    }
}
