# API de productos

API REST para administrar productos mediante operaciones CRUD. Está desarrollada con ASP.NET Core y utiliza MySQL para almacenar la información. Incluye Swagger para consultar y probar sus endpoints.

## Tecnologías

- .NET 10 y ASP.NET Core
- MySQL y MySqlConnector
- Swagger / OpenAPI
- Docker Compose para ejecutar MySQL localmente

## Estructura

```text
ApiProductos/
├── Api/              Endpoints y configuración de la aplicación
├── Application/      Interfaces, servicios y validaciones
├── Domain/           Modelo de producto y DTO
├── Infrastructure/   Repositorio y consultas a MySQL
├── database/         Script para crear la tabla
└── docker-compose.yml
```

## Requisitos

- SDK de .NET 10.
- Docker y Docker Compose, o una instalación local de MySQL.

## Ejecución

Desde la carpeta del proyecto, inicia MySQL:

```bash
docker compose up -d
```

El contenedor crea la base de datos y la tabla en su primer inicio. Para revisar que esté listo, ejecuta `docker compose ps` y espera a que el servicio aparezca como `healthy`.

Después inicia la API:

```bash
dotnet run --project Api
```

Swagger estará disponible en [http://localhost:5210/swagger](http://localhost:5210/swagger).

La configuración de conexión se encuentra en `Api/appsettings.json`. Si utilizas una instalación propia de MySQL, crea la base `tienda_productos`, ejecuta `database/crear-tabla.sql` y ajusta la cadena de conexión. Las credenciales incluidas son de ejemplo para uso local.

## Endpoints

| Método | Ruta | Descripción |
| --- | --- | --- |
| GET | `/productos` | Lista los productos; admite `?buscar=texto` para filtrar por nombre. |
| GET | `/productos/{id}` | Obtiene un producto por su ID. |
| POST | `/productos` | Crea un producto. |
| PUT | `/productos/{id}` | Actualiza un producto. |
| DELETE | `/productos/{id}` | Elimina un producto. |

Ejemplo de cuerpo para crear o actualizar un producto:

```json
{
  "nombre": "Cuaderno",
  "precio": 35.50,
  "cantidad": 10
}
```

El nombre es obligatorio, el precio debe ser no negativo y tener como máximo dos decimales, y la cantidad debe ser no negativa. Las rutas por ID responden con `404` cuando el producto no existe.
