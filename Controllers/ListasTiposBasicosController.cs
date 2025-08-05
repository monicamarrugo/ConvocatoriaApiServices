using ConvocatoriaApiServices.Models.Dtos;
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
        [HttpGet("GetListaTiposDocMinimo")]
        public IActionResult GetListaTiposDocumentosMinimos()
        {
            var tiposIdentificacion = this._tiposService.GetAllTiposDocumentoMinimo();
            return Ok(tiposIdentificacion);
        }

        [HttpGet("GetListaFacultadPerfil")]
        public IActionResult GetListaFacultadPerfil(int idComision)
        {
            var tiposIdentificacion = this._tiposService.GetAllFacultadPerfil(idComision);
            return Ok(tiposIdentificacion);
        }

        [HttpGet("GetListaTiposDocumentos")]
        public IActionResult GetListaTiposDocumentos()
        {
            List<TiposBasicosResponseDto> tiposDocumento = new List<TiposBasicosResponseDto>();
            var tiposDocumentoResponse = this._tiposService.GetAllTiposDocumento();
            if (tiposDocumentoResponse != null && tiposDocumentoResponse.Count > 0) {

                tiposDocumentoResponse.ForEach(t =>
                {
                    tiposDocumento.Add(
                        new TiposBasicosResponseDto
                        {
                            codigo = t.codigo,
                            descripcion = t.descripcion,
                            nombre = t.nombre,
                            subtipos = t.Subtipos.Select(s => new SubtipoDocumentoDto
                            {
                                codigo = s.codigo,
                                descripcion = s.descripcion,
                                nombre = s.nombre
                            }).ToList()
                        }
                        );
                });
            }

            return Ok(tiposDocumento);
        }
    }
}
