
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Insertar] 
	-- Add the parameters for the stored procedure here
	@UsuarioId int,
	@FechaTransaccion date,
	@Monto decimal(18,2),
	@CategoriaId int,
	@CuentaId int,
	@Nota nvarchar(1000) = null
AS
BEGIN
	set nocount on;

	insert into Transacciones(UsuarioId, FechaTransaccion, Monto, CategoriaId, CuentaId,  Nota)
	values (@UsuarioId, @FechaTransaccion, abs(@Monto), @CategoriaId, @CuentaId,  @Nota)

	update cuentas 
	set Balance += @Monto
	Where Id = @CuentaId;

	select SCOPE_IDENTITY();


END
