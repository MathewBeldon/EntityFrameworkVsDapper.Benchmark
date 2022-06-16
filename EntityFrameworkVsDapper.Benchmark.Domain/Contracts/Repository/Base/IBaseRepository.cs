using EntityFrameworkVsDapper.Benchmark.Core.Common;

namespace EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
