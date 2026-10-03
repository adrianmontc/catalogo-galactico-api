namespace ApiCatalogo_Galactico.Models;

public record ResultadoSimulacion(
    int EventoId,
    string Evento,
    List<ParticipanteSimulacion> Participantes,
    Dictionary<string, int> PoderPorFaccion,
    Dictionary<string, double> PoderAjustadoPorFaccion,
    string Ganador,
    double FactorAleatorio,
    string Criterio
);

public record ParticipanteSimulacion(
    int PersonajeId,
    string Personaje,
    string Faccion,
    int Poder
);

public record RankingPersonaje(
    int PersonajeId,
    string Personaje,
    string Faccion,
    int Poder,
    int NivelPeligrosidad
);

public record MvpEvento(
    int EventoId,
    string Evento,
    int PersonajeId,
    string Personaje,
    string Faccion,
    int Poder,
    string HabilidadEspecial
);