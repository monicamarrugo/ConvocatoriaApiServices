using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;
using Microsoft.EntityFrameworkCore;

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
                    _context.Documentos
                    .Include(t=>t.TipoDocumento)
                    .ThenInclude(s => s.Subtipos)
                    .Where(d => d.codigo_inscripcion.Equals(codigoInscripcion))
                    .ToList();
                return documentos;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RtaTransaccion SaveDocumento(DocumentoUploadedDto datosDocumento)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                Documento documento = new Documento();
                documento.tipo_documento = datosDocumento.tipoDocumento;
                documento.contenido = datosDocumento.ruta;
                documento.codigo_inscripcion = datosDocumento.codigoInscripcion;
                documento.subtipo_documento = datosDocumento.subtipoDocumento;

                _context.Documentos.Add(documento);
                _context.SaveChanges();
                rta.error = "NO";
                rta.mensaje = "Documento guardado con exito!";
                return rta;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteDocumento(int idDocumento)
        {

            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                var documento = _context.Documentos.Find(idDocumento);
                if (documento == null)
                {
                    return false;
                }


                _context.Documentos.Remove(documento);
                _context.SaveChanges();
                rta.error = "NO";
                rta.mensaje = "Documento eliminado con exito!";
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
