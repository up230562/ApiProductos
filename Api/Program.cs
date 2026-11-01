using ApiProductos.Endpoints;
using ApiProductos.Application.Interfaces;
using ApiProductos.Application.Services;
using ApiProductos.Infrastructure.Repositories;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var cadenaConexion = builder.Configuration.GetConnectionString("MySql")
    ?? throw new InvalidOperationException("Falta la cadena de conexión MySql.");
builder.Services.AddScoped<IProductoRepository>(_ => new ProductoRepository(cadenaConexion));
builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

app.Use(async (contexto, siguiente) =>
{
    try
    {
        await siguiente(contexto);
    }
    catch (MySqlException)
    {
        contexto.Response.StatusCode = 503;
        await contexto.Response.WriteAsJsonAsync(new { mensaje = "No se pudo consultar MySQL. Revisa que la base de datos esté encendida y la conexión sea correcta." });
    }
});

app.UseSwagger();
app.UseSwaggerUI();
app.MapProductoEndpoints();
app.Run();
