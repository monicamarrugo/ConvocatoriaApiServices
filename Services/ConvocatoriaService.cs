using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        public Verificacion_HV FindEvaluacionHojaVida(string codigoInscripcion)
        {
            try
            {
                var verificacion =
                     _context.Verificacion_HV.Where(v => v.codigoinscripcion.Equals(codigoInscripcion)).FirstOrDefault();

                return verificacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ConsolidadoDto> GetEvaluacionesHojaVida(List<string> perfiles)
        {
            List<ConsolidadoDto> consolidadoDtos = new List<ConsolidadoDto>();
            try
            {
                var listaVerificacion =
                     _context.Verificacion_HV.Include( i=> i.Inscripcion_Convocatoria)
                     .Where(v => perfiles.Contains(v.Inscripcion_Convocatoria.codigo_perfil)).ToList();

                foreach(var item in listaVerificacion)
                {
                    consolidadoDtos.Add(new ConsolidadoDto()
                    {
                        codigoperfil = item.Inscripcion_Convocatoria.codigo_perfil,
                        codigoinscripcion = item.codigoinscripcion,
                        totalFormacion = item.totalFormacion,
                        totalFormacionIn = item.totalFormacionIn,
                        totalProduccion = item.totalProduccion,
                        totalExperiencia = item.totalExperiencia,
                        totalEvaluacion = item.totalEvaluacion,
                        admitido = item.admitido
                    });
                }

                return consolidadoDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
