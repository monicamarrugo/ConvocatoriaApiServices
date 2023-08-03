﻿using ConvocatoriaApiServices.Services.Interfaces;
using ConvocatoriaServices.Context.Application;
using ConvocatoriaServices.Models;

namespace ConvocatoriaApiServices.Services
{
    public class ListasTiposBasicosService : IListasTiposBasicosService
    {
        private readonly ApplicationDbContext _context;

        public ListasTiposBasicosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Perfil> GetAllPerfiles()
        {
            return _context.Perfiles.ToList();
        }

        public List<Tipo_Identificacion> GetAllTiposIdentificacion() 
        {
            return _context.Tipo_Identificaciones.ToList();
        }

    }
}