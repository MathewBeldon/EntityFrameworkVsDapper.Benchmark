using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;

namespace EntityFrameworkVsDapper.Benchmark.Program.Shared.Bench
{
    internal static class GetBenchById
    {
        internal static void GetOneRecord(IBenchRepository benchRepository)
        {
            var result = benchRepository.GetBenchById(1);
            if (result is not null && result.Id != 1) throw new NullReferenceException();
        }
    }
}
