using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;

namespace EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository
{
    public interface IBenchRepository : IBaseRepository<Benches>
    {
        Benches GetBenchById(int id);
        Benches GetBenchByIdPopulated(int id);
    }
}
