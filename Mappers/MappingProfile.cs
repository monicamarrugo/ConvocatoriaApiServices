using AutoMapper;
using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;

namespace ConvocatoriaApiServices.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompentenciasAcademicas, EvaluacionDto>();
            CreateMap<List<CompentenciasAcademicas>, List<EvaluacionDto>>();
        }
    }
}
