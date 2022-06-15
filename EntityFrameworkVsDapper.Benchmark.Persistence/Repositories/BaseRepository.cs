using EntityFrameworkVsDapper.Benchmark.Core.Common;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly BenchmarkDbContext _context;
        public BaseRepository(BenchmarkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T GetById(int id)
        {
            return _context.Set<T>().AsNoTracking().SingleOrDefault(x => x.Id == id);
        }
    }
}
