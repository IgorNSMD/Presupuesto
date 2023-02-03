using Dapper;
using Microsoft.Data.SqlClient;
using Presupuesto.Models;

namespace Presupuesto.Servicios
{
    public interface IRepositorioTipoCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
    }

    public class RepositorioTiposCuentas: IRepositorioTipoCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConecction");
        }
        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = connection.Query("SELECT 1").FirstOrDefault();
                var id = await connection.QuerySingleAsync<int>
                                                        (@"INSERT INTO TiposCuentas(Nombre,UsuarioId,Orden )
                                                        VALUES (@Nombre,@UsuarioId,0);
                                                        SELECT SCOPE_IDENTITY()", tipoCuenta);
                tipoCuenta.id = id;

            }
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                                                        (@"SELECT 1 FROM TiposCuentas
                                                           WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId", new
                                                        { nombre, usuarioId });
            return existe == 1;

        }


        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>
                                            (@"SELECT Id, Nombre, Orden 
                                               FROM TiposCuentas
                                               WHERE UsuarioId = @UsuarioId", new { usuarioId });

        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas 
                                           SET Nombre = @Nombre
                                           WHERE Id = @Id", tipoCuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>
                                                                (@"SELECT Id, Nombre, Orden
                                                                   FROM TiposCuentas 
                                                                   WHERE Id = @Id and UsuarioId = @UsuarioId",
                                                                   new { id, usuarioId });
        }
    }
}
