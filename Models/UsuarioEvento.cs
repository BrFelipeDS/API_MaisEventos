using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class UsuarioEvento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Id do Usuário é necessário")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Id do Evento é necessário")]
        public int EventoId { get; set; }
    }
}
