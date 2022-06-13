using EntityFrameworkVsDapper.Benchmark.Domain.Entities;

namespace EntityFrameworkVsDapper.Benchmark.Domain.Contracts.Repository
{
    public interface IBaseRepository
    {
        Benches GetById(int id);
    }
}
