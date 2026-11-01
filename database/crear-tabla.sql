-- Docker crea la base tienda_productos antes de ejecutar este archivo.
-- Si utilizas MySQL sin Docker, crea la base y selecciónala primero.
CREATE TABLE IF NOT EXISTS productos (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    cantidad INT NOT NULL
);
