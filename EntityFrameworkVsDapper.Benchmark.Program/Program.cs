using BenchmarkDotNet.Running;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks;
using EntityFrameworkVsDapper.Benchmark.Program.Config;

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
}
