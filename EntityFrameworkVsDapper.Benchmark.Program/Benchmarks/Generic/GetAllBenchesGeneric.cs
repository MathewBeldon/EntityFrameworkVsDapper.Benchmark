using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Generic
{
    internal static class GetAllBenchesGeneric
    {
        internal static void GetAllRecords(IBaseRepository<Benches> baseBenchRepository)
        {
            var result = baseBenchRepository.GetAll();
            if (result.Count() != DatabaseConstants.RecordCount) throw new NullReferenceException();
        }
    }
}
