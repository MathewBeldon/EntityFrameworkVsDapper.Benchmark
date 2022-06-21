using BenchmarkDotNet.Attributes;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using System.ComponentModel;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    [Description("EF")]
    public class EntityFrameworkBenchmarks : BenchmarkBase
    {
        [GlobalSetup]
        public void GlobalSetupEntityFramework()
        {
            _baseGenericBenchRepository = new EntityFramework.Repositories.Base.BaseRepository<Benches>();
            _benchRepository = new EntityFramework.Repositories.BenchRepository();
        }

        [GlobalCleanup]
        public void GlobalCleanupEntityFramework()
        {
            using (var context = new BenchmarkDbContext())
            {
                var delete = context.Benches.Where(x => x.Id > 10000);
                context.RemoveRange(delete);
            }
        }

        [Benchmark(Description = "Read record T")]
        public void GenericSingleRecord()
        {
            GenericSingleRecordShared();
        }

        [Benchmark(Description = "Read paged records T")]
        public void GenericPagedRecords()
        {
            GenericPagedRecordsShared();
        }

        [Benchmark(Description = "Read record")]
        public void SingleRecord()
        {
            SingleRecordShared();
        }

        [Benchmark(Description = "Read record w/ joins")]
        public void SingleRecordPopulated()
        {
            SingleRecordPopulatedShared();
        }

        [Benchmark(Description = "Read paged records w/ joins")]
        public void PagedRecordsPopulated()
        {
            PagedRecordsPopulatedShared();
        }

        [Benchmark(Description = "Create record")]
        public void CreateRecord()
        {
            CreateRecordShared();
        }

        [Benchmark(Description = "Create then delete record")]
        public void CreateDeleteRecord()
        {
            CreateDeleteRecordShared();
        }

        [Benchmark(Description = "Create then update record")]
        public void CreateUpdateRecord()
        {
            CreateUpdateRecordShared();
        }
    }
}
