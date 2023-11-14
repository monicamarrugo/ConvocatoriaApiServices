using AutoMapper;
using ConvocatoriaApiServices.Models;
using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

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

        public EvaluacionDto GetCompentenciasAcademicas()
        {
            List<Competencias> competenciasDto = new List<Competencias>();
            List<CompentenciasAcademicas> competencias =  _context.CompentenciasAcademicas.ToList();
            foreach(var competencia in competencias)
            {
                competenciasDto.Add(new Competencias()
                {
                    idCompetencia = competencia.id,
                    descripcion = competencia.descripcion,
                    tipo = competencia.tipo,
                    inferior = competencia.inferior,
                    medio = competencia.medio,
                    superior = competencia.superior,
                    peso = competencia.peso
                });
            }
            EvaluacionDto evaluacionDto = new EvaluacionDto();
            evaluacionDto.competencias = competenciasDto;
            return evaluacionDto;
        }

        public EvaluacionDto GetCompentenciasPonderadas(string codigoInscripcion)
        {
            List<Competencias> competenciasDto = new List<Competencias>();
            List<Detalle_Evaluacion> detalles = new List<Detalle_Evaluacion>();
            List<Evaluacion_Competencia> evaluaciones = _context.Evaluacion_Competencia.Include(ev => ev.Detalle_Evaluacion)
                .ThenInclude(c => c.CompentenciasAcademicas)
                .Where( e => e.codigo_inscripcion.Equals(codigoInscripcion)).ToList();
            int numEvaluaciones = evaluaciones.Count;

          
            foreach (var evaluacion in evaluaciones)
            {
                foreach(var detalle in evaluacion.Detalle_Evaluacion)
                {
                    detalles.Add(detalle);
                }
              
            }

            var resultado = detalles
            .GroupBy(item => item.id_competencia)
            .Select(grupo => new Competencias
            {
                idCompetencia = grupo.Key,
                descripcion = grupo.Select( d=> d.CompentenciasAcademicas.descripcion).FirstOrDefault(),
                calificacionPromediada = (double)grupo.Sum(item => item.calificacionPonderada)/ numEvaluaciones
            }).ToList(); 

            EvaluacionDto evaluacionDto = new EvaluacionDto();
            evaluacionDto.competencias = resultado;
            return evaluacionDto;
        }

        public RtaTransaccion SaveEvalucionCompetencia(EvaluacionDto dtoEvaluacion)
        {
            try
            {
                RtaTransaccion rta = new RtaTransaccion();
                var encontrado =  _context.Evaluacion_Competencia
                    .Any(c => c.identificacion_evaluador.Equals(dtoEvaluacion.cabeceraEvaluacion.identificacionEvaluador) 
                    && c.codigo_inscripcion.Equals(dtoEvaluacion.cabeceraEvaluacion.codigoInscripicion));
                if (encontrado)
                {
                    rta.error = "SI";
                    rta.errorDetail = "Usted ya realizó una evaluación a este participante!";
                    return rta;
                }
                
                Evaluacion_Competencia evaluacion = new Evaluacion_Competencia();
                List<Detalle_Evaluacion> detalleEvaluacion = new List<Detalle_Evaluacion>();

                evaluacion.codigo_inscripcion = dtoEvaluacion.cabeceraEvaluacion.codigoInscripicion;
                evaluacion.identificacion_evaluador = dtoEvaluacion.cabeceraEvaluacion.identificacionEvaluador;
                evaluacion.nombre_evaluador = dtoEvaluacion.cabeceraEvaluacion.nombreEvaluador;
                evaluacion.total = dtoEvaluacion.cabeceraEvaluacion.totalEvaluacion;
                evaluacion.observacion = dtoEvaluacion.cabeceraEvaluacion.observacion;

                foreach (var detalle in dtoEvaluacion.detalleEvaluacion)
                {
                    detalleEvaluacion.Add(new Detalle_Evaluacion()
                    {
                        id_competencia = detalle.idCompetencia,
                        calificacionObtenida = detalle.calificacionObtenida,
                        calificacionPonderada = detalle.calificacionPonderada
                    });
                }
                evaluacion.Detalle_Evaluacion = detalleEvaluacion;
                _context.Evaluacion_Competencia.Add(evaluacion);
                _context.SaveChanges();

                rta.error = "NO";
                rta.mensaje = "Evaluación guardada con exito!";
                return rta;
            }
            catch (Exception ex)
            {
                RtaTransaccion rta = new RtaTransaccion();
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return rta;
            }
            
        }
        public RtaTransaccion SavePromedioCompetencia(ConsolidadoCompetenciaDto dtoConsolidado)
        {
            try
            {
                RtaTransaccion rta = new RtaTransaccion();
                var evaluaciones = _context.Evaluacion_Competencia
                    .Where(c => c.codigo_inscripcion.Equals(dtoConsolidado.codigo_inscripcion));
                if (evaluaciones.Count() == 0)
                {
                    rta.error = "SI";
                    rta.errorDetail = "NO Existe evaluaciones realizadas!";
                    return rta;
                }
              
                foreach (var evaluacion in evaluaciones)
                {
                    evaluacion.total_ponderado = dtoConsolidado.total_ponderado;
                    evaluacion.elegible = dtoConsolidado.elegible;
                    //_context.Evaluacion_Competencia.Update(evaluacion);
                    _context.SaveChanges();
                }

                rta.error = "NO";
                rta.mensaje = "Evaluación guardada con exito!";
                return rta;
            }
            catch (Exception ex)
            {
                RtaTransaccion rta = new RtaTransaccion();
                rta.error = "SI";
                rta.errorDetail = ex.Message;
                return rta;
            }

        }
    }
}
