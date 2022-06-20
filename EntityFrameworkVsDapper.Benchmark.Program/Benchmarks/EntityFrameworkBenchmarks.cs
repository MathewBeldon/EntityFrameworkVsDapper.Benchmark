using BenchmarkDotNet.Attributes;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    [Description("EF")]
    public class EntityFrameworkBenchmarks : BenchmarkBase
    {        
        private BenchmarkDbContext _context;

        [GlobalSetup]
        public void GlobalSetupEntityFramework()
        {
            var builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
            var serverVersion = new MySqlServerVersion(DatabaseConstants.MySqlVersion);
            builder.UseMySql(DatabaseConstants.ConnectionString, serverVersion);
            _context = new BenchmarkDbContext(builder.Options);
            _baseGenericBenchRepository = new EntityFramework.Repositories.Base.BaseRepository<Benches>(_context);
            _benchRepository = new EntityFramework.Repositories.BenchRepository(_context);
        }

        [GlobalCleanup]
        public void GlobalCleanupEntityFramework()
        {
            var delete = _context.Benches.Where(x => x.Id > 10000);
            _context.RemoveRange(delete);
            _context.Dispose();            
        }

        [Benchmark(Description = "Read record T (interface)")]
        public void GenericSingleRecord()
        {
            GenericSingleRecordShared();
        }

        [Benchmark(Description = "Read paged records T (interface)")]
        public void GenericPagedRecords()
        {
            GenericPagedRecordsShared();
        }

        [Benchmark(Description = "Read record (interface)")]
        public void SingleRecord()
        {
            SingleRecordShared();
        }

        [Benchmark(Description = "Read record w/ joins (interface)")]
        public void SingleRecordPopulated()
        {
            SingleRecordPopulatedShared();
        }

        [Benchmark(Description = "Read paged records w/ joins (interface)")]
        public void PagedRecordsPopulated()
        {
            PagedRecordsPopulatedShared();
        }

        [Benchmark(Description = "Create record (interface)")]
        public void CreateRecord()
        {
            CreateRecordShared();
        }

        [Benchmark(Description = "Create then delete record (interface)")]
        public void CreateDeleteRecord()
        {
            CreateDeleteRecordShared();
        }

        [Benchmark(Description = "Create then update record (interface)")]
        public void CreateUpdateRecord()
        {
            CreateUpdateRecordShared();
        }
    }
}
