using EntityFrameworkVsDapper.Benchmark.Domain.Common;

namespace EntityFrameworkVsDapper.Benchmark.Domain.Contracts.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
    }
}
