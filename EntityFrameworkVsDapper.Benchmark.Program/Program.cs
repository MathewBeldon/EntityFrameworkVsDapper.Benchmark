// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks;
using EntityFrameworkVsDapper.Benchmark.Program.Config;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using EntityFrameworkVsDapper.Benchmark.Program.Shared.Bench;
using EntityFrameworkVsDapper.Benchmark.Program.Shared.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.Program
{
    public class Program
    {
        public static void Main()
        {
            _ = BenchmarkRunner.Run(typeof(BenchmarkBase).Assembly, new CustomConfig());
            //new BenchmarkSwitcher(typeof(BenchmarkBase).Assembly).Run(new string[0], new CustomConfig());
            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupEntityFramework();
            //benchmark.EntityFramework_Bench_OneRecordPopulated();
            //benchmark.GlobalCleanupEntityFramework();

            //var benchmark = new DatabaseBenchmarks();
            //benchmark.GlobalSetupDapper();
            //benchmark.Dapper_Bench_OneRecordPopulated();
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
