namespace ConvocatoriaApiServices.Models.Dtos
{
    public class EvaluacionDto
    {
        public EvaluacionCompetencia cabeceraEvaluacion { get; set; }

        public List<DetalleEvaluacion> detalleEvaluacion { get; set; }

        public List<Competencias> competencias { get; set; }
        


    }

    public class EvaluacionCompetencia
    {
        public int? idEvaluacion { get; set; }
        public string codigoInscripicion { get; set; }

        public string identificacionEvaluador { get; set; }

        public string nombreEvaluador { get; set; }

        public int totalEvaluacion { get; set; }

        public string observacion { get; set; }
    }

    public class DetalleEvaluacion
    {
        public int? idDetalle { get; set; }
        public int? idEvaluacion { get; set; }
        public int idCompetencia { get; set; }

        public int calificacionObtenida { get; set; }
        public int calificacionPonderada { get; set; }
    }

    public class Competencias
    {
        public int idCompetencia { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public int inferior { get; set; }
        public int medio { get; set; }
        public int superior { get; set; }
        public int peso { get; set; }
        public int calificacionObtenida { get; set; }
        public int calificacionPonderada { get; set; }
        public double? calificacionPromediada { get; set; }
    }
}
