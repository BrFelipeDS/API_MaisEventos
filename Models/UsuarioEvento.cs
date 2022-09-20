using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIMaisEventos.Models
{
    public class UsuarioEvento
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuarios")]
        [Required(ErrorMessage = "Id do Usuário é necessário")]
        public int UsuarioId { get; set; }
        public Usuarios Usuarios { get; set; }

        [ForeignKey("Eventos")]
        [Required(ErrorMessage = "Id do Evento é necessário")]
        public int EventoId { get; set; }
        public Eventos Eventos { get; set; }
    }
}
