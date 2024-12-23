using FuscaFilmes.Domain.Entities;

namespace FuscaFilme.Repo.Contrato
{
    public interface IDiretorRepository
    {
        Task<IEnumerable<Diretor>> GetDiretoresAsync();
        Task<Diretor> GetDiretorByNameAsync(string name);
        Task<IEnumerable<Diretor>> GetDiretoresByIdAsync(int id);
        Task AddAsync(Diretor diretor);
        Task Update(Diretor diretor);
        Task Delete(int diretorId);
        Task<bool> SaveChangesAsync();

    }
}
