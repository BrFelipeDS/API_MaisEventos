using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da categoria é obrigatória")]
        public string Categoria { get; set; }

        public ICollection<Eventos> _Eventos { get; set; }
    }
}
