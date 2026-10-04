using ApiCatalogo_Galactico.Models;
using ApiCatalogo_Galactico.Services;

namespace ApiCatalogo_Galactico.Endpoints;
public static class CardEndpoints
{
    public static void MapCardEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/cartas")
            .WithTags("Cartas");

        group.MapGet("/", (CardService service) =>
        {
            return Results.Ok(service.ObtenerTodas());
        })
        .WithName("GetCartas")
        .WithSummary("Lista todas las cartas ordenadas por poder")
        .Produces<IEnumerable<CardPersonaje>>(StatusCodes.Status200OK);

        group.MapGet("/mas-poderosas", (int? limite, CardService service) =>
        {
            var cantidad = limite ?? 5;

            if (cantidad < 1 || cantidad > 50)
                return Results.BadRequest("El limite debe estar entre 1 y 50.");

            return Results.Ok(service.MasPoderosas(cantidad));
        })
        .WithName("GetCartasMasPoderosas")
        .WithSummary("Obtiene las cartas con mayor poder")
        .Produces<IEnumerable<CardPersonaje>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/{id:int}", (int id, CardService service) =>
        {
            var card = service.Obtener(id);
            return card is null ? Results.NotFound("Carta no encontrada.") : Results.Ok(card);
        })
        .WithName("GetCarta")
        .WithSummary("Obtiene una carta por ID")
        .Produces<CardPersonaje>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", (CrearCardDto dto, CardService service) =>
        {
            var error = service.Crear(dto, out var card);

            if (error is null)
                return Results.Created($"/cartas/{card!.Id}", card);

            if (error == "El personaje indicado no existe.")
                return Results.NotFound(error);

            if (error == "El personaje ya tiene una carta asociada.")
                return Results.Conflict(error);

            return Results.BadRequest(error);
        })
        .WithName("CreateCarta")
        .WithSummary("Crea una carta para un personaje")
        .Produces<CardPersonaje>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict);

        group.MapPut("/{id:int}", (int id, ActualizarCardDto dto, CardService service) =>
        {
            var error = service.Actualizar(id, dto);

            if (error == "NO_ENCONTRADO")
                return Results.NotFound("Carta no encontrada.");

            if (error == "El personaje indicado no existe.")
                return Results.NotFound(error);

            if (error == "El personaje ya tiene otra carta asociada.")
                return Results.Conflict(error);

            return error is null
                ? Results.Ok(service.Obtener(id))
                : Results.BadRequest(error);
        })
        .WithName("UpdateCarta")
        .WithSummary("Actualiza una carta")
        .Produces<CardPersonaje>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict);
    }
}