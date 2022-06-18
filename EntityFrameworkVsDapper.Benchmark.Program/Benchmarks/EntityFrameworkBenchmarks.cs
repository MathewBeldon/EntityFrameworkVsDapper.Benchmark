using BenchmarkDotNet.Attributes;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program.Shared.Bench;
using EntityFrameworkVsDapper.Benchmark.Program.Shared.Generic;
using System.ComponentModel;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    [Description("Entity Framework")]
    public class EntityFrameworkBenchmarks : BenchmarkBase
    {        
        private BenchmarkDbContext _context;

        [GlobalSetup]
        public void GlobalSetupEntityFramework()
        {
            _context = new BenchmarkDbContext(Database.GetOptions());
            _baseGenericBenchRepository = new EntityFramework.Repositories.Base.BaseRepository<Benches>(_context);
            _benchRepository = new EntityFramework.Repositories.BenchRepository(_context);
        }

        [GlobalCleanup]
        public void GlobalCleanupEntityFramework()
        {
            _context.Dispose();
        }

        [Benchmark]
        public void EntityFramework_Generic_OneRecord()
        {
            GetBenchByIdGeneric.GetOneRecord(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void EntityFramework_Generic_AllRecords()
        {
            GetAllBenchesGeneric.GetAllRecords(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void EntityFramework_Bench_OneRecord()
        {
            GetBenchById.GetOneRecord(_benchRepository);
        }

        [Benchmark]
        public void EntityFramework_Bench_OneRecordPopulated()
        {
            GetBenchByIdPopulated.GetOneRecordPopulated(_benchRepository);
        }

    }
}
