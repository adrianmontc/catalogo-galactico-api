using ApiCatalogo_Galactico.Data;
using ApiCatalogo_Galactico.Models;

namespace ApiCatalogo_Galactico.Services;

public class EstadisticaService
{
    public IEnumerable<RankingPersonaje> RankingPorPoder(int limite)
    {
        return GalaxiaData.Cards
            .Join(
                GalaxiaData.Personajes,
                card => card.PersonajeId,
                personaje => personaje.Id,
                (card, personaje) => new RankingPersonaje(
                    personaje.Id,
                    personaje.Nombre,
                    personaje.Faccion,
                    card.Poder,
                    card.NivelPeligrosidad))
            .OrderByDescending(x => x.Poder)
            .ThenByDescending(x => x.NivelPeligrosidad)
            .Take(limite);
    }

    public MvpEvento? ObtenerMvp(Evento evento)
    {
        var mvp = GalaxiaData.Cards
            .Where(card => evento.Participantes.Contains(card.PersonajeId))
            .Join(
                GalaxiaData.Personajes,
                card => card.PersonajeId,
                personaje => personaje.Id,
                (card, personaje) => new MvpEvento(
                    evento.Id,
                    evento.Nombre,
                    personaje.Id,
                    personaje.Nombre,
                    personaje.Faccion,
                    card.Poder,
                    card.HabilidadEspecial))
            .OrderByDescending(x => x.Poder)
            .FirstOrDefault();

        return mvp;
    }
}