using FuscaFilmes.DbContexts;
using FuscaFilmes.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Criando o Banco de dados
using (var context = new Context())
{
    context.Database.EnsureCreated();
}


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
//Verbos do Http
app.MapGet("/diretor", () =>
{
    using var context = new Context();
    return context?.Diretores?.Include(diretor => diretor.Filmes).ToList();
}).WithOpenApi();

app.MapPost("/diretor", (Diretor diretor) =>
{
    using var context = new Context();
    context.Diretores?.Add(diretor);
    context.SaveChanges();

}).WithOpenApi();

app.MapPut("/diretor/{diretorId}", (int diretorId, Diretor diretorNovo) =>
{
    using var context = new Context();
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

app.MapDelete("/diretor/{diretorId}", (int diretorId) =>
{
    using var context = new Context();
    var diretor = context.Diretores.Find(diretorId);

    if (diretor != null)
        context.Diretores?.Remove(diretor);

    context.SaveChanges();

}).WithOpenApi();

app.Run();
