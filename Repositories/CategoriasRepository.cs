using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using APIMaisEventos.Contexts;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace APIMaisEventos.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        MaisEventosContext ctx;

        public CategoriasRepository(MaisEventosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Categorias categoria)
        {
            ctx.Categorias.Remove(categoria);
            ctx.SaveChanges();
        }

        public ICollection<Categorias> GetAll()
        {
            var categorias = ctx.Categorias.ToList();
            return categorias;
        }

        public Categorias GetById(int id)
        {
            var categorias = ctx.Categorias.Find(id);                
                //.FirstOrDefault(p => p.Id == id);

            return categorias;
        }

        public Categorias Insert(Categorias categoria)
        {
            ctx.Categorias.Add(categoria);
            ctx.SaveChanges();
            return categoria;
        }

        public void Update(Categorias categoria)
        {
            ctx.Entry(categoria).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Categorias categoria)
        {
            patch.ApplyTo(categoria);
            ctx.Entry(categoria).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}

