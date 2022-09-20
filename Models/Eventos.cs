using System;
using System.ComponentModel.DataAnnotations;

namespace APIMaisEventos.Models
{
    public class Eventos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data e Hora são necessários")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Status do Evento necessário")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Preço é necessários")]
        public double Preco { get; set; }

        [Required(ErrorMessage = "Categoria do Evento necessária")]
        public int CategoriaId { get; set; }
    }
}
