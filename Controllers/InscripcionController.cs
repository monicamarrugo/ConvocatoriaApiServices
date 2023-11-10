using ConvocatoriaApiServices.Models;
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
            InscripcionDto inscripcionDto = new InscripcionDto();
            try
            {
                var inscripcion = this._inscripcionService.FindInscripcion(codigoInscripcion);
                if(inscripcion != null)
                {
                    inscripcionDto.tipoIdentificacion = inscripcion.Participante.tipo_identificacion;
                    inscripcionDto.identificacion = inscripcion.Participante.identificacion;
                    inscripcionDto.nombres = inscripcion.Participante.nombres;
                    inscripcionDto.apellidos = inscripcion.Participante.apellidos;
                }
                rta.error = "NO";
                rta.respuesta = JsonConvert.SerializeObject(inscripcionDto);

                return Ok(inscripcionDto);
            }catch(Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return Ok(rta);
            }
        }

        [HttpGet("inscripcionPerfil")]
        public IActionResult GetInscripcionByPerfil(string codigoPerfil)
        {
            RtaTransaccion rta = new RtaTransaccion();
            List<InscripcionDto> listaInscripcion = new List<InscripcionDto>();
            try
            {
                var inscripciones = this._inscripcionService.GetInscripcionByPerfil(codigoPerfil);
                foreach(var inscripcion in inscripciones)
                {
                    var evaluado = this._inscripcionService.GetEvaluado(inscripcion.codigo);
                    listaInscripcion.Add(new InscripcionDto()
                    {
                        codigoInscripcion = inscripcion.codigo,
                        tipoIdentificacion = inscripcion.Participante.tipo_identificacion,
                        identificacion = inscripcion.Participante.identificacion,
                        nombres = inscripcion.Participante.nombres,
                        apellidos = inscripcion.Participante.apellidos,
                        codigoPerfil = inscripcion.codigo_perfil,
                        evaluado = evaluado
                    });
                }
                rta.error = "NO";
                rta.respuesta = JsonConvert.SerializeObject(listaInscripcion);

                return Ok(listaInscripcion);
            }
            catch (Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return Ok(rta);
            }
        }

        [HttpGet("consultarReqMinimos")]
        public IActionResult GetInscripcionDocMinimos(string codigoInscripcion)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                var inscripcion = this._inscripcionService.GetDocumentosMinimos(codigoInscripcion);
                rta.error = "NO";
                rta.respuesta = JsonConvert.SerializeObject(inscripcion);

                return Ok(inscripcion);
            }
            catch (Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return Ok(rta);
            }
        }

        [HttpGet("consultarEvaluacionMinimos")]
        public IActionResult GetInscripcionMinimosByPerfil(string codigoPerfil)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                var inscripcion = this._inscripcionService.GetInscripcionDocumentoMinimoByPerfil(codigoPerfil);
                rta.error = "NO";
                rta.respuesta = JsonConvert.SerializeObject(inscripcion);

                return Ok(inscripcion);
            }
            catch (Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return Ok(rta);
            }
        }

        [HttpPost("guardarRequisito")]
        public IActionResult SaveDocumentoMinimo([FromBody] List<DtoDocumentoMinimo> datosDocumento)
        {
            var respuestaInscripcion = this._inscripcionService.SaveRequerimientoMin(datosDocumento);
            return Ok(respuestaInscripcion);
        }

        [HttpPost("guardarHojaVida")]
        public IActionResult SaveHV([FromBody] Verificacion_HV datosHv)
        {
            var respuestaInscripcion = this._inscripcionService.SaveVerificacionHV(datosHv);
            return Ok(respuestaInscripcion);
        }
    }
}
