using APIMaisEventos.Models;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IUsuarioEventoRepository
    {
        // CRUD

        // Read
        ICollection<UsuarioEvento> GetAll();
        UsuarioEvento GetById(int id);

        // Create
        UsuarioEvento Insert(UsuarioEvento usuarioEvento);

        // Update
        UsuarioEvento Update(int id, UsuarioEvento usuarioEvento);

        // Delete
        bool Delete(int id);
    }
}
