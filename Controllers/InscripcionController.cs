using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConvocatoriaApiServices.Controllers
{
    [Route("/api/[controller]")]
    public class InscripcionController : ControllerBase
    {
        private IInscripcionService _inscripcionService;
        public InscripcionController(IInscripcionService inscripcionService) 
        {
            this._inscripcionService = inscripcionService;
        }
        [HttpPost("inscribir")]
        public IActionResult SaveInscripcion([FromBody] InscripcionDto datosInscripcion)
        {
            var respuestaInscripcion = this._inscripcionService.SaveInscripcion(datosInscripcion);
            return Ok(respuestaInscripcion);            
        }
        [HttpGet("consultar")]
        public IActionResult GetInscripcion(string codigoInscripcion)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                var inscripcion = this._inscripcionService.FindInscripcion(codigoInscripcion);
                rta.error = "NO";
                rta.respuesta = JsonConvert.SerializeObject(inscripcion);

                return Ok(rta);
            }catch(Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return Ok(rta);
            }
        }
    }
}
