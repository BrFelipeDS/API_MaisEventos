using APIMaisEventos.Models;
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
        Categorias Update(int id, Categorias categoria);

        // Delete
        bool Delete(int id);
    }
}
