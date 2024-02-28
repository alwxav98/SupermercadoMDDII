CREATE PROCEDURE sp_registrarusuarioss
    @Cedula varchar(10),
    @Nombre varchar(100),
    @Apellido varchar(100),
    @Direccion varchar(200),
    @Telefono varchar(10),
    @Correo varchar(max),
    @Sueldo decimal(8,2), -- Modificación aquí
    @Rol varchar(max),
    @Clave varchar(max),
    @EsActivo bit,
    @FechaRegistro datetime2(7),
    @Registrado bit output,
    @Mensaje varchar(100) output
AS
BEGIN
    IF (NOT EXISTS(SELECT * FROM Usuario WHERE correo = @Correo))
    BEGIN
        INSERT INTO Usuario(nombre, apellido, direccion, telefono, sueldo, rol, correo, clave, esActivo, fechaRegistro, cedula) 
        VALUES (@Nombre, @Apellido, @Direccion, @Telefono, @Sueldo, @Rol, @Correo, @Clave, @EsActivo, @FechaRegistro, @Cedula);
        SET @Registrado = 1;
        SET @Mensaje = 'Usuario registrado';
    END
    ELSE
    BEGIN
        SET @Registrado = 0;
        SET @Mensaje = 'Correo ya existente';
    END;
END;
