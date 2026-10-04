using ApiCatalogo_Galactico.Endpoints;
using ApiCatalogo_Galactico.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Catalogo galactico de personajes y eventos",
        Version = "v1",
        Description = @"Una API REST hecha con Minimal APIs.

Este sistema permite la administracion de:
* **Personajes:** Registro de entidades, facciones y estados vitales.
* **Cartas Coleccionables:** Asignacion de atributos de combate, armas y peligrosidad.
* **Eventos:** Historial cruzado con validacion temporal cronologica (ABY/BBY).

Ademas, incluye logica de negocio avanzada para la simulacion de batallas, calculo de MVP por evento y generacion de rankings estadisticos."
    });
});

builder.Services.AddSingleton<PersonajeService>();
builder.Services.AddSingleton<CardService>();
builder.Services.AddSingleton<EventoService>();
builder.Services.AddSingleton<EstadisticaService>();
builder.Services.AddSingleton<SimulacionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalogo galactico API v1");
        options.RoutePrefix = "swagger";
    });
}

app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription();

var api = app.MapGroup("/api");

api.MapPersonajeEndpoints();
api.MapCardEndpoints();
api.MapEventoEndpoints();

app.Run();