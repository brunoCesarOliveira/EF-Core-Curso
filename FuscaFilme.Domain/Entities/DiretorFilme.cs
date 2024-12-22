using FuscaFilmes.Domain.Entities;

namespace FuscaFilme.Domain.Entities
{
    public class DiretorFilme
    {
        public int DiretorId { get; set; }
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }
        public Diretor Diretor { get; set; }
    }
}
