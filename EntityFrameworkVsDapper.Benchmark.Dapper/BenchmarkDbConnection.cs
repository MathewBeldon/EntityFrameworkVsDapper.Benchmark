using Npgsql;
using System.Data;

namespace EntityFrameworkVsDapper.Benchmark.Dapper
{
    public class BenchmarkDbConnection : IDisposable
    {
        public readonly IDbConnection connection;

        public BenchmarkDbConnection(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
