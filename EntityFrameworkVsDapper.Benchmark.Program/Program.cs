// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EntityFrameworkVsDapper.Benchmark.Dapper;
using EntityFrameworkVsDapper.Benchmark.Domain.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Domain.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.Program
{

    [MemoryDiagnoser]
    public class DatabaseBenchmarks 
    {
        private IBaseRepository<Benches> _baseBenchRepository;
        private BenchmarkDbContext _context;
        private BenchmarkDbConnection _connection;

        [GlobalSetup(Target = nameof(EntityFramework))]
        public void GlobalSetupEntityFramework()
        {
            _context = new BenchmarkDbContext(Database.GetOptions());
            _baseBenchRepository = new EntityFramework.Repositories.BaseRepository<Benches>(_context);
        }

        [GlobalCleanup(Target = nameof(EntityFramework))]
        public void GlobalCleanupEntityFramework()
        {
            _context.Dispose();
        }

        [Benchmark]
        public void EntityFramework()
        {
            GetBenchById.OneHundredThousand(_baseBenchRepository);
        }

        [GlobalSetup(Target = nameof(Dapper))]
        public void GlobalSetupDapper()
        {
            _connection = new BenchmarkDbConnection(DatabaseConstants.ConnectionString);
            _baseBenchRepository = new Dapper.Repositories.BaseRepository<Benches>(_connection);
        }

        [GlobalCleanup(Target = nameof(Dapper))]
        public void GlobalCleanupDapper()
        {
            _connection.Dispose();
        }

        [Benchmark]
        public void Dapper()
        {
            GetBenchById.OneHundredThousand(_baseBenchRepository);
        }
    }

    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<DatabaseBenchmarks>();
            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupEntityFramework();
            //benchmark.EntityFramework();
            //benchmark.GlobalCleanupEntityFramework();

            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupDapper();
            //benchmark.Dapper();
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
