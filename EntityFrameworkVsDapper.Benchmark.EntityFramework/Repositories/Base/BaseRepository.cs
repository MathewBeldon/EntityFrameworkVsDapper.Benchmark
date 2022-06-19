using EntityFrameworkVsDapper.Benchmark.Core.Common;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly BenchmarkDbContext _context;
        public BaseRepository(BenchmarkDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        private static readonly Func<BenchmarkDbContext, int, T> GetByIdCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int id) => 
                context
                    .Set<T>()
                    .First(p => p.Id == id));
        public T GetById(int id)
        {
            return GetByIdCompiled(_context, id);
        }

        private static readonly Func<BenchmarkDbContext, int, int, int, IEnumerable<T>> GetAllCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int page, int pageSize, int totalCount) =>
                context
                    .Set<T>()
                    .OrderByDescending(x => x.Id)
                    .Where(a => a.Id <= totalCount - (pageSize * (page - 1)))
                    .Take(pageSize));
        public IEnumerable<T> GetByIdPaged(int page, int pageSize, int totalCount)
        {
            return GetAllCompiled(_context, page, pageSize, totalCount);
        }
    }
}
