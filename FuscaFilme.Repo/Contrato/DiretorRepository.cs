using FuscaFilmes.Contexts;
using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilme.Repo.Contrato
{
    public class DiretorRepository(Context _context) : IDiretorRepository
    {

        public Context Context { get; } = _context;

        public async Task<IEnumerable<Diretor>> GetDiretoresAsync()
        {
            return await Context.Diretores
                                .Include(diretor => diretor.DiretoresFilmes)
                                .ThenInclude(df => df.Filme)
                                .Include(d => d.DiretorDetalhes)
                                .ToListAsync();
        }

        public async Task<Diretor> GetDiretorByNameAsync(string name)
        {
            return await Context?.Diretores?
                            .Include(diretor => diretor.DiretoresFilmes)
                            .FirstOrDefaultAsync(diretor => diretor.Name.Contains(name))
                                                ?? new Diretor { Id = 99999, Name = "Marina" };

        }

        public async Task<IEnumerable<Diretor>> GetDiretoresByIdAsync(int id)
        {
            return await Context?.Diretores
                                 .Include(diretor => diretor.DiretoresFilmes)
                                 .ThenInclude(d => d.Filme)
                                 .Include(d => d.DiretorDetalhes)
                                 .Where(diretor => diretor.Id == id)
                                 .ToListAsync();
        }

        public async Task AddAsync(Diretor diretor)
        {
            await Context.Diretores.AddAsync(diretor);
        }

        public async Task Delete(int diretorId)
        {
            var diretor = await Context.Diretores.FindAsync(diretorId);

            if (diretor != null)
                Context.Diretores?.Remove(diretor);
        }

        public async Task Update(Diretor diretorNovo)
        {
            var diretor = await Context.Diretores.FindAsync(diretorNovo.Id);

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

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
