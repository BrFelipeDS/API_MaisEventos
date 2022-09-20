using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Usuarios
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8)]
        public string Senha { get; set; }


        public string Imagem { get; set; }
    }
}
