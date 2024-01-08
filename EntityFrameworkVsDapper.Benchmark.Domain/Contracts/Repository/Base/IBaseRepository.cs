using EntityFrameworkVsDapper.Benchmark.Core.Entities.Common;

namespace EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetByIdPaged(int page, int pageSize, int totalCount);
    }
}
