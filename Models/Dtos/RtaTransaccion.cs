using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ConvocatoriaApiServices.Models.Dtos
{
    public class RtaTransaccion
    {
        public string error { get; set; }
        public string errorDetail { get; set; } = string.Empty;

        public string mensaje { get; set; }

        public string respuesta { get; set; } 
    }
}
