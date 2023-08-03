using ConvocatoriaApiServices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvocatoriaApiServices.Controllers
{
    [Route("/api/[controller]")]
    public class ListasTiposBasicosController : ControllerBase
    {
        public IListasTiposBasicosService _tiposService;
        public ListasTiposBasicosController(IListasTiposBasicosService tiposService)
        {
            this._tiposService = tiposService;
        }


        [HttpGet("GetListaPerfiles")]
        public IActionResult GetListaPerfiles()
        {
            var perfiles = this._tiposService.GetAllPerfiles();
            return Ok(perfiles);
        }

        [HttpGet("GetListaTiposIdentificacion")]
        public IActionResult GetListaTiposIdentificacion()
        {
            var tiposIdentificacion = this._tiposService.GetAllTiposIdentificacion();
            return Ok(tiposIdentificacion);
        }
    }
}
