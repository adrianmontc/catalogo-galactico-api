using ApiCatalogo_Galactico.Models;
using ApiCatalogo_Galactico.Services;

namespace ApiCatalogo_Galactico.Endpoints;

public static class EventoEndpoints
{
    public static void MapEventoEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/eventos")
            .WithTags("Eventos");

        group.MapGet("/", (int? anioDesde, int? anioHasta, EventoService service) =>
        {
            if (anioDesde.HasValue && anioHasta.HasValue && anioDesde > anioHasta)
                return Results.BadRequest("anioDesde no puede ser mayor que anioHasta.");

            return Results.Ok(service.ObtenerTodos(anioDesde, anioHasta));
        })
        .WithName("GetEventos")
        .WithSummary("Lista eventos con filtros opcionales por año")
        .Produces<IEnumerable<Evento>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/{id:int}", (int id, EventoService service) =>
        {
            var evento = service.Obtener(id);
            return evento is null ? Results.NotFound("Evento no encontrado.") : Results.Ok(evento);
        })
        .WithName("GetEvento")
        .WithSummary("Obtiene un evento por ID")
        .Produces<Evento>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{id:int}/participantes", (int id, EventoService service) =>
        {
            if (service.Obtener(id) is null)
                return Results.NotFound("Evento no encontrado.");

            return Results.Ok(service.ObtenerParticipantes(id));
        })
        .WithName("GetParticipantesEvento")
        .WithSummary("Lista los participantes de un evento")
        .Produces<IEnumerable<Personaje>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}