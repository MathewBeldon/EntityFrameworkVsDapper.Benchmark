using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Bench
{
    internal static class GetBenchById
    {
        internal static void GetSingleRecordLoopAll(IBenchRepository benchRepository)
        {
            for (int i = 1; i < DatabaseConstants.RecordCount + 1; i++)
            {
                var result = benchRepository.GetBenchById(i);
                if (result is not null && result.Id != i) throw new NullReferenceException();
            }
        }

        internal static void GetSingleRecord(IBenchRepository benchRepository)
        {
            var result = benchRepository.GetBenchById(1);
            if (result is not null && result.Id != 1) throw new NullReferenceException();
        }        
    }
}
