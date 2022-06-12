using EntityFrameworkVsDapper.Benchmark.Domain.Common;

namespace EntityFrameworkVsDapper.Benchmark.Program.Contracts.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
