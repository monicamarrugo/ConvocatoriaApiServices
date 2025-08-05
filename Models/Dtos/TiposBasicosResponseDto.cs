namespace ConvocatoriaApiServices.Models.Dtos
{
    public class TiposBasicosResponseDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }

        public List<SubtipoDocumentoDto> subtipos { get; set; }
    }

    public class SubtipoDocumentoDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }
    }
}
