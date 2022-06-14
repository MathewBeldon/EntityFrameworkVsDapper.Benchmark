using EntityFrameworkVsDapper.Benchmark.Domain.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Domain.Entities;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    internal static class GetBenchById
    {
        internal static void OneHundredThousand(IBaseRepository<Benches> baseBenchRepository)
        {
            for (int i = 1; i < DatabaseConstants.BenchRecordCount + 1; i++)
            {
                var result = baseBenchRepository.GetById(i);
                if (result is null && result.Id != i) throw new NullReferenceException();
            }
        }
    }
}
