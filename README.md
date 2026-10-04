# Catalogo Galactico API

Proyecto desarrollado con C# y ASP.NET Core Minimal API (.NET 10). Consiste en un servicio REST para administrar un catalogo de personajes, cartas coleccionables y eventos historicos de una galaxia.
El sistema funciona completamente en memoria donde se estructura una Minimal API aplicando relaciones entre entidades y reglas de negocio, teniendo mas funcionalidades que un CRUD tradicional.

## Caracteristicas

- Gestion de recursos relacionados: Personajes (entidades base), Cartas (atributos 1:1) y Eventos (relaciones N:N).
- Reglas de negocio: Validacion de consistencia de estado (por ejemplo, evitar que un personaje sea registrado en un evento si su estado actual es muerto).
- Linea temporal: Soporte para la convencion cronologica ABY/BBY en los eventos.
- Logica y estadisticas: Generacion de rankings, calculo del MVP por evento y un motor para simular resultados de batallas en base a los atributos de las cartas.

## Tecnologias

- C# / .NET 10
- ASP.NET Core Minimal APIs
- LINQ (para consultas y filtros de colecciones)
- Swagger / OpenAPI

## Estructura del codigo

El proyecto mantiene una separacion directa de responsabilidades para no mezclar la logica con el enrutamiento:

- Data      : Colecciones de datos en memoria, basicos.
- Models    : Entidades de dominio y DTOs.
- Services  : Logica de negocio, validaciones y simulaciones.
- Endpoints : Mapeo de rutas y configuracion HTTP.
