using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIMaisEventos.Models
{
    public class Eventos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public double Preco { get; set; }

        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; }
        public Categorias Categorias { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<UsuarioEvento> UsuarioEventos { get; set; }
    }
}
