using APIMaisEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMaisEventos.Contexts
{
    public class MaisEventosContext : DbContext
    {

        public MaisEventosContext(DbContextOptions<MaisEventosContext> options) : base(options)
        {

        }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<UsuarioEvento> UsuarioEvento { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
