IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categoria] (
    [Idcategoria] int NOT NULL IDENTITY,
    [descripcion] nvarchar(50) NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Idcategoria])
);
GO

CREATE TABLE [Cliente] (
    [Idcliente] int NOT NULL IDENTITY,
    [cedula] nvarchar(10) NOT NULL,
    [nombre] nvarchar(100) NOT NULL,
    [apellido] nvarchar(100) NOT NULL,
    [direccion] nvarchar(200) NULL,
    [telefono] nvarchar(10) NULL,
    [puntosRecompensa] int NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Idcliente])
);
GO

CREATE TABLE [Configuracion ] (
    [Idconfiguracion] int NOT NULL IDENTITY,
    [recurso] nvarchar(50) NULL,
    [propiedad] nvarchar(50) NULL,
    [valor] nvarchar(60) NULL,
    CONSTRAINT [PK_Configuracion ] PRIMARY KEY ([Idconfiguracion])
);
GO

CREATE TABLE [Negocio] (
    [Idnegocio] int NOT NULL IDENTITY,
    [ruc] nvarchar(50) NULL,
    [nombre] nvarchar(50) NULL,
    [correo] nvarchar(50) NULL,
    [direccion] nvarchar(50) NULL,
    [telefono] nvarchar(50) NULL,
    [porcentajeImpuesto] decimal(8,2) NULL,
    CONSTRAINT [PK_Negocio] PRIMARY KEY ([Idnegocio])
);
GO

CREATE TABLE [Proveedor] (
    [Idproveedor] int NOT NULL IDENTITY,
    [ruc] nvarchar(13) NOT NULL,
    [nombre] nvarchar(100) NOT NULL,
    [apellido] nvarchar(100) NOT NULL,
    [direccion] nvarchar(200) NULL,
    [telefono] nvarchar(10) NULL,
    [correo] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Proveedor] PRIMARY KEY ([Idproveedor])
);
GO

CREATE TABLE [Usuario] (
    [Idusuario] int NOT NULL IDENTITY,
    [cedula] nvarchar(10) NOT NULL,
    [nombre] nvarchar(100) NOT NULL,
    [apellido] nvarchar(100) NOT NULL,
    [direccion] nvarchar(200) NULL,
    [telefono] nvarchar(10) NULL,
    [correo] nvarchar(max) NULL,
    [sueldo] decimal(8,2) NOT NULL,
    [rol] nvarchar(max) NOT NULL,
    [clave] nvarchar(max) NOT NULL,
    [esActivo] bit NOT NULL,
    [fechaRegistro] datetime2 NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Idusuario])
);
GO

CREATE TABLE [Producto] (
    [Idproducto] int NOT NULL IDENTITY,
    [codigoBarra] nvarchar(50) NOT NULL,
    [marca] nvarchar(50) NOT NULL,
    [descripcion] nvarchar(100) NOT NULL,
    [Idcategoria] int NOT NULL,
    [Idproveedor] int NOT NULL,
    [stock] int NOT NULL,
    [precio] decimal(8,2) NOT NULL,
    [esActivo] bit NOT NULL,
    [fechaCaducidad] datetime2 NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY ([Idproducto]),
    CONSTRAINT [FK_Producto_Categoria_Idcategoria] FOREIGN KEY ([Idcategoria]) REFERENCES [Categoria] ([Idcategoria]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Producto_Proveedor_Idproveedor] FOREIGN KEY ([Idproveedor]) REFERENCES [Proveedor] ([Idproveedor]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Asistencia] (
    [Idasistencia] int NOT NULL IDENTITY,
    [Idusuario] int NOT NULL,
    [ingreso] datetime2 NOT NULL,
    [salida] datetime2 NOT NULL,
    [horasExtra] float NOT NULL,
    CONSTRAINT [PK_Asistencia] PRIMARY KEY ([Idasistencia]),
    CONSTRAINT [FK_Asistencia_Usuario_Idusuario] FOREIGN KEY ([Idusuario]) REFERENCES [Usuario] ([Idusuario]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Venta] (
    [Idventa] int NOT NULL IDENTITY,
    [Idusuario] int NOT NULL,
    [Idcliente] int NOT NULL,
    [subTotal] decimal(8,2) NULL,
    [impuestoTotal] decimal(8,2) NULL,
    [Total] decimal(8,2) NOT NULL,
    [fechaRegistro] datetime2 NOT NULL,
    CONSTRAINT [PK_Venta] PRIMARY KEY ([Idventa]),
    CONSTRAINT [FK_Venta_Cliente_Idcliente] FOREIGN KEY ([Idcliente]) REFERENCES [Cliente] ([Idcliente]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Venta_Usuario_Idusuario] FOREIGN KEY ([Idusuario]) REFERENCES [Usuario] ([Idusuario]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Pedido] (
    [Idpedido] int NOT NULL IDENTITY,
    [Idproducto] int NOT NULL,
    [Idproveedor] int NOT NULL,
    [cantidad] int NOT NULL,
    [precioU] decimal(8,2) NOT NULL,
    [total] decimal(8,2) NOT NULL,
    [fechaPedido] datetime2 NOT NULL,
    [fechaEntrega] datetime2 NOT NULL,
    CONSTRAINT [PK_Pedido] PRIMARY KEY ([Idpedido]),
    CONSTRAINT [FK_Pedido_Producto_Idproducto] FOREIGN KEY ([Idproducto]) REFERENCES [Producto] ([Idproducto]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Pedido_Proveedor_Idproveedor] FOREIGN KEY ([Idproveedor]) REFERENCES [Proveedor] ([Idproveedor]) ON DELETE NO ACTION
);
GO

CREATE TABLE [DetalleVenta ] (
    [IddetalleVenta] int NOT NULL IDENTITY,
    [Idventa] int NOT NULL,
    [Idproducto] int NOT NULL,
    [cantidad] int NOT NULL,
    [precio] decimal(8,2) NOT NULL,
    [total] decimal(8,2) NOT NULL,
    CONSTRAINT [PK_DetalleVenta ] PRIMARY KEY ([IddetalleVenta]),
    CONSTRAINT [FK_DetalleVenta _Producto_Idproducto] FOREIGN KEY ([Idproducto]) REFERENCES [Producto] ([Idproducto]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleVenta _Venta_Idventa] FOREIGN KEY ([Idventa]) REFERENCES [Venta] ([Idventa]) ON DELETE NO ACTION
);
GO

CREATE TABLE [EstadoPedido] (
    [IdestadoPedido] int NOT NULL IDENTITY,
    [Idpedido] int NOT NULL,
    [estado] nvarchar(max) NOT NULL,
    [calificiacion] nvarchar(max) NULL,
    CONSTRAINT [PK_EstadoPedido] PRIMARY KEY ([IdestadoPedido]),
    CONSTRAINT [FK_EstadoPedido_Pedido_Idpedido] FOREIGN KEY ([Idpedido]) REFERENCES [Pedido] ([Idpedido]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Asistencia_Idusuario] ON [Asistencia] ([Idusuario]);
GO

CREATE INDEX [IX_DetalleVenta _Idproducto] ON [DetalleVenta ] ([Idproducto]);
GO

CREATE INDEX [IX_DetalleVenta _Idventa] ON [DetalleVenta ] ([Idventa]);
GO

CREATE INDEX [IX_EstadoPedido_Idpedido] ON [EstadoPedido] ([Idpedido]);
GO

CREATE INDEX [IX_Pedido_Idproducto] ON [Pedido] ([Idproducto]);
GO

CREATE INDEX [IX_Pedido_Idproveedor] ON [Pedido] ([Idproveedor]);
GO

CREATE INDEX [IX_Producto_Idcategoria] ON [Producto] ([Idcategoria]);
GO

CREATE INDEX [IX_Producto_Idproveedor] ON [Producto] ([Idproveedor]);
GO

CREATE INDEX [IX_Venta_Idcliente] ON [Venta] ([Idcliente]);
GO

CREATE INDEX [IX_Venta_Idusuario] ON [Venta] ([Idusuario]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240220040117_m2', N'7.0.0');
GO

COMMIT;
GO

