namespace ApiCatalogo_Galactico.Models;

public record CrearCardDto(int PersonajeId,int Poder,string HabilidadEspecial,string Arma,int NivelPeligrosidad,string ImagenUrl);

public record ActualizarCardDto(int PersonajeId,int Poder,string HabilidadEspecial,string Arma,int NivelPeligrosidad,string ImagenUrl);