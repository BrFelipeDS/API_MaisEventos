using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Categorias
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da categoria é obrigatória")]
        public string Categoria { get; set; }
    }
}
