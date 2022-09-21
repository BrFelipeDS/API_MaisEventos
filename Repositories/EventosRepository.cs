using APIMaisEventos.Contexts;
using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace APIMaisEventos.Repositories
{
    public class EventosRepository : IEventosRepository
    {
        MaisEventosContext ctx;

        public EventosRepository(MaisEventosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Eventos evento)
        {
            ctx.Eventos.Remove(evento);
            ctx.SaveChanges();
        }

        public ICollection<Eventos> GetAll()
        {
            var categorias = ctx.Eventos.ToList();
            return categorias;
        }

        public Eventos GetById(int id)
        {
            var evento = ctx.Eventos.Find(id);
            //.FirstOrDefault(p => p.Id == id);

            return evento;
        }

        public Eventos Insert(Eventos evento)
        {
            ctx.Eventos.Add(evento);
            ctx.SaveChanges();
            return evento;
        }

        public void Update(Eventos evento)
        {
            ctx.Entry(evento).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Eventos evento)
        {
            patch.ApplyTo(evento);
            ctx.Entry(evento).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
