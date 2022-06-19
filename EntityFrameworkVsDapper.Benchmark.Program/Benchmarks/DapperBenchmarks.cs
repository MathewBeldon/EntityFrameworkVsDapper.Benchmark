using BenchmarkDotNet.Attributes;
using Dapper;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using System.ComponentModel;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    [Description("Dapper")]
    public class DapperBenchmarks : BenchmarkBase
    {
        private BenchmarkDbConnection _connection;

        [GlobalSetup]
        public void GlobalSetupDapper()
        {
            _connection = new BenchmarkDbConnection(DatabaseConstants.ConnectionString);
            _baseGenericBenchRepository = new Dapper.Repositories.Base.BaseRepository<Benches>(_connection);
            _benchRepository = new Dapper.Repositories.BenchRepository(_connection);
        }

        [GlobalCleanup]
        public void GlobalCleanupDapper()
        {
            _connection.connection.Execute("DELETE FROM Benches WHERE Id > 10000");
            _connection.Dispose();
        }

        [Benchmark(Description = "Single record <T> (interface)")]
        public void GenericSingleRecord()
        {
            GenericSingleRecordShared();
        }

        [Benchmark(Description = "Paged records <T> (interface)")]
        public void GenericPagedRecords()
        {
            GenericPagedRecordsShared();
        }

        [Benchmark(Description = "Single record (interface)")]
        public void SingleRecord()
        {
            SingleRecordShared();
        }

        [Benchmark(Description = "Single record w/ joins (interface)")]
        public void SingleRecordPopulated()
        {
            SingleRecordPopulatedShared();
        }

        [Benchmark(Description = "Paged records w/ joins (interface)")]
        public void PagedRecordsPopulated()
        {
            PagedRecordsPopulatedShared();
        }

        [Benchmark(Description = "Create record w/ transactions (interface)")]
        public void CreateRecord()
        {
            CreateRecordShared();
        }
    }
}
