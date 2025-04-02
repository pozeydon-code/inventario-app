-- Crear la base de datos
CREATE DATABASE inventarioApp

-- Usar la base de datos
USE inventarioApp

-- Crear tabla Products
CREATE TABLE Products (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(100),
    Description NVARCHAR(255),
    Category NVARCHAR(100),
    Image NVARCHAR(255),
    Price DECIMAL(18,2),
    Stock INT
);

-- Crear tabla Transactions
CREATE TABLE Transactions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Date DATETIME,
    Type int,
    ProductId UNIQUEIDENTIFIER,
    Quantity INT,
    UnitPrice DECIMAL(18,2),
    Detail NVARCHAR(255),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Insertar 15 productos
INSERT INTO Products (Id, Name, Description, Category, ImageUrl, Price, Stock) VALUES
(NEWID(), 'Laptop Lenovo', 'Portátil potente para trabajo', 'Electrónica', '', 899.99, 20),
(NEWID(), 'Teclado Mecánico', 'RGB y switches rojos', 'Accesorios', '', 79.50, 40),
(NEWID(), 'Mouse Logitech', 'Mouse inalámbrico', 'Accesorios', '', 49.99, 60),
(NEWID(), 'Monitor Dell 27"', 'Full HD con HDMI', 'Electrónica', '', 219.00, 15),
(NEWID(), 'Silla Gamer', 'Ergonómica y reclinable', 'Muebles', '', 199.90, 10),
(NEWID(), 'SSD Samsung 1TB', 'Velocidad de lectura alta', 'Almacenamiento', '', 139.00, 25),
(NEWID(), 'Memoria RAM 16GB', 'DDR4 3200MHz', 'Hardware', '', 75.00, 30),
(NEWID(), 'Tablet Samsung', '10 pulgadas', 'Electrónica', '', 299.99, 12),
(NEWID(), 'Impresora HP', 'Multifunción con WiFi', 'Oficina', '', 129.99, 18),
(NEWID(), 'Auriculares JBL', 'Bluetooth y cancelación', 'Audio', '', 89.95, 22),
(NEWID(), 'Cámara Logitech', 'Full HD para streaming', 'Accesorios', '', 59.90, 35),
(NEWID(), 'Hub USB-C', '6 en 1 con HDMI', 'Accesorios', '', 45.00, 50),
(NEWID(), 'PowerBank Xiaomi', '20000mAh carga rápida', 'Energía', '', 39.90, 20),
(NEWID(), 'Cargador Anker', 'USB dual 30W', 'Accesorios', '', 29.99, 45),
(NEWID(), 'Cable USB-C', '1 metro trenzado', 'Accesorios', '', 9.99, 100);

-- Para insertar las transacciones debemos generar primero los productos y ahi ejecutar agregarlo manualmente.
-- EJEMPLO
-- INSERT INTO Transactions (Id, Date, Type, ProductId , Quantity , UnitPrice , Detail) VALUES
-- (NEWID(), GETDATE(), 0, '6BFF7CF6-87D1-470B-AEDC-1B922CF0ADEB', 5, 120, 'Stock inicial')
