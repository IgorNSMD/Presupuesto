using Dapper;
using Microsoft.Data.SqlClient;
using Presupuesto.Models;

namespace Presupuesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
    }

    public class RepositorioCuentas: IRepositorioCuentas
    {
        private readonly string connectionString;

        public RepositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConecction");
        }
        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Cuentas (Nombre, TipoCuentaId, Balance, Descripcion)
                                                            VALUES (@Nombre, @TipoCuentaId, @Balance, @Descripcion);
                                                            SELECT SCOPE_IDENTITY();", cuenta);
        }

        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Cuenta>(@"SELECT Cuentas.Id, Cuentas.Nombre, Balance, tc.Nombre as TipoCuenta
                                                        FROM Cuentas
                                                        INNER JOIN TiposCuentas tc ON
                                                        tc.id = Cuentas.TipoCuentaId
                                                        WHERE tc.UsuarioId = @usuarioId
                                                        ORDER BY tc.Orden", new { usuarioId });
        }
    }
}
