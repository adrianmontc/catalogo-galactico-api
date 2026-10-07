using ApiCatalogo_Galactico.Models;

namespace ApiCatalogo_Galactico.Data;
public static class GalaxiaData
{
    public static List<Personaje> Personajes { get; } =
    [
        new() { Id = 1, Nombre = "Luke Skywalker", Especie = "Humano", Faccion = "Rebelde", Afiliacion = "Alianza Rebelde", Estado = "vivo", FuerzaSensitivo = true, ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSHZrq-q9nN8oj94BvtOqNvXNVJPdZt4EQq2ctvpHeGEQ&s=10" },
        new() { Id = 2, Nombre = "Darth Vader", Especie = "Humano", Faccion = "Imperio", Afiliacion = "Imperio Galáctico", Estado = "muerto", FuerzaSensitivo = true, AnioMuerte = 4 ,ImagenUrl = "https://thumb.wikimedia.org/wikipedia/commons/thumb/0/04/Darth_Vader_at_Galaxy%E2%80%99s_Edge_%28cropped%29.jpg/330px-Darth_Vader_at_Galaxy%E2%80%99s_Edge_%28cropped%29.jpg?utm_source=es.wikipedia.org&utm_campaign=parser&utm_content=thumbnail"},
        new() { Id = 3, Nombre = "Leia Organa", Especie = "Humano", Faccion = "Rebelde", Afiliacion = "Alianza Rebelde", Estado = "vivo", FuerzaSensitivo = true , ImagenUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d4/SWC4_-_Costume_Pageant-_Princess_Leia_%28514396508%29.jpg?utm_source=es.wikipedia.org&utm_campaign=index&utm_content=original"},
        new() { Id = 4, Nombre = "Han Solo", Especie = "Humano", Faccion = "Rebelde", Afiliacion = "Alianza Rebelde", Estado = "muerto", FuerzaSensitivo = false, AnioMuerte = 34 ,ImagenUrl = "https://static.wikia.nocookie.net/charactercommunity/images/a/ac/Han_Solo_render.png/revision/latest?cb=20210824161233"},
        new() { Id = 5, Nombre = "Chewbacca", Especie = "Wookiee", Faccion = "Rebelde", Afiliacion = "Alianza Rebelde", Estado = "vivo", FuerzaSensitivo = false,ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/f/f1/Chewbacca.png?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 6, Nombre = "Yoda", Especie = "Desconocida", Faccion = "Neutral", Afiliacion = "Orden Jedi", Estado = "muerto", FuerzaSensitivo = true, AnioMuerte = 4,ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/9/9b/Yoda_Empire_Strikes_Back.png?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 7, Nombre = "Obi-Wan Kenobi", Especie = "Humano", Faccion = "Rebelde", Afiliacion = "Orden Jedi", Estado = "muerto", FuerzaSensitivo = true, AnioMuerte = 0,ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/3/32/Ben_Kenobi.png?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 8, Nombre = "Palpatine", Especie = "Humano", Faccion = "Imperio", Afiliacion = "Imperio Galáctico", Estado = "muerto", FuerzaSensitivo = true, AnioMuerte = 4 , ImagenUrl = "https://static.wikia.nocookie.net/battlefront/images/5/56/Battlefront_Palpatine.jpg/revision/latest?cb=20190720212547"},
        new() { Id = 9, Nombre = "Boba Fett", Especie = "Humano", Faccion = "Neutral", Afiliacion = "Cazarrecompensas", Estado = "vivo", FuerzaSensitivo = false , ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR3Zrwg4jd3LjdvdwxWylA75zcTokIIGHm_bF9mw9hoWQ&s=10"},
        new() { Id = 10, Nombre = "Ahsoka Tano", Especie = "Togruta", Faccion = "Neutral", Afiliacion = "Orden Jedi", Estado = "vivo", FuerzaSensitivo = true , ImagenUrl = "https://static.wikia.nocookie.net/esstarwars/images/2/27/Ahsoka-Tano-AG-2023.png/revision/latest?cb=20230823075054"}
    ];

    public static List<CardPersonaje> Cards { get; } =
    [
        new() { Id = 1, PersonajeId = 1, Poder = 88, HabilidadEspecial = "Dominio de la Fuerza", Arma = "Sable de luz", NivelPeligrosidad = 8, ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSHZrq-q9nN8oj94BvtOqNvXNVJPdZt4EQq2ctvpHeGEQ&s=10" },
        new() { Id = 2, PersonajeId = 2, Poder = 98, HabilidadEspecial = "Asfixia de la Fuerza", Arma = "Sable de luz rojo", NivelPeligrosidad = 10, ImagenUrl = "https://thumb.wikimedia.org/wikipedia/commons/thumb/0/04/Darth_Vader_at_Galaxy%E2%80%99s_Edge_%28cropped%29.jpg/330px-Darth_Vader_at_Galaxy%E2%80%99s_Edge_%28cropped%29.jpg?utm_source=es.wikipedia.org&utm_campaign=parser&utm_content=thumbnail" },
        new() { Id = 3, PersonajeId = 3, Poder = 70, HabilidadEspecial = "Liderazgo estratégico", Arma = "Bláster", NivelPeligrosidad = 7, ImagenUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d4/SWC4_-_Costume_Pageant-_Princess_Leia_%28514396508%29.jpg?utm_source=es.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 4, PersonajeId = 4, Poder = 68, HabilidadEspecial = "Estrategia y pilotaje", Arma = "Bláster DL-44", NivelPeligrosidad = 7, ImagenUrl = "https://static.wikia.nocookie.net/charactercommunity/images/a/ac/Han_Solo_render.png/revision/latest?cb=20210824161233" },
        new() { Id = 5, PersonajeId = 5, Poder = 75, HabilidadEspecial = "Fuerza física", Arma = "Ballesta láser", NivelPeligrosidad = 8, ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/f/f1/Chewbacca.png?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 6, PersonajeId = 6, Poder = 95, HabilidadEspecial = "Maestría Jedi", Arma = "Sable de luz verde", NivelPeligrosidad = 9, ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/9/9b/Yoda_Empire_Strikes_Back.png?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 7, PersonajeId = 7, Poder = 91, HabilidadEspecial = "Maestría Jedi", Arma = "Sable de luz azul", NivelPeligrosidad = 9, ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/3/32/Ben_Kenobi.png?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original" },
        new() { Id = 8, PersonajeId = 8, Poder = 100, HabilidadEspecial = "Relámpagos de la Fuerza", Arma = "Sable de luz rojo", NivelPeligrosidad = 10, ImagenUrl = "https://static.wikia.nocookie.net/battlefront/images/5/56/Battlefront_Palpatine.jpg/revision/latest?cb=20190720212547" },
        new() { Id = 9, PersonajeId = 9, Poder = 82, HabilidadEspecial = "Arsenal mandaloriano", Arma = "Bláster EE-3", NivelPeligrosidad = 9, ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR3Zrwg4jd3LjdvdwxWylA75zcTokIIGHm_bF9mw9hoWQ&s=10" },
        new() { Id = 10, PersonajeId = 10, Poder = 89, HabilidadEspecial = "Combate acrobático", Arma = "Dos sables de luz", NivelPeligrosidad = 8, ImagenUrl = "https://static.wikia.nocookie.net/esstarwars/images/2/27/Ahsoka-Tano-AG-2023.png/revision/latest?cb=20230823075054" }
    ];

    public static List<Evento> Eventos { get; } =
    [
        new()
        {
            Id = 1,
            Nombre = "Batalla de Yavin",
            Anio = 0,
            Ubicacion = "Yavin 4",
            Descripcion = "La Alianza Rebelde ataca la Estrella de la Muerte.",
            Participantes = [1, 2, 3, 4, 5],
            Muertos = [],
            Resultado = "La Estrella de la Muerte fue destruida.",
            Ganador = "Rebelde"
        },
        new()
        {
            Id = 2,
            Nombre = "Batalla de Hoth",
            Anio = 3,
            Ubicacion = "Hoth",
            Descripcion = "El Imperio ataca la base Echo de la Alianza Rebelde.",
            Participantes = [1, 2, 3, 4, 5],
            Muertos = [],
            Resultado = "La Alianza evacuó Hoth.",
            Ganador = "Imperio"
        },
        new()
        {
            Id = 3,
            Nombre = "Batalla de Endor",
            Anio = 4,
            Ubicacion = "Endor",
            Descripcion = "La Alianza enfrenta al Imperio en la batalla final de la segunda Estrella de la Muerte.",
            Participantes = [1, 2, 3, 5, 8],
            Muertos = [2, 8],
            Resultado = "La segunda Estrella de la Muerte fue destruida y el Emperador cayó.",
            Ganador = "Rebelde"
        },
        new()
        {
            Id = 4,
            Nombre = "Batalla de Mustafar",
            Anio = -19,
            Ubicacion = "Mustafar",
            Descripcion = "Anakin Skywalker se enfrenta a Obi-Wan Kenobi.",
            Participantes = [2, 7],
            Muertos = [],
            Resultado = "Obi-Wan derrota a Anakin y lo deja gravemente herido.",
            Ganador = "Rebelde"
        },
        new()
        {
            Id = 5,
            Nombre = "Batalla de Geonosis",
            Anio = -22,
            Ubicacion = "Geonosis",
            Descripcion = "Los Jedi y sus aliados combaten al ejército separatista.",
            Participantes = [7, 6],
            Muertos = [],
            Resultado = "La República consigue sobrevivir al ataque.",
            Ganador = "Rebelde"
        }
    ];

    public static int SiguienteId<T>(IEnumerable<T> elementos, Func<T, int> selector) =>
        elementos.Select(selector).DefaultIfEmpty(0).Max() + 1;
}