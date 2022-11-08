using BenchmarkDotNet.Running;
using EntityFrameworkVsDapper.Benchmark.Program.Benchmarks;
using EntityFrameworkVsDapper.Benchmark.Program.Config;

namespace EntityFrameworkVsDapper.Benchmark.Program
{
    public class Program
    {
        public static void Main()
        {
            //_ = BenchmarkRunner.Run(typeof(BenchmarkBase).Assembly, new CustomConfig());
            //new BenchmarkSwitcher(typeof(BenchmarkBase).Assembly).Run(new string[0], new CustomConfig());

            var benchmark = new EntityFrameworkBenchmarks();
            benchmark.GlobalSetupEntityFramework();
            benchmark.GenericPagedRecords();
            benchmark.GlobalCleanupEntityFramework();

            //var benchmark = new DapperBenchmarks();
            //benchmark.GlobalSetupDapper();
            //benchmark.CreateRecord();
            //benchmark.GlobalCleanupDapper();
        }
    } 
}
