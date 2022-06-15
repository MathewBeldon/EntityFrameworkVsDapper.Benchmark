// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks.Generic;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.Program
{

    [MemoryDiagnoser]
    public class DatabaseBenchmarks
    {
        private IBaseRepository<Benches> _baseGenericBenchRepository;
        private BenchmarkDbContext _context;
        private BenchmarkDbConnection _connection;

        #region EF Setup/Cleanup

        [GlobalSetup(Targets = new[]
        {
            nameof(EntityFrameworkSingleRecord),
            nameof(EntityFrameworkSingleRecordLoopAll)
        })]
        public void GlobalSetupEntityFramework()
        {
            _context = new BenchmarkDbContext(Database.GetOptions());
            _baseGenericBenchRepository = new EntityFramework.Repositories.BaseRepository<Benches>(_context);
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
            nameof(DapperSingleRecord),
            nameof(DapperSingleRecordLoopAll)
        })]
        public void GlobalSetupDapper()
        {
            _connection = new BenchmarkDbConnection(DatabaseConstants.ConnectionString);
            _baseGenericBenchRepository = new Dapper.Repositories.BaseRepository<Benches>(_connection);
        }

        [GlobalCleanup(Target = nameof(Dapper))]
        public void GlobalCleanupDapper()
        {
            _connection.Dispose();
        }
        #endregion Dapper Setup/Cleanup

        #region Get Bench By Id Generic Single Record

        [Benchmark]
        public void EntityFrameworkSingleRecord()
        {
            GetBenchByIdGeneric.GetSingleRecord(_baseGenericBenchRepository);
        }
        
        [Benchmark]
        public void DapperSingleRecord()
        {
            GetBenchByIdGeneric.GetSingleRecord(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void EntityFrameworkSingleRecordLoopAll()
        {
            GetBenchByIdGeneric.GetSingleRecordLoopAll(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void DapperSingleRecordLoopAll()
        {
            GetBenchByIdGeneric.GetSingleRecordLoopAll(_baseGenericBenchRepository);
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
            //benchmark.EntityFrameworkSingleRecord();
            //benchmark.GlobalCleanupEntityFramework();

            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupDapper();
            //benchmark.DapperSingleRecord();
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
