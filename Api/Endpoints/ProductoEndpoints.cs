using ApiProductos.Application.Interfaces;
using ApiProductos.Domain.DTOs;

namespace ApiProductos.Endpoints;

public static class ProductoEndpoints
{
    public static IEndpointRouteBuilder MapProductoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/productos", (string? buscar, IProductoService servicio) =>
        {
            return Results.Ok(servicio.ObtenerTodos(buscar));
        });

        endpoints.MapGet("/productos/{id:int}", (int id, IProductoService servicio) =>
        {
            var producto = servicio.ObtenerPorId(id);
            return producto is null
                ? Results.NotFound(new { mensaje = "Producto no encontrado." })
                : Results.Ok(producto);
        });

        endpoints.MapPost("/productos", (ProductoDto producto, IProductoService servicio) =>
        {
            string? error = servicio.Validar(producto);
            if (error is not null) return Results.BadRequest(new { mensaje = error });
            var creado = servicio.Crear(producto);
            return Results.Created($"/productos/{creado.Id}", creado);
        });

        endpoints.MapPut("/productos/{id:int}", (int id, ProductoDto producto, IProductoService servicio) =>
        {
            string? error = servicio.Validar(producto);
            if (error is not null) return Results.BadRequest(new { mensaje = error });
            return servicio.Actualizar(id, producto)
                ? Results.NoContent()
                : Results.NotFound(new { mensaje = "Producto no encontrado." });
        });

        endpoints.MapDelete("/productos/{id:int}", (int id, IProductoService servicio) =>
        {
            return servicio.Eliminar(id)
                ? Results.NoContent()
                : Results.NotFound(new { mensaje = "Producto no encontrado." });
        });

        return endpoints;
    }

}
