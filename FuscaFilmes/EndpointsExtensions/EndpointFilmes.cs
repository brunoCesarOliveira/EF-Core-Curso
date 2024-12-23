
using FuscaFilmes.EndPointHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace FuscaFilmes.EndpointsExtensions
{
    public static class EndpointFilmes
    {
        public static void FilmesEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/filmes/byId/{id}", FilmesHandlers.GetFilmeByIdAsync).WithOpenApi();

            app.MapGet("/filmes/byName/{tiutlo}", FilmesHandlers.GetFilmeByNameAsync).WithOpenApi();

            app.MapGet("/filmes/EFFunction/byNames/{tiutlo}", FilmesHandlers.GetFilmeEEFunctionsByTituloAsync).WithOpenApi();

            app.MapPost("/filmes", FilmesHandlers.AddFilmeAsync).WithOpenApi();

            app.MapDelete("/filmes/{filmeId}", FilmesHandlers.ExecuteDeleteFilmeAsync).WithOpenApi();

            app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilme).WithOpenApi();

            app.MapPatch("/filmesExecuteUpdate", FilmesHandlers.ExecuteUpdateFilmeAsync).WithOpenApi();
        }
    }
}
