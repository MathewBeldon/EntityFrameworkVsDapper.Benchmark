using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;

namespace EntityFrameworkVsDapper.Benchmark.Program.Shared.Generic
{
    internal static class GetBenchByIdGeneric
    {
        internal static void GetOneRecord(IBaseRepository<Benches> baseBenchRepository)
        {
            var result = baseBenchRepository.GetById(1);
            if (result is not null && result.Id != 1) throw new NullReferenceException();
        }
    }
}
