using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using APIMaisEventos.Contexts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIMaisEventos.Repositories
{
    public class UsuarioEventoRepository : IUsuarioEventoRepository
    {
        MaisEventosContext ctx;

        public UsuarioEventoRepository(MaisEventosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(UsuarioEvento usuarioEvento)
        {
            ctx.UsuarioEvento.Remove(usuarioEvento);
            ctx.SaveChanges();
        }

        public ICollection<UsuarioEvento> GetAll()
        {
            var categorias = ctx.UsuarioEvento.ToList();
            return categorias;
        }

        public UsuarioEvento GetById(int id)
        {
            var usuarioEvento = ctx.UsuarioEvento.Find(id);
            //.FirstOrDefault(p => p.Id == id);

            return usuarioEvento;
        }

        public UsuarioEvento Insert(UsuarioEvento usuarioEvento)
        {
            ctx.UsuarioEvento.Add(usuarioEvento);
            ctx.SaveChanges();
            return usuarioEvento;
        }

        public void Update(UsuarioEvento usuarioEvento)
        {
            ctx.Entry(usuarioEvento).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, UsuarioEvento usuarioEvento)
        {
            patch.ApplyTo(usuarioEvento);
            ctx.Entry(usuarioEvento).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
