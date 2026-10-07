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
                return Results.BadRequest("La faccion debe ser Rebelde, Imperio o Neutral.");

            if (!string.IsNullOrWhiteSpace(estado) && !ValidacionService.Estados.Contains(estado))
                return Results.BadRequest("El estado debe ser vivo, muerto o desconocido.");

            return Results.Ok(service.Filtrar(faccion, fuerzaSensitivo, estado));
        })
        .WithName("GetPersonajes")
        .WithSummary("Lista personajes con filtros opcionales")
        .WithDescription("Permite combinar faccion, fuerzaSensitivo y estado.")
        .Produces<IEnumerable<Personaje>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/ranking", (string? por, int? limite, EstadisticaService service) =>
        {
            if (!string.Equals(por, "poder", StringComparison.OrdinalIgnoreCase))
                return Results.BadRequest("El parámetro 'por' debe ser 'poder'.");

            var cantidad = limite ?? 10;

            if (cantidad < 1 || cantidad > 50)
                return Results.BadRequest("El límite debe estar entre 1 y 50.");

            return Results.Ok(service.RankingPorPoder(cantidad));
        })
        .WithName("GetPersonajesRanking")
        .WithSummary("Ranking de personajes por poder")
        .Produces<IEnumerable<RankingPersonaje>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/{id:int}/con-card", (int id, PersonajeService service) =>
        {
            var resultado = service.ObtenerConCard(id);

            return resultado is null
                ? Results.NotFound("El personaje o su carta no existe.")
                : Results.Ok(resultado);
        })
        .WithName("GetPersonajeConCard")
        .WithSummary("Obtiene un personaje junto con su carta")
        .Produces<PersonajeConCard>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{id:int}", (int id, PersonajeService service) =>
        {
            var personaje = service.Obtener(id);
            return personaje is null ? Results.NotFound("Personaje no encontrado.") : Results.Ok(personaje);
        })
        .WithName("GetPersonaje")
        .WithSummary("Obtiene un personaje por ID")
        .Produces<Personaje>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{id:int}/carta", (int id, PersonajeService personajeService, CardService cardService) =>
        {
            if (personajeService.Obtener(id) is null)
                return Results.NotFound("Personaje no encontrado.");

            var card = cardService.ObtenerPorPersonaje(id);
            return card is null ? Results.NotFound("El personaje no tiene una carta asociada.") : Results.Ok(card);
        })
        .WithName("GetCartaDePersonaje")
        .WithSummary("Obtiene la carta principal de un personaje")
        .Produces<CardPersonaje>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{id:int}/eventos", (int id, PersonajeService personajeService, EventoService eventoService) =>
        {
            if (personajeService.Obtener(id) is null)
                return Results.NotFound("Personaje no encontrado.");

            return Results.Ok(eventoService.ObtenerEventosDePersonaje(id));
        })
        .WithName("GetEventosDePersonaje")
        .WithSummary("Lista los eventos en los que participó un personaje")
        .Produces<IEnumerable<Evento>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", (CrearPersonajeDto dto, PersonajeService service) =>
        {
            var error = service.Crear(dto, out var personaje);

            return error is null
                ? Results.Created($"/personajes/{personaje!.Id}", personaje)
                : Results.BadRequest(error);
        })
        .WithName("CreatePersonaje")
        .WithSummary("Crea un personaje")
        .Produces<Personaje>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:int}", (int id, ActualizarPersonajeDto dto, PersonajeService service) =>
        {
            var error = service.Actualizar(id, dto);

            if (error == "NO_ENCONTRADO")
                return Results.NotFound("Personaje no encontrado.");

            return error is null
                ? Results.Ok(service.Obtener(id))
                : Results.BadRequest(error);
        })
        .WithName("UpdatePersonaje")
        .WithSummary("Actualiza un personaje")
        .Produces<Personaje>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:int}", (int id, PersonajeService service) =>
        {
            var error = service.Eliminar(id);

            if (error == "NO_ENCONTRADO")
                return Results.NotFound("Personaje no encontrado.");

            return error is null
                ? Results.NoContent()
                : Results.Conflict(error);
        })
        .WithName("DeletePersonaje")
        .WithSummary("Elimina un personaje sin relaciones")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict);
    }
}