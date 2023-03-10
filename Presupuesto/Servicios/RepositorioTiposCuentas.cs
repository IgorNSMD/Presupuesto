using Dapper;
using Microsoft.Data.SqlClient;
using Presupuesto.Models;

namespace Presupuesto.Servicios
{
    public interface IRepositorioTipoCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Crear(TipoCuenta tipoCuenta);
        Task Eliminar(int id);
        Task<bool> Existe(string nombre, int usuarioId, int id = 0);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tiposCuentas);
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
                                                        ("TiposCuentas_Insertar", new
                                                        {
                                                            Nombre = tipoCuenta.Nombre,
                                                            UsuarioId = tipoCuenta.UsuarioId,
 
                                                        }, commandType: System.Data.CommandType.StoredProcedure);
                tipoCuenta.id = id;

            }
        }

        public async Task<bool> Existe(string nombre, int usuarioId, int id = 0)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                                                        (@"SELECT 1 FROM TiposCuentas
                                                           WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId and id <> @id", new
                                                        { nombre, usuarioId, id });
            return existe == 1;

        }


        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>
                                            (@"SELECT Id, Nombre, Orden 
                                               FROM TiposCuentas
                                               WHERE UsuarioId = @UsuarioId
                                               ORDER BY Orden", new { usuarioId });

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

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM TiposCuentas
                                           WHERE Id = @Id", 
                                           new { id });
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tiposCuentas)
        {
            var query = "UPDATE TiposCuentas SET Orden = @Orden WHERE Id = @Id";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tiposCuentas);

        }
    }
}
