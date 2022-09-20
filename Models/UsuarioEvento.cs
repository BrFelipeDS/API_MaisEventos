using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIMaisEventos.Models
{
    public class UsuarioEvento
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuarios")]
        public int UsuarioId { get; set; }
        public Usuarios Usuarios { get; set; }

        [ForeignKey("Eventos")]
        public int EventoId { get; set; }
        public Eventos Eventos { get; set; }
    }
}
