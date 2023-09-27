using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Security.Cryptography;

namespace ConvocatoriaApiServices.Controllers
{
    [Route("/api/[controller]")]
    public class DocumentoController : ControllerBase
    {

        private readonly IWebHostEnvironment _environment;
        private IDocumentoService _documentoService;
        private readonly IConfiguration _configuration;
        private IInscripcionService _inscripcionService;
        public DocumentoController(IWebHostEnvironment environment, IDocumentoService documentoService,
            IConfiguration configuration, IInscripcionService inscripcionService)
        {
            _environment = environment;
            this._documentoService = documentoService;
            _configuration = configuration;
            _inscripcionService = inscripcionService;
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
                    datosDocumento.codigoInscripcion = datosDocumento.codigoInscripcion.ToUpper();
                    if (!this._inscripcionService.ExistsInscripcion(datosDocumento.codigoInscripcion))
                    {
                        rta.error = "SI";
                        rta.errorDetail = "¡No se encontró el código de inscripción!";
                        return Ok(rta);
                    }
                    var dirUploads = _configuration.GetSection("CustomProperties").GetValue<String>("DirUploads");
                    var dirInscripcion = Path.Combine(dirUploads, "DocumentosConvocatoria",
                        datosDocumento.codigoInscripcion);
                    if (!Directory.Exists(dirInscripcion))
                    {
                        Directory.CreateDirectory(dirInscripcion);
                    }

                    var filePath = Path.Combine(dirUploads, "DocumentosConvocatoria", 
                        datosDocumento.codigoInscripcion, file.FileName);

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
                rta.error = "SI";
                rta.errorDetail= ex.Message;
                return Ok(rta);
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

        [HttpPost("descargarDocumentos")]
        public async Task<IActionResult> DescargarZip(string codigoInscripcion)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                if (!this._inscripcionService.ExistsInscripcion(codigoInscripcion))
                {
                    rta.error = "SI";
                    rta.errorDetail = "¡No se encontró el código de inscripción!";
                    return Ok(rta);
                }
                var dirUploads = _configuration.GetSection("CustomProperties").GetValue<String>("DirUploads");
                var dirInscripcion = Path.Combine(dirUploads, "DocumentosConvocatoria",
                    codigoInscripcion);
                if (!Directory.Exists(dirInscripcion))
                {
                    rta.error = "SI";
                    rta.errorDetail = "¡No se encontró archivos para el código de inscripción!";
                    return Ok(rta);
                }
                List<Documento> documentos = _documentoService.ListDocumento(codigoInscripcion);
                List<String> files = new List<String>();
                

                // Crear un archivo temporal ZIP
                var zipPath = Path.Combine(dirUploads, "DocumentosComprimidos",  $"archivos_"+ codigoInscripcion+ ".zip");

                // Crear el archivo ZIP y agregar los archivos
                using (var zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    foreach (var archivo in documentos)
                    {
                        var archivoPath = Path.Combine(dirInscripcion, archivo.contenido); 
                        if (System.IO.File.Exists(archivoPath))
                        {
                            var directorioTipo = archivo.TipoDocumento.descripcion;
                              // Crear un directorio dentro del ZIP
                                var directorioEnZip = zipArchive.CreateEntry($"{directorioTipo}" + "/" + $"{archivo.contenido}");
                           
                                // Agregar el archivo al directorio en el ZIP
                                using (var stream = directorioEnZip.Open())
                                using (var fileStream = new FileStream(archivoPath, FileMode.Open, FileAccess.Read))
                                {
                                    await fileStream.CopyToAsync(stream);
                                }

                               // zipArchive.CreateEntryFromFile(archivoPath, archivo.contenido);
                            
                        }
                    }
                }

                // Leer el contenido del archivo ZIP como bytes
                var zipBytes = await System.IO.File.ReadAllBytesAsync(zipPath);

                // Eliminar el archivo temporal ZIP después de su creación
                System.IO.File.Delete(zipPath);

                // Enviar el archivo ZIP como respuesta HTTP
                return File(zipBytes, "application/zip", $"{codigoInscripcion}.zip");
            }
            catch (Exception ex)
            {
                // Manejar errores si es necesario
                return StatusCode(500, $"Error: {ex.Message}");
            }
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

