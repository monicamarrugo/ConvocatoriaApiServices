namespace ConvocatoriaApiServices.Models.Dtos
{
    public class EvaluadoDto
    {
        public string codigoInscripcion { get; set; }

        public bool evaluado { get; set; }
        public bool DID { get; set; }
        public bool TPU { get; set; }
        public bool TMD { get; set; }
        public bool CED { get; set; }
        public bool CEP { get; set; }
        public bool CCI { get; set; }
        public bool ADM { get; set; }

        public string observacion { get; set; }
    }
}
