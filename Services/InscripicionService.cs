using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;
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
                    telefono = datosInscripcion.telefono
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


        public Inscripcion_Convocatoria FindInscripcion(string codigoInscripcion)
        {
            try
            {
               var inscripcion =
                    _context.Inscripcion_Convocatorias.Where(i => i.codigo.Equals(codigoInscripcion)).FirstOrDefault();
                
                return inscripcion;
            }
            catch(Exception ex)
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
    }
}
