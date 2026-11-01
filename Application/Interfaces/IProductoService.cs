using ApiProductos.Domain.DTOs;
using ApiProductos.Domain.Models;

namespace ApiProductos.Application.Interfaces;

public interface IProductoService
{
    List<Producto> ObtenerTodos(string? buscar);
    Producto? ObtenerPorId(int id);
    string? Validar(ProductoDto producto);
    Producto Crear(ProductoDto producto);
    bool Actualizar(int id, ProductoDto producto);
    bool Eliminar(int id);
}
