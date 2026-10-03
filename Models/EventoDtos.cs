namespace ApiCatalogo_Galactico.Models;

public record CrearEventoDto(
    string Nombre,
    int Anio,
    string Ubicacion,
    string Descripcion,
    List<int> Participantes,
    List<int> Muertos,
    string? Resultado,
    string? Ganador
);

public record ActualizarEventoDto(
    string Nombre,
    int Anio,
    string Ubicacion,
    string Descripcion,
    List<int> Participantes,
    List<int> Muertos,
    string? Resultado,
    string? Ganador
);