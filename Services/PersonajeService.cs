using ApiCatalogo_Galactico.Data;
using ApiCatalogo_Galactico.Models;

namespace ApiCatalogo_Galactico.Services;

public class PersonajeService
{
    public IEnumerable<Personaje> Filtrar(string? faccion, bool? fuerzaSensitivo, string? estado)
    {
        IEnumerable<Personaje> resultado = GalaxiaData.Personajes;

        if (!string.IsNullOrWhiteSpace(faccion))
            resultado = resultado.Where(p => p.Faccion == faccion);

        if (fuerzaSensitivo.HasValue)
            resultado = resultado.Where(p => p.FuerzaSensitivo == fuerzaSensitivo.Value);

        if (!string.IsNullOrWhiteSpace(estado))
            resultado = resultado.Where(p => p.Estado == estado);

        return resultado.OrderBy(p => p.Id);
    }

    public Personaje? Obtener(int id) =>
        GalaxiaData.Personajes.FirstOrDefault(p => p.Id == id);

    public string? Crear(CrearPersonajeDto dto, out Personaje? personaje)
    {
        personaje = null;

        var error = ValidacionService.ValidarPersonaje(
            dto.Nombre,
            dto.Especie,
            dto.Faccion,
            dto.Afiliacion,
            dto.Estado,
            dto.AnioMuerte,
            dto.ImagenUrl
            );

        if (error is not null)
            return error;

        personaje = new Personaje
        {
            Id = GalaxiaData.SiguienteId(GalaxiaData.Personajes, p => p.Id),
            Nombre = dto.Nombre.Trim(),
            Especie = dto.Especie.Trim(),
            Faccion = dto.Faccion,
            Afiliacion = dto.Afiliacion.Trim(),
            Estado = dto.Estado,
            FuerzaSensitivo = dto.FuerzaSensitivo,
            AnioMuerte = dto.AnioMuerte,
            ImagenUrl = dto.ImagenUrl.Trim()
        };

        GalaxiaData.Personajes.Add(personaje);
        return null;
    }

    public string? Actualizar(int id, ActualizarPersonajeDto dto)
    {
        var personaje = Obtener(id);

        if (personaje is null)
            return "NO_ENCONTRADO";

        var error = ValidacionService.ValidarPersonaje(
            dto.Nombre,
            dto.Especie,
            dto.Faccion,
            dto.Afiliacion,
            dto.Estado,
            dto.AnioMuerte,
            dto.ImagenUrl
            );

        if (error is not null)
            return error;

        personaje.Nombre = dto.Nombre.Trim();
        personaje.Especie = dto.Especie.Trim();
        personaje.Faccion = dto.Faccion;
        personaje.Afiliacion = dto.Afiliacion.Trim();
        personaje.Estado = dto.Estado;
        personaje.FuerzaSensitivo = dto.FuerzaSensitivo;
        personaje.AnioMuerte = dto.AnioMuerte;
        personaje.ImagenUrl = dto.ImagenUrl.Trim();

        return null;
    }

    public string? Eliminar(int id)
    {
        var personaje = Obtener(id);

        if (personaje is null)
            return "NO_ENCONTRADO";

        if (GalaxiaData.Cards.Any(c => c.PersonajeId == id))
            return "No se puede eliminar el personaje porque tiene una carta asociada.";

        if (GalaxiaData.Eventos.Any(e => e.Participantes.Contains(id)))
            return "No se puede eliminar el personaje porque participa en uno o más eventos.";

        GalaxiaData.Personajes.Remove(personaje);
        return null;
    }

    public string? ValidarParticipacionTemporal(int personajeId, int anioEvento)
    {
        var personaje = Obtener(personajeId);

        if (personaje is null)
            return "El personaje indicado no existe.";

        if (personaje.Estado == "muerto" &&
            personaje.AnioMuerte.HasValue &&
            anioEvento > personaje.AnioMuerte.Value)
        {
            return $"El personaje {personaje.Nombre} no puede participar en un evento posterior a su muerte en {personaje.AnioMuerte.Value}.";
        }

        return null;
    }

    public void RegistrarMuerte(int personajeId, int anioMuerte)
    {
        var personaje = Obtener(personajeId);

        if (personaje is null)
            return;

        personaje.Estado = "muerto";
        personaje.AnioMuerte = anioMuerte;
    }

    public PersonajeConCard? ObtenerConCard(int id)
    {
        var personaje = GalaxiaData.Personajes.FirstOrDefault(p => p.Id == id);

        if (personaje is null)
            return null;

        var card = GalaxiaData.Cards.FirstOrDefault(c => c.PersonajeId == id);

        if (card is null)
            return null;

        return new PersonajeConCard(personaje, card);
    }
}