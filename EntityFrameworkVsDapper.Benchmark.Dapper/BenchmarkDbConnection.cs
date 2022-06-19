using MySql.Data.MySqlClient;
using System.Data;

namespace EntityFrameworkVsDapper.Benchmark.Dapper
{
    public class BenchmarkDbConnection : IDisposable
    {
        public readonly IDbConnection connection;

        public BenchmarkDbConnection(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
