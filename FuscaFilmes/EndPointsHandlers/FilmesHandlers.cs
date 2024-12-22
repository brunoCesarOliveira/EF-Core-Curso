using FuscaFilmes.Contexts;
using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FuscaFilmes.EndPointHandlers
{
    public static class FilmesHandlers
    {
        public static IEnumerable<Filme> GetFilmes(int id, Context context)
        {
            return context?.Filmes?
                           .Include(filme => filme.Diretores)
                           .ThenInclude(filme => filme)

                           //  .Where(filme => filme.Id == id)
                           // .OrderBy(filme => filme.Ano)
                           .OrderByDescending(filme => filme.Ano)
                           //.ThenBy(filme => filme.Titulo)
                           .ThenByDescending(filme => filme.Titulo)
                           .ToList();
        }

        public static IEnumerable<Filme>? GetFilmeById(int Id, Context context)
        {
            return context?.Filmes?
                           .Where(filme => filme.Id == Id)
                           .Include(filme => filme.Diretores).ToList();
        }

        public static IEnumerable<Filme> GetFilmeEEFunctionsByTitulo(string titulo, Context context)
        {
            return context?.Filmes?
                           .Where(filme =>
                                 EF.Functions.Like(filme.Titulo, $"%{titulo}%"))
                           .Include(filme => filme.Diretores).ToList();
        }

        public static IEnumerable<Filme> GetFilmeContainsTitulo(string titulo, Context context)
        {
            return context.Filmes
                          .Where(filme => filme.Titulo.Contains(titulo))
                          .Include(filme => filme.Diretores).ToList();
        }

        public static IEnumerable<Filme>? GetFilmeByName(string titulo, Context context)
        {
            return context?.Filmes?
                           .Include(filme => filme.Diretores)
                           .Where(filme => filme.Titulo.Contains(titulo))
                           .ToList();
        }

        public static void AddFilme(Context context, Filme filme)
        {
            context.Filmes?.Add(filme);
            context.SaveChanges();
        }

        public static void ExecuteDeleteFilme(Context context, int filmeId)
        {
            // var filme = new Filme { Id = filmeId, Titulo = "xpto" };  // Crinado um objeto Fack
            context.Filmes
                   .Where(filme => filme.Id == filmeId)
                   .ExecuteDelete<Filme>();  // Deleta todos os filmes que obedecem a condição e retorna  a quantidade que foi deletada

        }

        public static IResult UpdateFilme(Context context, FilmeUpdate filmeUpdate)
        {
            var filme = context.Filmes.Find(filmeUpdate.Id);
            if (filme == null)
                return Results.Ok("Filme não encontrado");

            filme.Titulo = filmeUpdate.Titulo;
            filme.Ano = filmeUpdate.Ano;

            context.Filmes.Update(filme);
            context.SaveChanges();

            return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com Sucesso");

        }

        public static IResult ExecuteUpdateFilme(Context context, FilmeUpdate filmeUpdate)
        {
            // var filme = new Filme { Id = filmeId, Titulo = "xpto" };  // Crinado um objeto Fack
            var linhasAfetadas = context.Filmes
                    .Where(filme => filme.Id == filmeUpdate.Id)
                    .ExecuteUpdate(setter => setter
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
