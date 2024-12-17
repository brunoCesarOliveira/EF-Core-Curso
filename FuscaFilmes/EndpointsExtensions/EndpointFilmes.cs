
using FuscaFilmes.EndPointHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace FuscaFilmes.EndpointsExtensions
{
    public static class EndpointFilmes
    {
        public static void FilmesEndpoints(this IEndpointRouteBuilder app)
        {
            /*

            app.MapGet("/filmes/byId/{id}", FilmesHandlers.GetFilmeById).WithOpenApi();

            app.MapGet("/filmes/byName/{tiutlo}", FilmesHandlers.GetFilmeByName).WithOpenApi();

            app.MapGet("/filmes/EFFunction/byNames/{tiutlo}", FilmesHandlers.GetFilmeEEFunctionsByTitulo).WithOpenApi();

            app.MapGet("/filmes/EFFunction/byNames/{tiutlo}", FilmesHandlers.GetFilmeContainsTitulo).WithOpenApi();

            app.MapPost("/filmes", FilmesHandlers.AddFilme).WithOpenApi();

            app.MapDelete("/filmes/{filmeId}", FilmesHandlers.ExecuteDeleteFilme).WithOpenApi();

            app.MapPatch("/filmesUpdate", FilmesHandlers.UpdateFilme).WithOpenApi();

            app.MapPatch("/filmesExecuteUpdate", FilmesHandlers.ExecuteUpdateFilme).WithOpenApi();
            */

        }
    }
}
