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
        public ConvocatoriaController(IConvocatoriaService convocatoriaService) {
            this._convocatoriaService = convocatoriaService;
        }


        [HttpGet("listar")]
        public IActionResult GetListConvocatoria()
        {
            var convocatorias = this._convocatoriaService.GetAllConvocatorias();
            return Ok(convocatorias);
        }

        [HttpGet("obtenerConvocatoria")]
        public IActionResult GetConvocatoriaIsActive(int idConvocatoria)
        {
            var convocatoria = this._convocatoriaService.EstadoConvocatoria(idConvocatoria);

           return Ok(convocatoria);
        }
    }
}
