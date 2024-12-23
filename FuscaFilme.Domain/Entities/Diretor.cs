using FuscaFilme.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuscaFilmes.Domain.Entities
{
    public class Diretor
    {
        /*Data Annotation
        [Key]
        [Column("id_diretor")]]
        */
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<DiretorFilme> DiretoresFilmes { get; set; } = null!;
        public ICollection<Filme> Filmes { get; set; } = null!;
        public DiretorDetalhe DiretorDetalhes { get; set; }
    }
}
