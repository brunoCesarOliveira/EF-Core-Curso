using FuscaFilmes.Contexts;
using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilme.Repo.Contrato
{
    public class DiretorRepository(Context _context) : IDiretorRepository
    {

        public Context Context { get; } = _context;

        public IEnumerable<Diretor> GetDiretores()
        {
            return Context.Diretores
                .Include(diretor => diretor.DiretoresFilmes)
                .ThenInclude(df => df.Filme)
                .Include(d => d.DiretorDetalhes)
                .ToList();
        }

        public Diretor GetDiretorByName(string name)
        {
            return Context?.Diretores?
                            .Include(diretor => diretor.DiretoresFilmes)
                             .FirstOrDefault(diretor => diretor.Name.Contains(name)) ?? new Diretor { Id = 99999, Name = "Marina" };

        }

        public IEnumerable<Diretor> GetDiretoresById(int id)
        {
            return Context?.Diretores
                  .Include(diretor => diretor.DiretoresFilmes)
                  .ThenInclude(d => d.Filme)
                  .Include(d=>d.DiretorDetalhes)
                  .Where(diretor => diretor.Id == id)
                  .ToList();
        }

        public void Add(Diretor diretor)
        {
            Context.Diretores.Add(diretor);
        }

        public void Delete(int diretorId)
        {
            var diretor = Context.Diretores.Find(diretorId);

            if (diretor != null)
                Context.Diretores?.Remove(diretor);
        }

        public void Update(Diretor diretorNovo)
        {
            var diretor = Context.Diretores.Find(diretorNovo.Id);

            if (diretor != null)
            {
                diretor.Name = diretorNovo.Name;
                if (diretorNovo.DiretoresFilmes.Count > 0)
                {
                    diretor.DiretoresFilmes.Clear();
                    foreach (var filme in diretorNovo.DiretoresFilmes)

                        diretor.DiretoresFilmes.Add(filme);
                }
            }
        }

        public bool SaveChanges()
        {
            return Context.SaveChanges() > 0;
        }


    }
}
