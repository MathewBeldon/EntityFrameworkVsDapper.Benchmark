using Dapper;
using EntityFrameworkVsDapper.Benchmark.Core.Common;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;

namespace EntityFrameworkVsDapper.Benchmark.Dapper.Repositories
{

    public sealed class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly BenchmarkDbConnection _context;
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
            });

            return _context.QuerySingle<T>(commandDefinition);
        }
    }
}
