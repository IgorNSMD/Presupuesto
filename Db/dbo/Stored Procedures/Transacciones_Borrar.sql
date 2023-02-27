
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Borrar]
	-- Add the parameters for the stored procedure here
	@Id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @monto decimal(18,2),
			@cuentaId int,
			@TipoOperacionId int

	select @monto = Monto, @cuentaId = CuentaId, @TipoOperacionId = cat.TipoOperacionId 
	from Transacciones
	inner join Categorias cat on
	cat.Id = Transacciones.CategoriaId
	where Transacciones.Id = @Id

	declare @FactorMultiplicativo int = 1

	if (@TipoOperacionId = 2)
		set @FactorMultiplicativo = -1

	set @monto = @monto * @FactorMultiplicativo;

	update cuentas set
	balance -= @monto
	where id = @cuentaId

	delete from Transacciones
	where Id = @Id 

	select 1;


END
