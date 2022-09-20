using APIMaisEventos.Models;
using System.Collections;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IEventosRepository
    {
        // CRUD

        // Read
        ICollection<Eventos> GetAll();

        Eventos GetById(int id);

        // Create
        Eventos Insert(Eventos evento);

        // Update
        Eventos Update(int id, Eventos evento);
        
        // Delete
        bool Delete(int id);
    }
}
