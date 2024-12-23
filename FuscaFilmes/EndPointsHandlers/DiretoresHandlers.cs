
using FuscaFilme.Repo.Contrato;
using FuscaFilmes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuscaFilmes.EndPointHandlers
{
    public static class DiretoresHandlers
    {
        public static Task<IEnumerable<Diretor>> GetDiretoresAsync(IDiretorRepository diretorRepository)
        {
            return diretorRepository.GetDiretoresAsync();

        }

        public static Task<Diretor> GetDiretorByNameAsync(string name, IDiretorRepository diretorRepository)
        {
            return diretorRepository.GetDiretorByNameAsync(name);
        }

        public static Task<IEnumerable<Diretor>> GetDiretorByIdAsync(int id, IDiretorRepository diretorRepository)
        {
            return diretorRepository.GetDiretoresByIdAsync(id);

        }

        public static async Task AddDiretorAsync(IDiretorRepository diretorRepository, Diretor diretor)
        {
            await diretorRepository.AddAsync(diretor);
            await diretorRepository.SaveChangesAsync();
        }

        public static async Task UpdateDiretor(IDiretorRepository diretorRepository, Diretor diretorNovo)
        {
            _ = diretorRepository.Update(diretorNovo);
            await diretorRepository.SaveChangesAsync();

        }

        public static async Task DeleteDiretor(IDiretorRepository diretorRepository, int diretorId)
        {
            _ = diretorRepository.Delete(diretorId);
            await diretorRepository.SaveChangesAsync();
        }
    }
}
