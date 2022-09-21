using APIMaisEventos.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        void Update(UsuarioEvento usuarioEvento);

        // Delete
        void Delete(UsuarioEvento usuarioEvento);

        void UpdateParcial(JsonPatchDocument patch, UsuarioEvento usuarioEvento);

    }
}
