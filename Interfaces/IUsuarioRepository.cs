using APIMaisEventos.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        void Update(Usuarios usuario);

        // Delete
        void Delete(Usuarios usuario);

        void UpdateParcial(JsonPatchDocument patch, Usuarios usuario);
    }
}
