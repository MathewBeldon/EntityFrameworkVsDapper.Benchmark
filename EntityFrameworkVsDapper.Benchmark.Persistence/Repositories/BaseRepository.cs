using EntityFrameworkVsDapper.Benchmark.Domain.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Domain.Entities;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly BenchmarkDbContext _context;
        public BaseRepository(BenchmarkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Benches GetById(int id)
        {
            return _context.Benches.Find(new object[] { id });
        }
    }
}
