using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using Microsoft.EntityFrameworkCore;

namespace ConvocatoriaApiServices.Services
{
    public class ComisionConvocatoriaService: IComisionConvocatoriaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ComisionConvocatoriaService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public List<Comision_Convocatoria> GetAllComisionConvocatoria()
        {
            try
            {
                var comisiones =
                     _context.Comision_Convocatorias.OrderBy(c => c.id).ToList();

                return comisiones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ComisionDto ExistComisionConvocatoria(UsuarioDto usuario)
        {
            try
            {
                ComisionDto comision = new ComisionDto();
                byte[] bPassword = Convert.FromBase64String(usuario.contrasena);
                var contrasena = System.Text.Encoding.UTF8.GetString(bPassword);

                var existeComision =
                     _context.Comision_Convocatorias.Where(c => c.usuario.Equals(usuario.usuario) && c.password.Equals(contrasena)).FirstOrDefault();

                if(existeComision != null)
                {
                    comision.id = existeComision.id;
                    comision.nombre = existeComision.nombre;
                    comision.facultad = existeComision.facultad;
                    comision.tipoUsuario = existeComision.tipoUsuario;
                }
                return comision;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
