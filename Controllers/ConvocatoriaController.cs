using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services;
using ConvocatoriaApiServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConvocatoriaApiServices.Controllers
{
    [Route("/api/[controller]")]
    public class ConvocatoriaController : ControllerBase
    {
        public IConvocatoriaService _convocatoriaService;
        public IComisionConvocatoriaService _comisionConvocatoriaService;
        public ConvocatoriaController(IConvocatoriaService convocatoriaService, IComisionConvocatoriaService comisionConvocatoriaService = null)
        {
            this._convocatoriaService = convocatoriaService;
            _comisionConvocatoriaService = comisionConvocatoriaService;
        }


        [HttpGet("listar")]
        public IActionResult GetListConvocatoria()
        {
            var convocatorias = this._convocatoriaService.GetAllConvocatorias();
            return Ok(convocatorias);
        }

        [HttpGet("listarComision")]
        public IActionResult GetListComision()
        {
            var convocatorias = this._comisionConvocatoriaService.GetAllComisionConvocatoria();
            return Ok(convocatorias);
        }

        [HttpGet("obtenerConvocatoria")]
        public IActionResult GetConvocatoriaIsActive(int idConvocatoria)
        {
            var convocatoria = this._convocatoriaService.EstadoConvocatoria(idConvocatoria);

           return Ok(convocatoria);
        }

        [HttpPost("autenticarComision")]
        public IActionResult AutenticarComision([FromBody] UsuarioDto datosInscripcion)
        {
            var comision = this._comisionConvocatoriaService.ExistComisionConvocatoria(datosInscripcion);
            return Ok(comision);
        }

        [HttpGet("buscarEvaluacionHojaVida")]
        public IActionResult AutenticarComision(String codigoInscripcion)
        {
            var comision = this._convocatoriaService.FindEvaluacionHojaVida(codigoInscripcion);
            return Ok(comision);
        }


        [HttpPost("consolidadoHojaVida")]
        public IActionResult GetEvaluacionesHojaVida([FromBody]  List<String> listaPerfiles)
        {
            var comision = this._convocatoriaService.GetEvaluacionesHojaVida(listaPerfiles);
            return Ok(comision);
        }
    }
}
