using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Bench
{
    internal static class GetBenchByIdPopulated
    {
        internal static void GetSingleRecordPopulated(IBenchRepository benchRespoitory)
        {
            var result = benchRespoitory.GetBenchByIdPopulated(1);
            if (result is not null && result.Id != 1) throw new NullReferenceException();
        }

        internal static void GetSingleRecordPopulatedLoopAll(IBenchRepository benchRepository)
        {
            for (int i = 1; i < DatabaseConstants.RecordCount + 1; i++)
            {
                var result = benchRepository.GetBenchByIdPopulated(i);
                if (result is not null && result.Id != i) throw new NullReferenceException();
            }
        }
    }
}
