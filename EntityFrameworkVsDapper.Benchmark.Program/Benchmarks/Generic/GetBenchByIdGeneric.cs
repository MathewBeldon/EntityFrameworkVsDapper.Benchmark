using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Generic
{
    internal static class GetBenchByIdGeneric
    {
        internal static void GetSingleRecordLoopAll(IBaseRepository<Benches> baseBenchRepository)
        {
            for (int i = 1; i < DatabaseConstants.RecordCount + 1; i++)
            {
                var result = baseBenchRepository.GetById(i);
                if (result is not null && result.Id != i) throw new NullReferenceException();
            }
        }

        internal static void GetSingleRecord(IBaseRepository<Benches> baseBenchRepository)
        {            
            var result = baseBenchRepository.GetById(1);
            if (result is not null && result.Id != 1) throw new NullReferenceException();            
        }
    }
}
