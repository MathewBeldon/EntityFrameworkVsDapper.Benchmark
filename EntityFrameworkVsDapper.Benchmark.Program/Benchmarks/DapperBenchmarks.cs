using BenchmarkDotNet.Attributes;
using Dapper;
using EntityFrameworkVsDapper.Benchmark.Core.Constants;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper;
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
