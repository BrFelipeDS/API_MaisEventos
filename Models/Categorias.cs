using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Categoria { get; set; }

        public ICollection<Eventos> Eventos { get; set; }
    }
}
