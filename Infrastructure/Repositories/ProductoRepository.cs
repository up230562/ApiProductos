using ApiProductos.Application.Interfaces;
using ApiProductos.Domain.Models;
using MySqlConnector;

namespace ApiProductos.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly string cadenaConexion;

    public ProductoRepository(string cadenaConexion)
    {
        this.cadenaConexion = cadenaConexion;
    }

    public List<Producto> ObtenerTodos()
    {
        var productos = new List<Producto>();
        using var conexion = new MySqlConnection(cadenaConexion);
        conexion.Open();

        string sql = "SELECT id, nombre, precio, cantidad FROM productos ORDER BY id";
        using var comando = new MySqlCommand(sql, conexion);
        using var lector = comando.ExecuteReader();

        while (lector.Read())
        {
            productos.Add(LeerProducto(lector));
        }

        return productos;
    }

    public Producto? ObtenerPorId(int id)
    {
        using var conexion = new MySqlConnection(cadenaConexion);
        conexion.Open();

        string sql = "SELECT id, nombre, precio, cantidad FROM productos WHERE id = @id";
        using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);
        using var lector = comando.ExecuteReader();

        return lector.Read() ? LeerProducto(lector) : null;
    }

    public int Insertar(Producto producto)
    {
        using var conexion = new MySqlConnection(cadenaConexion);
        conexion.Open();

        string sql = "INSERT INTO productos (nombre, precio, cantidad) VALUES (@nombre, @precio, @cantidad)";
        using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@nombre", producto.Nombre);
        comando.Parameters.AddWithValue("@precio", producto.Precio);
        comando.Parameters.AddWithValue("@cantidad", producto.Cantidad);
        comando.ExecuteNonQuery();

        return checked((int)comando.LastInsertedId);
    }

    public bool Actualizar(Producto producto)
    {
        using var conexion = new MySqlConnection(cadenaConexion);
        conexion.Open();

        string sql = "UPDATE productos SET nombre = @nombre, precio = @precio, cantidad = @cantidad WHERE id = @id";
        using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", producto.Id);
        comando.Parameters.AddWithValue("@nombre", producto.Nombre);
        comando.Parameters.AddWithValue("@precio", producto.Precio);
        comando.Parameters.AddWithValue("@cantidad", producto.Cantidad);

        return comando.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var conexion = new MySqlConnection(cadenaConexion);
        conexion.Open();

        string sql = "DELETE FROM productos WHERE id = @id";
        using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);

        return comando.ExecuteNonQuery() > 0;
    }

    private static Producto LeerProducto(MySqlDataReader lector)
    {
        return new Producto
        {
            Id = lector.GetInt32("id"),
            Nombre = lector.GetString("nombre"),
            Precio = lector.GetDecimal("precio"),
            Cantidad = lector.GetInt32("cantidad")
        };
    }
}
