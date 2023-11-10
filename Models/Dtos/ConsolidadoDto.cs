namespace ConvocatoriaApiServices.Models.Dtos
{
    public class ConsolidadoDto
    {
        public string codigoperfil  {get; set; }
        public string  codigoinscripcion { get; set; }
        public int totalFormacion { get; set; }
        public int totalFormacionIn { get; set; }
        public int totalProduccion { get; set; }
        public int totalExperiencia { get; set; }
        public int totalEvaluacion { get; set; }
        public bool admitido { get; set; }
    }
}
