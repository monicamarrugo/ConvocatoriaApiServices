using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly ApplicationDbContext _context;
        public DocumentoService(ApplicationDbContext context)
        {
           this._context = context;
        }
        public List<Documento> ListDocumento(string codigoInscripcion)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try { 
                List<Documento> documentos =
                    _context.Documentos.Where(d => d.codigo_inscripcion.Equals(codigoInscripcion)).ToList();
                return documentos;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RtaTransaccion SaveDocumento(DocumentoDto datosDocumento)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                Documento documento = new Documento();
                documento.tipo_documento = datosDocumento.tipoDocumento;
                documento.contenido = datosDocumento.ruta;
                documento.codigo_inscripcion = datosDocumento.codigoInscripcion;

                _context.Documentos.Add(documento);
                _context.SaveChanges();
                rta.error = "NO";
                rta.mensaje = "Documento guardado con exito!";
                return rta;
            }
            catch(Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return rta;
            }
        }
    }
}
