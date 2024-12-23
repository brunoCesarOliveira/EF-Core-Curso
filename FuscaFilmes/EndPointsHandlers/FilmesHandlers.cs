using FuscaFilmes.Contexts;
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuscaFilmes.EndPointHandlers
{
    public static class FilmesHandlers
    {
        public static async Task<IEnumerable<Filme>> GetFilmesAsync(Context context)
        {
            return await context?.Filmes?
                           .Include(filme => filme.Diretores)
                           .ThenInclude(filme => filme)

                           //  .Where(filme => filme.Id == id)
                           // .OrderBy(filme => filme.Ano)
                           .OrderByDescending(filme => filme.Ano)
                           //.ThenBy(filme => filme.Titulo)
                           .ThenByDescending(filme => filme.Titulo)
                           .ToListAsync()!;
        }

        public static async Task<IEnumerable<Filme>> GetFilmeByIdAsync(int Id, Context context)
        {
            return await context?.Filmes?
                           .Include(filme => filme.Diretores)
                           .Where(filme => filme.Id == Id)
                           .ToListAsync()!;
        }

        public static async Task<IEnumerable<Filme>> GetFilmeEEFunctionsByTituloAsync(string titulo, Context context)
        {
            return await context?.Filmes?
                           .Include(filme => filme.Diretores)
                           .Where(filme =>
                                 EF.Functions.Like(filme.Titulo, $"%{titulo}%"))
                           .ToListAsync()!;
        }

        public static async Task<IEnumerable<Filme>> GetFilmeContainsTituloAsync(string titulo, Context context)
        {
            return await context.Filmes
                          .Include(filme => filme.Diretores)
                          .Where(filme => filme.Titulo.Contains(titulo))
                          .ToListAsync();
        }

        public static async Task<IEnumerable<Filme>>? GetFilmeByNameAsync(string titulo, Context context)
        {
            return await context?.Filmes?
                           .Include(filme => filme.Diretores)
                           .Where(filme => filme.Titulo.Contains(titulo))
                           .ToListAsync()!;
        }

        public static async Task AddFilmeAsync(Context context, Filme filme)
        {
            context.Filmes?.AddAsync(filme);
            await context.SaveChangesAsync();
        }

        public static async Task ExecuteDeleteFilmeAsync(Context context, int filmeId)
        {
            // var filme = new Filme { Id = filmeId, Titulo = "xpto" };  // Crinado um objeto Fack
            await context.Filmes
                         .Where(filme => filme.Id == filmeId)
                         .ExecuteDeleteAsync<Filme>();  // Deleta todos os filmes que obedecem a condição e retorna  a quantidade que foi deletada

        }

        public static async Task<IResult> UpdateFilme(Context context, FilmeUpdate filmeUpdate)
        {
            var filme = await context.Filmes.FindAsync(filmeUpdate.Id);
            if (filme == null)
                return Results.Ok("Filme não encontrado");

            filme.Titulo = filmeUpdate.Titulo;
            filme.Ano = filmeUpdate.Ano;

            context.Filmes.Update(filme);
            await context.SaveChangesAsync();

            return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com Sucesso");
        }

        public static async Task<IResult> ExecuteUpdateFilmeAsync(Context context, FilmeUpdate filmeUpdate)
        {
            // var filme = new Filme { Id = filmeId, Titulo = "xpto" };  // Crinado um objeto Fack
            var linhasAfetadas = await context.Filmes
                    .Where(filme => filme.Id == filmeUpdate.Id)
                    .ExecuteUpdateAsync(setter => setter
                    .SetProperty(f => f.Titulo, filmeUpdate.Titulo)
                    .SetProperty(f => f.Ano, filmeUpdate.Ano)
                    );

            if (linhasAfetadas > 0)
                return Results.Ok($"Você teve um total de {linhasAfetadas} Linha(s) afeta(s)");
            else
                return Results.NoContent();
        }
    }
}
