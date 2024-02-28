create proc sp_ValidarUsuario(
@Correo varchar(100),
@Clave varchar(500)
)

as
begin
	if (exists(select * from Usuario where correo = @Correo and clave = @Clave))
		select Idusuario from Usuario where correo = @Correo and clave = @Clave
	else
		select '0'
end