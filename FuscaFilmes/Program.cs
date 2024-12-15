using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using FuscaFilmes.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Criando o Banco de dados
/**
using (var context = new Context())
{
    context.Database.EnsureCreated();
}
*/

builder.Services.AddDbContext<Context>(options =>
            options.UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmeStr"])
            .LogTo(Console.WriteLine, LogLevel.Information) //Adicionando os logs tem varios LogLevel.(vários)
);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Retirando o Cicle do Json 
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;  // Virgula no Final
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Referência circular
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Verbos do Http

#region Diretores

// Include, ToList
app.MapGet("/diretores", (Context context) =>
{
    return context?.Diretores?
                   .Include(diretor => diretor.Filmes)
                   .ToList();
}).WithOpenApi();

// Where == ToList
app.MapGet("/diretores/{id}", (int id, Context context) =>
{
    return context?.Diretores?
                    .Include(diretor => diretor.Filmes)
                    .Where(diretor => diretor.Id == id)
                    .ToList();
}).WithOpenApi();

// Include, OrderBy, OrderByDescending,FirstOrDefault
app.MapGet("/diretores/agregacao/{name}", (string name, Context context) =>
{
    return context?.Diretores?
                    .Include(diretor => diretor.Filmes)
                    // .OrderBy(diretor => diretor.Name)                 // Retorna o Primeiro por ordem alfabetica
                    // .OrderByDescending(diretor => diretor.Name)       // Retorna o Ultima por ordem alfabetica
                    // .FirstOrDefault();                                // Retorna o Primeio da lista      
                    // .LastOrDefault(); // Precisa do Orderby           // Retorna o Ultimo da lista
                    // .Select(diretor => diretor.Name);                 // Seleciona somente o Nome dos diretores
                    .FirstOrDefault(diretor => diretor.Name.Contains(name)) ?? new Diretor { Id = 99999, Name = "Marina" }; // Retorna Maria se não encontrar


}).WithOpenApi();

// Where == ToList
app.MapGet("/diretores/where/{id}", (int id, Context context) =>
{
    return context?.Diretores?
                    .Where(diretor => diretor.Id == id)
                    .Include(diretor => diretor.Filmes)
                    .ToList();
}).WithOpenApi();

// Add, SaveChanges
app.MapPost("/diretores", (Context context, Diretor diretor) =>
{
    context.Diretores?.Add(diretor);
    context.SaveChanges();

}).WithOpenApi();

// Find, Clear, Add, Update, SaveChanges
app.MapPut("/diretores/{diretorId}", (Context context, int diretorId, Diretor diretorNovo) =>
{

    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
    {
        diretor.Name = diretorNovo.Name;
        if (diretorNovo.Filmes.Count > 0)
        {
            // Removendo todos os filmes para adicinar os novos
            diretor.Filmes.Clear();
            foreach (var filme in diretorNovo.Filmes)
            {
                // Adicionando todos os filmes novos
                diretor.Filmes.Add(filme);
            }
        }

        context.Diretores?.Update(diretor);
    }

    context.SaveChanges();

}).WithOpenApi();

// Find, Remove, SaveChanges
app.MapDelete("/diretores/{diretorId}", (Context context, int diretorId) =>
{
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
        context.Diretores?.Remove(diretor);

    context.SaveChanges(); //Retorna true/false

}).WithOpenApi();

#endregion


#region Filmes

// Include , Order by, ToList 
app.MapGet("/filmes", (Context context) =>
{
    return context?.Filmes?
            .Include(filme => filme.Diretor)
            //.OrderBy(filme => filme.Ano)
            .OrderByDescending(filme => filme.Ano) // Primeira ordernação por Ano
            .ThenByDescending(filme => filme.Titulo) // Segunda ordenação por titulo 
            .ToList();
}).WithOpenApi();

// Where, == , Include , ToList
app.MapGet("/filmes/byId/{id}", (int id, Context context) =>
{
    return context?.Filmes?
                    .Where(filme => filme.Id == id)
                    .Include(filme => filme.Diretor).ToList();
}).WithOpenApi();

// Where e Contains Include ToList
app.MapGet("/filmes/byName/{tiutlo}", (string titulo, Context context) =>
{
    return context?.Filmes?
                    .Where(filme => filme.Titulo.Contains(titulo))
                    .Include(filme => filme.Diretor).ToList();
}).WithOpenApi();

// Where ,Function.Like,Include
app.MapGet("/filmes/EFFunction/byNames/{tiutlo}", (string titulo, Context context) =>
{
    return context?.Filmes?
                    .Where(filme =>
                    EF.Functions.Like(filme.Titulo, $"%{titulo}%"))
                   .Include(filme => filme.Diretor).ToList();
}).WithOpenApi();

// Add, SaveChanges
app.MapPost("/filmes", (Context context, Filme filme) =>
{
    context.Filmes?.Add(filme);
    context.SaveChanges();

}).WithOpenApi();

// Where, ExecuteDelete
app.MapDelete("/filmes/{filmeId}", (Context context, int filmeId) =>
{
    // var filme = new Filme { Id = filmeId, Titulo = "xpto" };  // Crinado um objeto Fack
    context.Filmes
           .Where(filme => filme.Id == filmeId)
           .ExecuteDelete<Filme>();  // Deleta todos os filmes que obedecem a condição e retorna  a quantidade que foi deletada


}).WithOpenApi();


// Patch Alterando partes do filme , Where, ExecuteDelete
app.MapPatch("/filmesUpdate", (Context context, FilmeUpdate filmeUpdate) =>
{
    var filme = context.Filmes.Find(filmeUpdate.Id);
    if (filme == null)
        return Results.Ok("Filme não encontrado");
    filme.Titulo = filmeUpdate.Titulo;
    filme.Ano = filmeUpdate.Ano;

    context.Filmes.Update(filme);
    context.SaveChanges();

    return Results.Ok($"Filme com ID {filmeUpdate.Id} atualizado com Sucesso");

}).WithOpenApi();


// Patch Alterando partes do filme , Where, ExecuteUpdate SetProperty
app.MapPatch("/filmesExecuteUpdate", (Context context, FilmeUpdate filmeUpdate) =>
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
}).WithOpenApi();


#endregion
app.Run();
