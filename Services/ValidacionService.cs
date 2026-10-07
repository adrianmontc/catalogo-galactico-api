namespace ApiCatalogo_Galactico.Services;

public static class ValidacionService
{
    public static readonly string[] Facciones = ["Rebelde", "Imperio", "Neutral"];
    public static readonly string[] Estados = ["vivo", "muerto", "desconocido"];

    public static string? ValidarPersonaje(string nombre,string especie,string faccion,string afiliacion,string estado,int? anioMuerte,string imagenUrl)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return "El nombre es obligatorio.";

        if (string.IsNullOrWhiteSpace(especie))
            return "La especie es obligatoria.";

        if (!Facciones.Contains(faccion))
            return "La facción debe ser Rebelde, Imperio o Neutral.";

        if (string.IsNullOrWhiteSpace(afiliacion))
            return "La afiliación es obligatoria.";

        if (!Estados.Contains(estado))
            return "El estado debe ser vivo, muerto o desconocido.";

        if (estado == "muerto" && anioMuerte is null)
            return "Un personaje muerto debe tener anio de muerte.";

        if (estado != "muerto" && anioMuerte is not null)
            return "Anio de muerte solo puede existir cuando el estado es muerto.";

        if (string.IsNullOrWhiteSpace(imagenUrl))
            return "La URL de la imagen es obligatoria.";

        return null;
    }

    public static string? ValidarCard(
        int personajeId,
        int poder,
        string habilidadEspecial,
        string arma,
        int nivelPeligrosidad,
        string imagenUrl)
    {
        if (personajeId <= 0)
            return "PersonajeId debe ser mayor que 0.";

        if (poder < 1 || poder > 100)
            return "Poder debe estar entre 1 y 100.";

        if (string.IsNullOrWhiteSpace(habilidadEspecial))
            return "La habilidad especial es obligatoria.";

        if (string.IsNullOrWhiteSpace(arma))
            return "El arma es obligatoria.";

        if (nivelPeligrosidad < 1 || nivelPeligrosidad > 10)
            return "NivelPeligrosidad debe estar entre 1 y 10.";

        if (string.IsNullOrWhiteSpace(imagenUrl))
            return "ImagenUrl es obligatoria.";

        return null;
    }

    public static string? ValidarEvento(string nombre, string ubicacion, string descripcion, int anio)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return "El nombre del evento es obligatorio.";

        if (string.IsNullOrWhiteSpace(ubicacion))
            return "La ubicación es obligatoria.";

        if (string.IsNullOrWhiteSpace(descripcion))
            return "La descripción es obligatoria.";

        if (anio < -50000 || anio > 50000)
            return "El año está fuera del rango permitido.";

        return null;
    }
}