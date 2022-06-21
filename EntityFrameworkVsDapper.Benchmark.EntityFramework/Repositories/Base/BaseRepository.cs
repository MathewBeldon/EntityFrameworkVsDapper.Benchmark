using EntityFrameworkVsDapper.Benchmark.Core.Common;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public BaseRepository()
        {
        }

        private static readonly Func<BenchmarkDbContext, int, T> GetByIdCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int id) =>
                context
                    .Set<T>()
                    .First(p => p.Id == id));
        public T GetById(int id)
        {
            using (var context = new BenchmarkDbContext())
            {
                return GetByIdCompiled(context, id);
            }
        }

        private static readonly Func<BenchmarkDbContext, int, int, int, IEnumerable<T>> GetByIdPagedCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int page, int pageSize, int totalCount) =>
                context
                    .Set<T>()
                    .OrderByDescending(x => x.Id)
                    .Where(a => a.Id <= totalCount - (pageSize * (page - 1)))
                    .Take(pageSize));
        public IEnumerable<T> GetByIdPaged(int page, int pageSize, int totalCount)
        {
            using (var context = new BenchmarkDbContext())
            {                
                return GetByIdPagedCompiled(context, page, pageSize, totalCount).ToList();
            }
        }
    }
}