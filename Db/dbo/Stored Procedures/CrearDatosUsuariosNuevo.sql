-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE CrearDatosUsuariosNuevo 
@UsuarioId int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @efectivo nvarchar(50) = 'Efectivo';
	declare @CuentasDeBanco nvarchar(50) = 'Cuentas de Banco';
	declare @Tarjetas nvarchar(50) = 'Tarjetas';

	insert into TiposCuentas(Nombre, UsuarioId, Orden)
	values(@efectivo,@UsuarioId,1),
	(@CuentasDeBanco,@UsuarioId,2),
	(@Tarjetas,@UsuarioId,3);

	insert into Cuentas (Nombre, Balance, TipoCuentaId)
	select nombre,0,id
	from TiposCuentas
	where usuarioid = @UsuarioId

	insert into categorias(nombre,TipoOperacionId,UsuarioId)
	values 
	('Libros',2,@UsuarioId),
	('Salario',1,@UsuarioId),
	('Ingreso Extra',1,@UsuarioId),
	('Comida',2,@UsuarioId)


END
