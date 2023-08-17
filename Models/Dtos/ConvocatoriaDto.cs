namespace ConvocatoriaApiServices.Models.Dtos
{
    public class ConvocatoriaDto
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
    }
}
