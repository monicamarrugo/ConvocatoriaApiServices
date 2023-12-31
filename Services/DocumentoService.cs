﻿using ConvocatoriaApiServices.Models.Dtos;
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
    }
}
