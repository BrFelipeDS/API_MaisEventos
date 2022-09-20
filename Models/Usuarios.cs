using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Senha { get; set; }


        public string Imagem { get; set; }

        public ICollection<UsuarioEvento> UsuarioEventos { get; set; }
    }
}
