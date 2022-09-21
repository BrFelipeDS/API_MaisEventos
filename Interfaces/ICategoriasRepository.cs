using APIMaisEventos.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface ICategoriasRepository
    {
        // CRUD

        // Read
        ICollection<Categorias> GetAll();
        Categorias GetById(int id);

        // Create
        Categorias Insert(Categorias categoria);

        // Update
        void Update(Categorias categoria);

        // Delete
        void Delete(Categorias categoria);

        void UpdateParcial(JsonPatchDocument patch, Categorias categoria);
    }
}
