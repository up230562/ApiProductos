using ApiProductos.Application.Interfaces;
using ApiProductos.Domain.DTOs;
using ApiProductos.Domain.Models;

namespace ApiProductos.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository repositorio;

    public ProductoService(IProductoRepository repositorio)
    {
        this.repositorio = repositorio;
    }

    public List<Producto> ObtenerTodos(string? buscar)
    {
        var productos = repositorio.ObtenerTodos();
        if (string.IsNullOrWhiteSpace(buscar)) return productos;
        return productos.Where(p => p.Nombre.Contains(buscar.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Producto? ObtenerPorId(int id) => repositorio.ObtenerPorId(id);

    public string? Validar(ProductoDto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Nombre) || producto.Nombre.Trim().Length > 100)
            return "El nombre es obligatorio y debe tener hasta 100 caracteres.";
        if (producto.Precio < 0 || producto.Precio > 99999999.99m || decimal.Round(producto.Precio, 2) != producto.Precio)
            return "El precio debe ser de 0 a 99999999.99 y tener máximo dos decimales.";
        if (producto.Cantidad < 0) return "La cantidad no puede ser negativa.";
        return null;
    }

    public Producto Crear(ProductoDto datos)
    {
        var producto = CrearModelo(datos);
        producto.Id = repositorio.Insertar(producto);
        return producto;
    }

    public bool Actualizar(int id, ProductoDto datos)
    {
        var producto = CrearModelo(datos);
        producto.Id = id;
        return repositorio.Actualizar(producto);
    }

    public bool Eliminar(int id) => repositorio.Eliminar(id);

    private static Producto CrearModelo(ProductoDto datos) => new()
    {
        Nombre = datos.Nombre.Trim(),
        Precio = datos.Precio,
        Cantidad = datos.Cantidad
    };
}
