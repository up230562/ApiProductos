namespace ApiProductos.Domain.DTOs;

// Datos que se reciben al crear o actualizar un producto.
public class ProductoDto
{
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
}
