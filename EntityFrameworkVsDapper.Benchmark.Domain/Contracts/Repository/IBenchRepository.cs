using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;

namespace EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository
{
    public interface IBenchRepository : IBaseRepository<Benches>
    {
        Benches GetBench(int id);
        Benches GetBenchPopulated(int id);
        IEnumerable<Benches> GetBenchPopulatedPage(int page, int pageSize, int totalCount);
        void CreateBench(Benches benche);
        void UpdateBench(Benches benche);
        void DeleteBench(int id);
    }
}
