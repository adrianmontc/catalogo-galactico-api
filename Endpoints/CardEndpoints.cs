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
                return Results.BadRequest("El límite debe estar entre 1 y 50.");

            return Results.Ok(service.MasPoderosas(cantidad));
        })
        .WithName("GetCartasMasPoderosas")
        .WithSummary("Obtiene las cartas con mayor poder")
        .Produces<IEnumerable<CardPersonaje>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}