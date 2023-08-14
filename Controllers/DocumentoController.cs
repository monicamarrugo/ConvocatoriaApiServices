using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConvocatoriaApiServices.Controllers
{
    [Route("/api/[controller]")]
    public class DocumentoController : ControllerBase
    {

        private readonly IWebHostEnvironment _environment;
        private IDocumentoService _documentoService;
        public DocumentoController(IWebHostEnvironment environment, IDocumentoService documentoService)
        {
            _environment = environment;
            this._documentoService = documentoService;
        }

        [HttpPost("subir2")]
        public async Task<IActionResult> UploadDocumento([FromBody] DocumentoUploadedDto datosDocumento)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                if (datosDocumento.archivo == null || datosDocumento.archivo.Length == 0)
                {
                    rta.error = "SI";
                    rta.mensaje = "No se ha seleccionado ningún archivo o el archivo está vacío.";
                    return Ok(rta);
                }
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "DocumentosConvocatoria");
                string filePath = Path.Combine(uploadsFolder, datosDocumento.archivo.FileName);

                // Guardar el archivo en el servidor
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await datosDocumento.archivo.CopyToAsync(stream);
                }

                datosDocumento.ruta = filePath;

                var respuestaDocumento = this._documentoService.SaveDocumento(datosDocumento);
                return Ok(respuestaDocumento);
            }
            catch(Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return Ok(rta);
            }
        }

        [HttpPost("subir")]
        public async Task<IActionResult> UploadFileWithProperties([FromForm] DocumentoUploadedDto datosDocumento)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    datosDocumento.ruta = file.FileName;
                    var respuestaDocumento = this._documentoService.SaveDocumento(datosDocumento);
                    return Ok(respuestaDocumento);
                }
                return BadRequest("No file was uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPost("descargar")]
        public IActionResult DownloadFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName)) {
                return BadRequest("Nombre de archivo es nulo");
            }
            // Ruta completa al archivo en el servidor (puede variar según tu aplicación)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

            // Comprobar si el archivo existe
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var mimeType = "application/pdf"; // Cambiar según el tipo de archivo

            return File(fileStream, mimeType, fileName);
        }


        [HttpGet("listar")]
        public IActionResult GetListDocumento([FromQuery] string codigoInscripcion)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                List<DocumentoResponseDto> listaDocumento = new List<DocumentoResponseDto>();
                var documentos = this._documentoService.ListDocumento(codigoInscripcion);
                documentos.ForEach(documento => {

                    listaDocumento.Add(
                    new DocumentoResponseDto
                    {
                        contenido = documento.contenido,
                        tipoDocumento = documento.tipo_documento,
                        descTipodocumento = documento.TipoDocumento.descripcion
                    }
                    );
                    }
                    );
                rta.error = "NO";
                rta.respuesta = JsonConvert.SerializeObject(listaDocumento);
                return Ok(listaDocumento);
            }
            catch(Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail= ex.Message;
                return Ok(rta);
            }
           
        }
    }
}

