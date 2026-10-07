namespace ApiCatalogo_Galactico.Models;

public record CrearPersonajeDto(string Nombre,string Especie,string Faccion,string Afiliacion,string Estado,bool FuerzaSensitivo,int? AnioMuerte, string ImagenUrl);

public record ActualizarPersonajeDto(string Nombre,string Especie,string Faccion,string Afiliacion,string Estado,bool FuerzaSensitivo,int? AnioMuerte, string ImagenUrl);