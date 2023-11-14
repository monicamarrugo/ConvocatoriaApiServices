namespace ConvocatoriaApiServices.Models.Dtos
{
    public class ConsolidadoCompetenciaDto
    {
        public string codigo_inscripcion { get; set; }

        public double? total_ponderado { get; set; }

        public bool elegible { get; set; }
    }
}
