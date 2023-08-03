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

    }
}
