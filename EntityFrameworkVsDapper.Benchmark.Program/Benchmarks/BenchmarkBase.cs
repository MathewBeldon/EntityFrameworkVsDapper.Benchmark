using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    public class BenchmarkBase
    {
        protected IBaseRepository<Benches> _baseGenericBenchRepository;
        protected IBenchRepository _benchRepository;
    }
}
