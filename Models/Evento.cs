namespace ApiCatalogo_Galactico.Models;
public class Evento
{
    public int Id { get; init; }
    public string Nombre { get; set; } = string.Empty;
    public int Anio { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public List<int> Participantes { get; set; } = [];
    public List<int> Muertos { get; set; } = [];
    public string? Resultado { get; set; }
    public string? Ganador { get; set; }
}
