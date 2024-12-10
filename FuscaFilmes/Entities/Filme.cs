namespace FuscaFilmes.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public int Ano { get; set; }
        //O diretor pode ser null
        public Diretor? Diretor { get; set; }
    }
}