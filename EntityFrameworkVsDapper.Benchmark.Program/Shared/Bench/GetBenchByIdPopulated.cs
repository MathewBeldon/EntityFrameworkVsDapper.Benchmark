using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;

namespace EntityFrameworkVsDapper.Benchmark.Program.Shared.Bench
{
    internal static class GetBenchByIdPopulated
    {
        internal static void GetOneRecordPopulated(IBenchRepository benchRespoitory)
        {
            var result = benchRespoitory.GetBenchByIdPopulated(1);
            if (result is not null && result.Id != 1) throw new NullReferenceException();
        }
    }
}
