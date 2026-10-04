using ApiCatalogo_Galactico.Data;
using ApiCatalogo_Galactico.Models;

namespace ApiCatalogo_Galactico.Services;

public class CardService
{
    public IEnumerable<CardPersonaje> ObtenerTodas() =>
        GalaxiaData.Cards.OrderByDescending(c => c.Poder);

    public CardPersonaje? Obtener(int id) =>
        GalaxiaData.Cards.FirstOrDefault(c => c.Id == id);

    public CardPersonaje? ObtenerPorPersonaje(int personajeId) =>
        GalaxiaData.Cards.FirstOrDefault(c => c.PersonajeId == personajeId);

    public string? Crear(CrearCardDto dto, out CardPersonaje? card)
    {
        card = null;

        var error = ValidacionService.ValidarCard(
            dto.PersonajeId,
            dto.Poder,
            dto.HabilidadEspecial,
            dto.Arma,
            dto.NivelPeligrosidad,
            dto.ImagenUrl);

        if (error is not null)
            return error;

        if (!GalaxiaData.Personajes.Any(p => p.Id == dto.PersonajeId))
            return "El personaje indicado no existe.";

        if (GalaxiaData.Cards.Any(c => c.PersonajeId == dto.PersonajeId))
            return "El personaje ya tiene una carta asociada.";

        card = new CardPersonaje
        {
            Id = GalaxiaData.SiguienteId(GalaxiaData.Cards, c => c.Id),
            PersonajeId = dto.PersonajeId,
            Poder = dto.Poder,
            HabilidadEspecial = dto.HabilidadEspecial.Trim(),
            Arma = dto.Arma.Trim(),
            NivelPeligrosidad = dto.NivelPeligrosidad,
            ImagenUrl = dto.ImagenUrl.Trim()
        };

        GalaxiaData.Cards.Add(card);
        return null;
    }

    public string? Actualizar(int id, ActualizarCardDto dto)
    {
        var card = Obtener(id);

        if (card is null)
            return "NO_ENCONTRADO";

        var error = ValidacionService.ValidarCard(
            dto.PersonajeId,
            dto.Poder,
            dto.HabilidadEspecial,
            dto.Arma,
            dto.NivelPeligrosidad,
            dto.ImagenUrl);

        if (error is not null)
            return error;

        if (!GalaxiaData.Personajes.Any(p => p.Id == dto.PersonajeId))
            return "El personaje indicado no existe.";

        if (GalaxiaData.Cards.Any(c => c.PersonajeId == dto.PersonajeId && c.Id != id))
            return "El personaje ya tiene otra carta asociada.";

        card.PersonajeId = dto.PersonajeId;
        card.Poder = dto.Poder;
        card.HabilidadEspecial = dto.HabilidadEspecial.Trim();
        card.Arma = dto.Arma.Trim();
        card.NivelPeligrosidad = dto.NivelPeligrosidad;
        card.ImagenUrl = dto.ImagenUrl.Trim();

        return null;
    }

    public IEnumerable<RankingPersonaje> Ranking(int limite)
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
            .OrderByDescending(r => r.Poder)
            .Take(limite);
    }

    public IEnumerable<CardPersonaje> MasPoderosas(int limite) =>
        GalaxiaData.Cards
            .OrderByDescending(c => c.Poder)
            .ThenByDescending(c => c.NivelPeligrosidad)
            .Take(limite);
}