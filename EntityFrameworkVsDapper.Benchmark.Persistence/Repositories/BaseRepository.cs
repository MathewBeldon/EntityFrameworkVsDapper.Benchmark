using EntityFrameworkVsDapper.Benchmark.Domain.Common;
using EntityFrameworkVsDapper.Benchmark.Persistence;
using EntityFrameworkVsDapper.Benchmark.Program.Contracts.Repository;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly BenchmarkDbContext _context;
        public BaseRepository(BenchmarkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
