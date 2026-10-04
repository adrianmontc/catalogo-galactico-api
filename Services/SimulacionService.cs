using ApiCatalogo_Galactico.Data;
using ApiCatalogo_Galactico.Models;

namespace ApiCatalogo_Galactico.Services;

public class SimulacionService
{
    private readonly Random _random = new();

    public (ResultadoSimulacion? Resultado, string? Error) Simular(Evento evento)
    {
        var participantes = evento.Participantes
            .Select(id =>
            {
                var personaje = GalaxiaData.Personajes.FirstOrDefault(p => p.Id == id);
                var card = GalaxiaData.Cards.FirstOrDefault(c => c.PersonajeId == id);

                return new { personaje, card };
            })
            .ToList();

        if (participantes.Any(x => x.personaje is null))
            return (null, "El evento contiene un personaje que ya no existe.");

        if (participantes.Any(x => x.card is null))
            return (null, "No se puede simular: todos los participantes deben tener una carta.");

        var datos = participantes
            .Select(x => new ParticipanteSimulacion(
                x.personaje!.Id,
                x.personaje.Nombre,
                x.personaje.Faccion,
                x.card!.Poder))
            .ToList();

        var facciones = datos.Select(x => x.Faccion).Distinct().ToList();

        if (facciones.Count < 2)
            return (null, "La simulacion necesita participantes de al menos dos facciones diferentes.");

        var poderPorFaccion = datos
            .GroupBy(x => x.Faccion)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Poder));

        var factor = Math.Round(_random.NextDouble() * 0.20 + 0.90, 2);

        var poderAjustado = poderPorFaccion
            .ToDictionary(
                x => x.Key,
                x => Math.Round(x.Value * factor, 2));

        var ganador = poderAjustado
            .OrderByDescending(x => x.Value)
            .First()
            .Key;

        var criterio = $"Se sumo el poder de las cartas por faccion y se aplico un factor aleatorio acotado entre 0.90 y 1.10. La faccion con mayor poder ajustado gana: {ganador}.";

        return (
            new ResultadoSimulacion(
                evento.Id,
                evento.Nombre,
                datos,
                poderPorFaccion,
                poderAjustado,
                ganador,
                factor,
                criterio),
            null);
    }
}