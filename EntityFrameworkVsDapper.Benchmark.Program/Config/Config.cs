using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using EntityFrameworkVsDapper.Benchmark.Program.Config.Columns;

namespace EntityFrameworkVsDapper.Benchmark.Program.Config
{
    public sealed class CustomConfig : ManualConfig
    {
        public CustomConfig()
        {
            AddLogger(ConsoleLogger.Default);

            AddExporter(HtmlExporter.Default);

            var md = MemoryDiagnoser.Default;
            AddDiagnoser(md);
            AddColumn(new ORMColumn());
            AddColumn(TargetMethodColumn.Method);
            AddColumn(StatisticColumn.Mean);
            AddColumn(StatisticColumn.Min);
            AddColumn(StatisticColumn.Max);
            AddColumnProvider(DefaultColumnProviders.Metrics);

            AddJob(Job.LongRun
                   .WithLaunchCount(3)
                   .WithWarmupCount(10)
                   .WithUnrollFactor(1)
                   .WithIterationCount(20)
            );

            Orderer = new DefaultOrderer(SummaryOrderPolicy.Method);
            Options |= ConfigOptions.JoinSummary;
        }
    }
}
