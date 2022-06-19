using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;

namespace EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository
{
    public interface IBenchRepository : IBaseRepository<Benches>
    {
        Benches GetBench(int id);
        Benches GetBenchPopulated(int id);
        IEnumerable<Benches> GetBenchPopulatedPage(int page, int pageSize, int totalCount);
        Benches CreateBench(Benches bench);
        void UpdateBench(Benches bench);
        void DeleteBench(int id);
    }
}
