﻿using ConvocatoriaApiServices.Models.Dtos;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services.Interfaces
{
    public interface IInscripcionService
    {
        public RtaTransaccion SaveInscripcion(InscripcionDto datosInscripcion);
        public Inscripcion_Convocatoria FindInscripcion(String codigoInscripcion);

        public Boolean ExistsInscripcion(string codigoInscripcion);

        public string CodeGenerate(string cadena);

        public List<Inscripcion_Convocatoria> GetInscripcionByPerfil(String codigoPerfil);

        public List<DtoDocumentoMinimo> GetDocumentosMinimos(string codigoInscripcion);

        public RtaTransaccion SaveRequerimientoMin(List<DtoDocumentoMinimo> documentoMinimo);
    }
}
