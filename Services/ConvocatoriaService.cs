using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services
{
    public class ConvocatoriaService: IConvocatoriaService
    {
        private readonly ApplicationDbContext _context;

        public ConvocatoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Convocatoria> GetAllConvocatorias()
        {
            return _context.Convocatorias.ToList();
        }

        public ConvocatoriaDto EstadoConvocatoria(int IdConvocatoria)
        {
            ConvocatoriaDto convocatoriaResponse = new ConvocatoriaDto();
            Convocatoria convocatoria = _context.Convocatorias.Where(c => c.codigo.Equals(IdConvocatoria)).First();
            convocatoriaResponse.codigo = convocatoria.codigo;
            convocatoriaResponse.nombre = convocatoria.nombre;
            convocatoriaResponse.fechaInicio = convocatoria.fecha_inicio.ToString("dd-MM-yyyy");
            convocatoriaResponse.fechaFin = convocatoria.fecha_fin.ToString("dd-MM-yyyy");

            if (convocatoria.fecha_fin >= DateTime.Now.Date)
            {
                convocatoriaResponse.activo = true; 
            }
            else
            {
                convocatoriaResponse.activo = false;
            }
            return convocatoriaResponse;
        }

    }
}
