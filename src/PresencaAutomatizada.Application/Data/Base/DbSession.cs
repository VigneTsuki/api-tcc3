using MySql.Data.MySqlClient;
using System.Data;

namespace PresencaAutomatizada.Application.Data.Base
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("SqlDb"));
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
