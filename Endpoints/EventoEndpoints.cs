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

        group.MapGet("/{id:int}/mvp", (int id, EventoService eventoService, EstadisticaService estadisticaService) =>
        {
            var evento = eventoService.Obtener(id);

            if (evento is null)
                return Results.NotFound("Evento no encontrado.");

            var mvp = estadisticaService.ObtenerMvp(evento);

            return mvp is null
                ? Results.NotFound("No hay participantes con carta para calcular el MVP.")
                : Results.Ok(mvp);
        })
       .WithName("GetMvpEvento")
       .WithSummary("Obtiene el MVP del evento según el mayor poder")
       .Produces<MvpEvento>(StatusCodes.Status200OK)
       .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{id:int}/historial", (int id, EventoService service) =>
        {
            var evento = service.Obtener(id);

            if (evento is null)
                return Results.NotFound("Evento no encontrado.");

            var personajeIds = evento.Participantes;

            var historial = personajeIds
                .SelectMany(personajeId => service.ObtenerEventosDePersonaje(personajeId))
                .GroupBy(e => e.Id)
                .Select(g => g.First())
                .OrderBy(e => e.Anio);

            return Results.Ok(historial);
        })
        .WithName("GetHistorialParticipantes")
        .WithSummary("Obtiene el historial combinado de eventos de los participantes")
        .Produces<IEnumerable<Evento>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", (CrearEventoDto dto, EventoService service) =>
        {
            var error = service.Crear(dto, out var evento);

            return error is null
                ? Results.Created($"/eventos/{evento!.Id}", evento)
                : Results.BadRequest(error);
        })
        .WithName("CreateEvento")
        .WithSummary("Crea un evento y aplica las reglas temporales de participación")
        .Produces<Evento>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:int}", (int id, ActualizarEventoDto dto, EventoService service) =>
        {
            var error = service.Actualizar(id, dto);

            if (error == "NO_ENCONTRADO")
                return Results.NotFound("Evento no encontrado.");

            return error is null
                ? Results.Ok(service.Obtener(id))
                : Results.BadRequest(error);
        })
        .WithName("UpdateEvento")
        .WithSummary("Actualiza un evento y sus relaciones")
        .Produces<Evento>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/{id:int}/simular", (int id, EventoService eventoService, SimulacionService simulacionService) =>
        {
            var evento = eventoService.Obtener(id);

            if (evento is null)
                return Results.NotFound("Evento no encontrado.");

            var (resultado, error) = simulacionService.Simular(evento);

            return error is not null
                ? Results.BadRequest(error)
                : Results.Ok(resultado);
        })
        .WithName("SimularEvento")
        .WithSummary("Simula el resultado del evento")
        .WithDescription("Calcula poder por facción, aplica un factor aleatorio acotado y devuelve una explicación.")
        .Produces<ResultadoSimulacion>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}