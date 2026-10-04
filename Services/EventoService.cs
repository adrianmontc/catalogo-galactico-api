using ApiCatalogo_Galactico.Data;
using ApiCatalogo_Galactico.Models;

namespace ApiCatalogo_Galactico.Services;
public class EventoService
{
    private readonly PersonajeService _personajeService;

    public EventoService(PersonajeService personajeService)
    {
        _personajeService = personajeService;
    }

    public IEnumerable<Evento> ObtenerTodos(int? anioDesde, int? anioHasta)
    {
        IEnumerable<Evento> resultado = GalaxiaData.Eventos;

        if (anioDesde.HasValue)
            resultado = resultado.Where(e => e.Anio >= anioDesde.Value);

        if (anioHasta.HasValue)
            resultado = resultado.Where(e => e.Anio <= anioHasta.Value);

        return resultado.OrderBy(e => e.Anio).ThenBy(e => e.Id);
    }

    public Evento? Obtener(int id) =>
        GalaxiaData.Eventos.FirstOrDefault(e => e.Id == id);

    public string? ValidarParticipantes(List<int> participantes, int anio, List<int> muertos)
    {
        if (participantes.Count < 2)
            return "Un evento debe tener al menos 2 participantes.";

        if (participantes.Distinct().Count() != participantes.Count)
            return "No se permiten participantes repetidos.";

        if (muertos.Distinct().Count() != muertos.Count)
            return "No se permiten personajes muertos repetidos.";

        if (muertos.Any(id => !participantes.Contains(id)))
            return "Todo personaje incluido en Muertos debe ser participante del evento.";

        foreach (var personajeId in participantes)
        {
            var error = _personajeService.ValidarParticipacionTemporal(personajeId, anio);

            if (error is not null)
                return error;
        }

        return null;
    }

    public string? Crear(CrearEventoDto dto, out Evento? evento)
    {
        evento = null;

        var error = ValidacionService.ValidarEvento(
            dto.Nombre,
            dto.Ubicacion,
            dto.Descripcion,
            dto.Anio);

        if (error is not null)
            return error;

        error = ValidarParticipantes(dto.Participantes, dto.Anio, dto.Muertos);

        if (error is not null)
            return error;

        foreach (var personajeId in dto.Muertos)
        {
            var personaje = _personajeService.Obtener(personajeId);

            if (personaje is null)
                return "Uno de los personajes indicados no existe.";

            if (personaje.Estado == "muerto" &&
                personaje.AnioMuerte.HasValue &&
                personaje.AnioMuerte.Value < dto.Anio)
            {
                return $"El personaje {personaje.Nombre} ya había muerto antes de este evento.";
            }
        }

        evento = new Evento
        {
            Id = GalaxiaData.SiguienteId(GalaxiaData.Eventos, e => e.Id),
            Nombre = dto.Nombre.Trim(),
            Anio = dto.Anio,
            Ubicacion = dto.Ubicacion.Trim(),
            Descripcion = dto.Descripcion.Trim(),
            Participantes = dto.Participantes.Distinct().ToList(),
            Muertos = dto.Muertos.Distinct().ToList(),
            Resultado = string.IsNullOrWhiteSpace(dto.Resultado) ? null : dto.Resultado.Trim(),
            Ganador = string.IsNullOrWhiteSpace(dto.Ganador) ? null : dto.Ganador.Trim()
        };

        GalaxiaData.Eventos.Add(evento);

        foreach (var personajeId in evento.Muertos)
            _personajeService.RegistrarMuerte(personajeId, evento.Anio);

        return null;
    }

    public string? Actualizar(int id, ActualizarEventoDto dto)
    {
        var evento = Obtener(id);

        if (evento is null)
            return "NO_ENCONTRADO";

        var error = ValidacionService.ValidarEvento(
            dto.Nombre,
            dto.Ubicacion,
            dto.Descripcion,
            dto.Anio);

        if (error is not null)
            return error;

        error = ValidarParticipantes(dto.Participantes, dto.Anio, dto.Muertos);

        if (error is not null)
            return error;

        foreach (var personajeId in dto.Muertos)
        {
            var personaje = _personajeService.Obtener(personajeId);

            if (personaje is null)
                return "Uno de los personajes indicados no existe.";

            if (personaje.Estado == "muerto" &&
                personaje.AnioMuerte.HasValue &&
                personaje.AnioMuerte.Value < dto.Anio &&
                personaje.AnioMuerte.Value != evento.Anio)
            {
                return $"El personaje {personaje.Nombre} ya había muerto antes de este evento.";
            }
        }

        evento.Nombre = dto.Nombre.Trim();
        evento.Anio = dto.Anio;
        evento.Ubicacion = dto.Ubicacion.Trim();
        evento.Descripcion = dto.Descripcion.Trim();
        evento.Participantes = dto.Participantes.Distinct().ToList();
        evento.Muertos = dto.Muertos.Distinct().ToList();
        evento.Resultado = string.IsNullOrWhiteSpace(dto.Resultado) ? null : dto.Resultado.Trim();
        evento.Ganador = string.IsNullOrWhiteSpace(dto.Ganador) ? null : dto.Ganador.Trim();

        foreach (var personajeId in evento.Muertos)
            _personajeService.RegistrarMuerte(personajeId, evento.Anio);

        return null;
    }

    public IEnumerable<Personaje> ObtenerParticipantes(int id)
    {
        var evento = Obtener(id);

        if (evento is null)
            return [];

        return GalaxiaData.Personajes
            .Where(p => evento.Participantes.Contains(p.Id))
            .OrderBy(p => p.Id);
    }

    public IEnumerable<Evento> ObtenerEventosDePersonaje(int personajeId) =>
        GalaxiaData.Eventos
            .Where(e => e.Participantes.Contains(personajeId))
            .OrderBy(e => e.Anio);
}