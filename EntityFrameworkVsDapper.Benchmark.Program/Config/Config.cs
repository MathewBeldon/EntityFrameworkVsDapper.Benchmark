using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;

namespace EntityFrameworkVsDapper.Benchmark.Program.Config
{
    public sealed class CustomConfig : ManualConfig
    {
        public const int Iterations = 500;

        public CustomConfig()
        {
            AddLogger(ConsoleLogger.Default);

            var md = MemoryDiagnoser.Default;
            AddDiagnoser(md);
            //AddColumn(new ORMColum());
            AddColumn(TargetMethodColumn.Method);
            //AddColumn(new ReturnColum());
            AddColumn(StatisticColumn.Mean);
            AddColumn(StatisticColumn.StdDev);
            AddColumn(StatisticColumn.Error);
            AddColumn(BaselineRatioColumn.RatioMean);
            AddColumnProvider(DefaultColumnProviders.Metrics);

            AddJob(Job.ShortRun
                   .WithLaunchCount(1)
                   .WithWarmupCount(2)
                   .WithUnrollFactor(Iterations)
                   .WithIterationCount(10)
            );
            Orderer = new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest);
            Options |= ConfigOptions.JoinSummary;
        }
    }
}
