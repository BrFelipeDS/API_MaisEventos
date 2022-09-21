using APIMaisEventos.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        void Update(Eventos evento);
        
        // Delete
        void Delete(Eventos evento);

        void UpdateParcial(JsonPatchDocument patch, Eventos evento);
    }
}
