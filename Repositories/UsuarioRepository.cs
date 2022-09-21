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
    public class UsuarioRepository : IUsuarioRepository
    {
        MaisEventosContext ctx;

        public UsuarioRepository(MaisEventosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Usuarios usuario)
        {
            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        public ICollection<Usuarios> GetAll()
        {
            var categorias = ctx.Usuarios.ToList();
            return categorias;
        }

        public Usuarios GetById(int id)
        {
            var usuario = ctx.Usuarios.Find(id);
            //.FirstOrDefault(p => p.Id == id);

            return usuario;
        }

        public Usuarios Insert(Usuarios usuario)
        {
            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
            return usuario;
        }

        public void Update(Usuarios usuario)
        {
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Usuarios usuario)
        {
            patch.ApplyTo(usuario);
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
