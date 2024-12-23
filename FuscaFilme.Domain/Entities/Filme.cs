using FuscaFilme.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuscaFilmes.Domain.Entities
{
    public class Filme
    {
        public int Id { get; set; }

        //DataAnnotation
        [Required]
        [MaxLength(100)]
        public required string Titulo { get; set; }

        [Range(1900, 2050)]
        public int Ano { get; set; }
        //  public Diretor Diretor { get; set; } // Relacioamento 1 para Muitos com Diretor
        //  public int DiretorId { get; set; }


        [Column(TypeName = "decimal(18f,2")]
        public decimal Orcamento { get; set; }
        public ICollection<DiretorFilme> DiretoresFilmes { get; set; } = null!;
        public ICollection<Diretor> Diretores { get; set; } = null!;
    }
}
