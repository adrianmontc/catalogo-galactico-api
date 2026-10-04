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
        Description = "REST API en Minimal API para gestionar personajes, cartas y eventos de una galaxia"
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