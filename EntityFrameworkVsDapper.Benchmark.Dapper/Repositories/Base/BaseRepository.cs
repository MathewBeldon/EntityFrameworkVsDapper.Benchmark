using Dapper;
using EntityFrameworkVsDapper.Benchmark.Core.Common;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;

namespace EntityFrameworkVsDapper.Benchmark.Dapper.Repositories.Base
{

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        internal readonly BenchmarkDbConnection _context;
        private readonly string _tableName;
        public BaseRepository(BenchmarkDbConnection context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tableName = typeof(T).Name;
        }

        public T GetById(int id)
        {
            string sql =
                @$"SELECT * 
                  FROM {_tableName}
                  WHERE id = @Id";

            var commandDefinition = new CommandDefinition(sql, new
            {
                Id = id,
            }, flags: CommandFlags.Buffered);

            return _context.connection.QuerySingle<T>(commandDefinition);
        }

        public IEnumerable<T> GetAll()
        {
            string sql =
                @$"SELECT * 
                  FROM {_tableName}";

            var commandDefinition = new CommandDefinition(sql, flags: CommandFlags.Buffered);

            return _context.connection.Query<T>(commandDefinition);
        }
    }
}
