using BenchmarkDotNet.Attributes;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using EntityFrameworkVsDapper.Benchmark.Program.Shared.Bench;
using EntityFrameworkVsDapper.Benchmark.Program.Shared.Generic;
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
            _connection.Dispose();
        }

        [Benchmark]
        public void Dapper_Generic_OneRecord()
        {
            GetBenchByIdGeneric.GetOneRecord(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void Dapper_Generic_AllRecords()
        {
            GetAllBenchesGeneric.GetAllRecords(_baseGenericBenchRepository);
        }

        [Benchmark]
        public void Dapper_Bench_OneRecord()
        {
            GetBenchById.GetOneRecord(_benchRepository);
        }

        [Benchmark]
        public void Dapper_Bench_OneRecordPopulated()
        {
            GetBenchByIdPopulated.GetOneRecordPopulated(_benchRepository);
        }
    }
}
