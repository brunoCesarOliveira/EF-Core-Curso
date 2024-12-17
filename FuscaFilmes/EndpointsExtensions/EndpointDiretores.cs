using FuscaFilmes.EndPointHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace FuscaFilmes.EndpointsExtensions
{
    public static class EndpointDiretores
    {
        // Extendendo a IEndpointRouteBuilder
        public static void DiretoresEndpoints(this IEndpointRouteBuilder app)
        {
            // Chamando a função vid delegagte
            app.MapGet("/diretores", DiretoresHandlers.GetDiretores).WithOpenApi();

            app.MapGet("/diretores/agregacao/{name}", DiretoresHandlers.GetDiretorByName).WithOpenApi();

            app.MapGet("/diretores/where/{id}", DiretoresHandlers.GetDiretorById).WithOpenApi();

            app.MapPost("/diretores", DiretoresHandlers.AddDiretor).WithOpenApi();

            app.MapPut("/diretores/{diretorId}", DiretoresHandlers.UpdateDiretor).WithOpenApi();

            app.MapDelete("/diretores/{diretorId}", DiretoresHandlers.DeleteDiretor).WithOpenApi();
        }
    }
}
