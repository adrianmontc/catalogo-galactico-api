using ApiCatalogo_Galactico.Data;
using ApiCatalogo_Galactico.Models;
using ApiCatalogo_Galactico.Services;

namespace ApiCatalogo_Galactico.Endpoints;

public static class PersonajeEndpoints
{
    public static void MapPersonajeEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/personajes")
            .WithTags("Personajes");

        group.MapGet("/", (string? faccion, bool? fuerzaSensitivo, string? estado, PersonajeService service) =>
        {
            if (!string.IsNullOrWhiteSpace(faccion) && !ValidacionService.Facciones.Contains(faccion))
                return Results.BadRequest("La facción debe ser Rebelde, Imperio o Neutral.");

            if (!string.IsNullOrWhiteSpace(estado) && !ValidacionService.Estados.Contains(estado))
                return Results.BadRequest("El estado debe ser vivo, muerto o desconocido.");

            return Results.Ok(service.Filtrar(faccion, fuerzaSensitivo, estado));
        })
        .WithName("GetPersonajes")
        .WithSummary("Lista personajes con filtros opcionales")
        .WithDescription("Permite combinar faccion, fuerzaSensitivo y estado.")
        .Produces<IEnumerable<Personaje>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}