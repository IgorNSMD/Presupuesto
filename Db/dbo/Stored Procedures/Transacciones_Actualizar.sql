



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Actualizar]
	-- Add the parameters for the stored procedure here
	@Id int,
	@FechaTransaccion date,
	@Monto decimal(18,2),
	@CategoriaId int,
	@CuentaId int,
	@Nota nvarchar(1000) = null,
	@MontoAnterior decimal(18,2),
	@CuentaAnteriorId int


AS
BEGIN
	SET NOCOUNT ON  

	 -- revertir transaccion anterior
	update cuentas 
	set balance -= @MontoAnterior
	where Id = @CuentaAnteriorId

	-- Realizar nueva transaccion
	update cuentas 
	set balance += @Monto
	where Id = @CuentaId

	update Transacciones
	set
	monto = abs(@Monto), 
	FechaTransaccion = @FechaTransaccion,
	CategoriaId = @CategoriaId,
	CuentaId = @CuentaId,
	Nota = @Nota
	where id = @id
	 
	 select * from Transacciones where id = @id;
	
END
