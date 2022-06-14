using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace EntityFrameworkVsDapper.Benchmark.Dapper
{
    public class BenchmarkDbConnection : IDisposable
    {
        private readonly IDbConnection connection;
        public BenchmarkDbConnection(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        public IReadOnlyList<T> Query<T>(CommandDefinition commandDefinition)
        {
            return connection.Query<T>(commandDefinition).AsList();
        }

        public T QueryFirstOrDefault<T>(CommandDefinition commandDefinition)
        {
            return connection.QueryFirstOrDefault<T>(commandDefinition);
        }

        public T QuerySingle<T>(CommandDefinition commandDefinition)
        {
            return connection.QuerySingle<T>(commandDefinition);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
