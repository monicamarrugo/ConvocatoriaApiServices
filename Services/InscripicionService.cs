using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConvocatoriaApiServices.Services
{
    public class InscripicionService : IInscripcionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public InscripicionService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public RtaTransaccion SaveInscripcion(InscripcionDto datosInscripcion)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {
                
                if (datosInscripcion == null)
                {
                    rta.error = "SI";
                    rta.errorDetail = "Datos inscripcion incompletos!";
                    return rta;
                }
                var encontrado = _context.Inscripcion_Convocatorias.Any(i
                    => i.identificacion_participante.Trim()
                    .Equals(datosInscripcion.identificacion.Trim()) 
                    && i.codigo_convocatoria.Equals(datosInscripcion.codigoConvocatoria));

                if (encontrado)
                {
                    rta.error = "SI";
                    rta.errorDetail = String.Format("El participante con número de identificación {0} " +
                        "se encuentra inscrito a la convocatoria!", datosInscripcion.identificacion.Trim());
                    return rta;
                }
               Inscripcion_Convocatoria inscripcion = new Inscripcion_Convocatoria();
                inscripcion.codigo = CodeGenerate(datosInscripcion.codigoConvocatoria
                                    + datosInscripcion.identificacion);
                inscripcion.codigo_convocatoria = datosInscripcion.codigoConvocatoria;
                inscripcion.codigo_perfil = datosInscripcion.codigoPerfil;
                inscripcion.identificacion_participante = datosInscripcion.identificacion;
                inscripcion.Participante = new Participante
                {
                    identificacion = datosInscripcion.identificacion,
                    tipo_identificacion = datosInscripcion.tipoIdentificacion,
                    
                    nombres = datosInscripcion.nombres,
                    apellidos = datosInscripcion.apellidos,
                    email = datosInscripcion.email,
                    telefono = datosInscripcion.telefono,
                    discapacidad = datosInscripcion.discapacidad
                };

                _context.Inscripcion_Convocatorias.Add(inscripcion);
                _context.SaveChanges();

                rta.error = "NO";
                rta.mensaje = "Inscripción realizada con exito!";
                rta.respuesta = inscripcion.codigo;
                return rta;
            }
            catch (Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return rta;
            }
        }

        public RtaTransaccion SaveVerificacionHV(Verificacion_HV datosHV)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {

                if (datosHV == null)
                {
                    rta.error = "SI";
                    rta.errorDetail = "Datos hv incompletos!";
                    return rta;
                }


                    _context.Verificacion_HV.Add(datosHV);
                    _context.SaveChanges();
                

                

                rta.error = "NO";
                rta.mensaje = "Inscripción realizada con exito!";
                rta.respuesta = "OK";
                return rta;
            }
            catch(DbUpdateException u)
            {
                rta.error = "SI";
                rta.errorDetail = "Ya existe un registro para este código de inscripción";
                return rta;
            }
            catch (Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return rta;
            }
        }


        public Inscripcion_Convocatoria FindInscripcion(string codigoInscripcion)
        {
            try
            {
               var inscripcion =
                    _context.Inscripcion_Convocatorias.Include(i => i.Participante).Where(i => i.codigo.Equals(codigoInscripcion)).FirstOrDefault();
                
                return inscripcion;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DtoDocumentoMinimo> GetDocumentosMinimos(string codigoInscripcion)
        {
            List<DtoDocumentoMinimo> minimos = new List<DtoDocumentoMinimo>();
            try
            {
                
                var tiposMinimos = _context.Tipo_DocumentoMinimo.ToList();

               
                    foreach (var tipo in tiposMinimos)
                    {
                        var documentoMinimos =
                         _context.Inscripcion_DocumentoMinimo.Include(dm => dm.Tipo_DocumentoMinimo)
                         .Where(i => i.codigo_inscripcion.Equals(codigoInscripcion) && i.codigo_documento.Equals(tipo.codigo_documento))
                         .FirstOrDefault();

                        if (documentoMinimos != null)
                        {
                            minimos.Add(new DtoDocumentoMinimo()
                            {
                                codigoInscripcion = codigoInscripcion,
                                codigoTipoDocumento = tipo.codigo_documento,
                                nombreTipoDocumento = documentoMinimos.Tipo_DocumentoMinimo.nombre_documento,
                                cumplido = documentoMinimos.cumplido,
                                no_cumplido = documentoMinimos.no_cumplido,
                                observacion = documentoMinimos.observacion
                            });
                        }
                        else
                        {
                        minimos.Add(new DtoDocumentoMinimo()
                        {
                            codigoInscripcion = codigoInscripcion,
                            codigoTipoDocumento = tipo.codigo_documento,
                            nombreTipoDocumento = tipo.nombre_documento,
                            cumplido = false,
                            no_cumplido = false
                        });
                    }
                    }
                

                return minimos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RtaTransaccion SaveRequerimientoMin(List<DtoDocumentoMinimo> documentoMinimo)
        {
            RtaTransaccion rta = new RtaTransaccion();
            try
            {

                if (documentoMinimo == null)
                {
                    rta.error = "SI";
                    rta.errorDetail = "Datos documento incompletos!";
                    return rta;
                }
                foreach (var documento in documentoMinimo)
                {
                    Inscripcion_DocumentoMinimo requerimiento = new Inscripcion_DocumentoMinimo();
                    requerimiento.codigo_inscripcion = documento.codigoInscripcion;
                    requerimiento.codigo_documento = documento.codigoTipoDocumento;
                    requerimiento.cumplido = documento.cumplido;
                    requerimiento.no_cumplido = documento.no_cumplido;
                    requerimiento.observacion = documento.observacion;

                    var documentoMinimos =
                             _context.Inscripcion_DocumentoMinimo.Include(dm => dm.Tipo_DocumentoMinimo)
                             .Where(i => i.codigo_inscripcion.Equals(documento.codigoInscripcion) && i.codigo_documento.Equals(documento.codigoTipoDocumento))
                             .FirstOrDefault();

                    if (documentoMinimos != null)
                    {
                        _context.Inscripcion_DocumentoMinimo.Update(requerimiento);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.Inscripcion_DocumentoMinimo.Add(requerimiento);
                        _context.SaveChanges();
                    }
                }
               

                rta.error = "NO";
                rta.mensaje = "Verificación guardada con exito!";
                rta.respuesta = "";
                return rta;
            }
            catch (Exception ex)
            {
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return rta;
            }
        }

        public Boolean ExistsInscripcion(string codigoInscripcion)
        {
            try
            {
                var existeInscripcion =
                     _context.Inscripcion_Convocatorias.Any(i => i.codigo.Equals(codigoInscripcion));

                return existeInscripcion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CodeGenerate(string cadena)
        {
           var prefix =  _configuration.GetSection("CustomProperties").GetValue<String>("PefixCodeConvocatoria");
           var caracteres = _configuration.GetSection("CustomProperties").GetValue<String>("CaracterGenerator");
           var longitud = _configuration.GetSection("CustomProperties").GetValue<Int32>("sizeCode");
            
            StringBuilder codigo = new StringBuilder();
            StringBuilder codigoRandom = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = random.Next(caracteres.Length);
                codigoRandom.Append(caracteres[indice]);
            }
            codigo.Append(prefix);
            codigo.Append(cadena);
            codigo.Append(codigoRandom.ToString());

            return codigo.ToString();
        }


        public List<Inscripcion_Convocatoria> GetInscripcionByPerfil(String codigoPerfil)
        {
            var listaInscripcion =
                    _context.Inscripcion_Convocatorias.Include(c=>c.Participante).Where(i => i.codigo_perfil.Equals(codigoPerfil) && i.admitido_psicologica == true).ToList();

            return listaInscripcion;
        }

        public List<InscripcionDto> GetAdmitidosHvByPerfil(String codigoPerfil)
        {
            List<InscripcionDto> admitidosHV = new List<InscripcionDto>();
            var listaAdmitidosHV =
                    _context.Verificacion_HV.Include(c => c.Inscripcion_Convocatoria).Include(p => p.Inscripcion_Convocatoria.Participante).Where(i => i.admitido == true 
                    && i.Inscripcion_Convocatoria.codigo_perfil.Equals(codigoPerfil)).ToList();

            foreach (var admitido in listaAdmitidosHV)
            {
                admitidosHV.Add(new InscripcionDto()
                {
                    nombres = admitido.Inscripcion_Convocatoria.Participante.nombres,
                    apellidos = admitido.Inscripcion_Convocatoria.Participante.apellidos,
                    codigoInscripcion = admitido.codigoinscripcion,
                    identificacion = admitido.Inscripcion_Convocatoria.identificacion_participante,
                    codigoPerfil = admitido.Inscripcion_Convocatoria.codigo_perfil
                });
            }

            return admitidosHV;
        }

        public List<EvaluadoDto> GetInscripcionDocumentoMinimoByPerfil(String codigoPerfil)
        {
            List<EvaluadoDto> evaluados = new List<EvaluadoDto>();
            try
            {
                var listaInscripcion =
                   _context.Inscripcion_Convocatorias.Include(c => c.Participante).Where(i => i.codigo_perfil.Equals(codigoPerfil)).ToList();

                foreach (var inscripcion in listaInscripcion)
                {
                    var evaluacionInscrito =
                    _context.Inscripcion_DocumentoMinimo.Where(dm => dm.codigo_inscripcion.Equals(inscripcion.codigo)).ToList();

                    evaluados.Add(new EvaluadoDto()
                    {
                        codigoInscripcion = inscripcion.codigo,
                        evaluado = evaluacionInscrito.Any(ei => ei.codigo_documento.Equals("ADM")),
                        DID = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("DID")).Select(e => e.cumplido).FirstOrDefault(),
                        TPU = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("TPU")).Select(e => e.cumplido).FirstOrDefault(),
                        TMD = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("TMD")).Select(e => e.cumplido).FirstOrDefault(),
                        CED = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("CED")).Select(e => e.cumplido).FirstOrDefault(),
                        CEP = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("CEP")).Select(e => e.cumplido).FirstOrDefault(),
                        CCI = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("CCI")).Select(e => e.cumplido).FirstOrDefault(),
                        ADM = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("ADM")).Select(e => e.cumplido).FirstOrDefault(),
                        observacion = evaluacionInscrito.Where(ei=> ei.codigo_documento.Equals("ADM")).Select(e => e.observacion).FirstOrDefault()                     
                    }
                        );
                }
                   return evaluados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }

        public bool GetEvaluado(string codigoInscripcion)
        {
            try { 
                var evaluado = false;
                evaluado =
                             _context.Inscripcion_DocumentoMinimo
                             .Any(d => d.codigo_inscripcion.Equals(codigoInscripcion) && d.codigo_documento.Equals("ADM"));
                return evaluado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
