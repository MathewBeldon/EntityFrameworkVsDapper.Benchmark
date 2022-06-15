using EntityFrameworkVsDapper.Benchmark.Core.Common;

namespace EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
    }
}
