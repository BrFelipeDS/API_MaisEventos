using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIMaisEventos.Models
{
    public class Eventos
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data e Hora são necessários")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Status do Evento necessário")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Preço é necessários")]
        public double Preco { get; set; }

        [ForeignKey("Categorias")]
        [Required(ErrorMessage = "Categoria do Evento necessária")]
        public int CategoriaId { get; set; }
        public Categorias Categorias { get; set; }

        public ICollection<UsuarioEvento> UsuarioEventos { get; set; }
    }
}
