using Dapper;
using Microsoft.Data.SqlClient;
using Presupuesto.Models;

namespace Presupuesto.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }
    public class RepositorioUsuario : IRepositorioUsuarios
    {
        private readonly string connectionString;

        public RepositorioUsuario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConecction");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            
            //usuario.EmailNormalizado = usuario.Email.ToUpper();

            using var connection = new SqlConnection(connectionString);
            var usuarioId = await connection.QuerySingleAsync<int>(@"
            INSERT INTO Usuarios (Email,EmailNormalizado, PasswordHash)
            VALUES (@Email,@EmailNormalizado,@PasswordHash);
            SELECT SCOPE_IDENTITY();
            ", usuario);

            await connection.ExecuteAsync("CrearDatosUsuariosNuevo", new { usuarioId },
                commandType: System.Data.CommandType.StoredProcedure);

            return usuarioId;

        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>
                (@"SELECT * FROM Usuarios WHERE EmailNormalizado = @emailNormalizado", new { emailNormalizado });

        }

    }
}
