using APIMaisEventos.Models;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IUsuarioRepository
    {
        // CRUD
        
        // Read
        ICollection<Usuarios> GetAll();
        Usuarios GetById(int id);

        // Create
        Usuarios Insert(Usuarios usuario);

        // Update
        Usuarios Update(int id, Usuarios usuario);

        // Delete
        bool Delete(int id);
    }
}
