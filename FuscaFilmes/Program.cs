using FuscaFilme.Repo.Contrato;
using FuscaFilmes.Contexts;
using FuscaFilmes.EndpointsExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Criando o Banco de dados
/**
using (var context = new Context())
{
    context.Database.EnsureCreated();
}
*/

//Inje��o de dependencia do Context
builder.Services.AddDbContext<Context>(options =>
            options.UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmeStr"])
            .LogTo(Console.WriteLine, LogLevel.Information) //Adicionando os logs tem varios LogLevel.(v�rios)
);


// Fazendo a inje��o de depend�ncia
builder.Services.AddScoped<IDiretorRepository, DiretorRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Retirando o Cicle do Json 
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;  // Virgula no Final
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Refer�ncia circular
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// SOLID Principio Open-Closed - Principio Aberto e Fechado
// Extens�o do  IEndpointRouteBuilder  e colocando os endpoints de Diretor dentro
app.DiretoresEndpoints();
app.FilmesEndpoints();

app.Run();
