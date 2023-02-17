using Dapper;
using Microsoft.Data.SqlClient;
using Presupuesto.Models;

namespace Presupuesto.Servicios
{
    public interface IRepositorioTransacciones
    {
        Task Actualizar(Transaccion transaccion, decimal montoAnterior, int cuentaAnterior);
        Task Borrar(int id);
        Task Crear(Transaccion transaccion);
        Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo);
        Task<Transaccion> ObtenerPorId(int id, int usuarioId);
    }

    public class RepositorioTransacciones: IRepositorioTransacciones
    {
        private readonly string connectionString;

        public RepositorioTransacciones(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConecction");
        }

        public async Task Crear(Transaccion transaccion)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"Transacciones_Insertar", 
                    new {
                        transaccion.UsuarioId,
                        transaccion.FechaTransaccion,
                        transaccion.Monto,
                        transaccion.CategoriaId,
                        transaccion.CuentaId,
                        transaccion.Nota
                    }, commandType: System.Data.CommandType.StoredProcedure);

            transaccion.id = id;

        }

        public async Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>(@"select t.id, t.Monto, t.FechaTransaccion, c.Nombre as Categoria,
                                                            cu.Nombre as Cuenta, c.TipoOperacionId
                                                            from transacciones t
                                                            inner join categorias c
                                                            on t.CategoriaId = c.id
                                                            inner join cuentas cu
                                                            on cu.id = t.CuentaId
                                                            where t.CuentaId = @CuentaId
                                                            and t.UsuarioId =  @usuarioId
                                                            and t.FechaTransaccion between @fechaInicio and @fechaFin", modelo);

        }

        public async Task Actualizar(Transaccion transaccion, 
                                    decimal montoAnterior, int cuentaAnteriorId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.QuerySingleAsync<int>(@"Transacciones_Actualizar",
                    new
                    {
                        transaccion.id,
                        transaccion.FechaTransaccion,
                        transaccion.Monto,
                        transaccion.CategoriaId,
                        transaccion.CuentaId,
                        transaccion.Nota,
                        montoAnterior,
                        cuentaAnteriorId
                    }, commandType: System.Data.CommandType.StoredProcedure);



        }

        public async Task<Transaccion> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaccion>(@"SELECT Transacciones.*, cat.TipoOperacionId
                                                                            FROM Transacciones
                                                                            INNER JOIN Categorias cat
                                                                            ON Cat.Id = Transacciones.CategoriaId
                                                                            where Transacciones.Id = @id
                                                                            and Transacciones.UsuarioId = @usuarioId
                                                                            ", new { id, usuarioId });
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.QuerySingleAsync<int>(@"Transacciones_Borrar",
                                                new { id }, commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
