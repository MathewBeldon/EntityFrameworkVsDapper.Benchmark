// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Bench;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Generic;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.Program
{

    [MemoryDiagnoser]
    public class DatabaseBenchmarks
    {
        private IBaseRepository<Benches> _baseGenericBenchRepository;
        private IBenchRepository _benchRepository;
        
        private BenchmarkDbContext _context;
        private BenchmarkDbConnection _connection;

        #region EF Setup/Cleanup

        [GlobalSetup(Targets = new[]
        {
            nameof(EntityFramework_Generic_SingleRecord),
            nameof(EntityFramework_Generic_SingleRecordLoopAll),
            nameof(EntityFramework_Generic_AllRecords),
            nameof(EntityFramework_Bench_SingleRecord),
            nameof(EntityFramework_Bench_SingleRecordLoopAll),
            nameof(EntityFramework_Bench_SingleRecordPopulated)
        })]
        public void GlobalSetupEntityFramework()
        {
            _context = new BenchmarkDbContext(Database.GetOptions());
            _baseGenericBenchRepository = new EntityFramework.Repositories.Base.BaseRepository<Benches>(_context);
            _benchRepository = new EntityFramework.Repositories.BenchRepository(_context);
        }

        [GlobalCleanup(Target = nameof(EntityFramework))]
        public void GlobalCleanupEntityFramework()
        {
            _context.Dispose();
        }
        #endregion

        #region Dapper Setup/Cleanup
        [GlobalSetup(Targets = new[]
        {
            nameof(Dapper_Generic_SingleRecord),
            nameof(Dapper_Generic_SingleRecordLoopAll),
            nameof(Dapper_Generic_AllRecords),
            nameof(Dapper_Bench_SingleRecord),
            nameof(Dappper_Bench_SingleRecordLoopAll),
            nameof(Dapper_Bench_SingleRecordPopulated)
        })]
        public void GlobalSetupDapper()
        {
            _connection = new BenchmarkDbConnection(DatabaseConstants.ConnectionString);
            _baseGenericBenchRepository = new Dapper.Repositories.Base.BaseRepository<Benches>(_connection);
            _benchRepository = new Dapper.Repositories.BenchRepository(_connection);
        }

        [GlobalCleanup(Target = nameof(Dapper))]
        public void GlobalCleanupDapper()
        {
            _connection.Dispose();
        }
        #endregion Dapper Setup/Cleanup

        #region Generic

        [Benchmark]
        public void EntityFramework_Generic_SingleRecord()
        {
            GetBenchByIdGeneric.GetSingleRecord(_baseGenericBenchRepository);
        }
        
        [Benchmark]
        public void Dapper_Generic_SingleRecord()
        {
            GetBenchByIdGeneric.GetSingleRecord(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void EntityFramework_Generic_SingleRecordLoopAll()
        {
            GetBenchByIdGeneric.GetSingleRecordLoopAll(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void Dapper_Generic_SingleRecordLoopAll()
        {
            GetBenchByIdGeneric.GetSingleRecordLoopAll(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void EntityFramework_Generic_AllRecords()
        {
            GetAllBenchesGeneric.GetAllRecords(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void Dapper_Generic_AllRecords()
        {
            GetAllBenchesGeneric.GetAllRecords(_baseGenericBenchRepository);
        }

        #endregion

        #region Bench

        [Benchmark]
        public void EntityFramework_Bench_SingleRecord()
        {
            GetBenchById.GetSingleRecord(_benchRepository);
        }

        [Benchmark]
        public void Dapper_Bench_SingleRecord()
        {
            GetBenchById.GetSingleRecord(_benchRepository);
        }

        [Benchmark]
        public void EntityFramework_Bench_SingleRecordLoopAll()
        {
            GetBenchById.GetSingleRecordLoopAll(_benchRepository);
        }

        [Benchmark]
        public void Dappper_Bench_SingleRecordLoopAll()
        {
            GetBenchById.GetSingleRecordLoopAll(_benchRepository);
        }

        [Benchmark]
        public void EntityFramework_Bench_SingleRecordPopulated()
        {
            GetBenchById.GetSingleRecordPopulated(_benchRepository);
        }

        [Benchmark]
        public void Dapper_Bench_SingleRecordPopulated()
        {
            GetBenchById.GetSingleRecordPopulated(_benchRepository);
        }

        #endregion
    }

    public class Program
    {
        public static void Main()
        {
            _ = BenchmarkRunner.Run<DatabaseBenchmarks>();
            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupEntityFramework();
            //benchmark.EntityFramework_Bench_SingleRecordPopulated();
            //benchmark.GlobalCleanupEntityFramework();

            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupDapper();
            //benchmark.Dapper_Bench_SingleRecordPopulated();
            //benchmark.GlobalCleanupDapper();
        }
    }

    public class Database
    {
        public static DbContextOptions<BenchmarkDbContext> GetOptions()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
            var serverVersion = new MySqlServerVersion(DatabaseConstants.MySqlVersion);
            builder.UseMySql(DatabaseConstants.ConnectionString, serverVersion);

            return (DbContextOptions<BenchmarkDbContext>)builder.Options;
        }
    }

    
}
