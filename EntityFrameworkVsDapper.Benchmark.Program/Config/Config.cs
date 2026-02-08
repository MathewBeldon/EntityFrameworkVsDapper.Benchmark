using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
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
            AddColumn(new RuntimeLabelColumn());
            AddColumn(TargetMethodColumn.Method);
            AddColumn(StatisticColumn.Mean);
            AddColumn(StatisticColumn.Min);
            AddColumn(StatisticColumn.Max);
            AddColumnProvider(DefaultColumnProviders.Metrics);

            AddJob(Job.LongRun
                   .WithRuntime(CoreRuntime.Core60)
                   .WithId("net6")
                   .WithLaunchCount(3)
                   .WithWarmupCount(10)
                   .WithUnrollFactor(1)
                   .WithIterationCount(20)
            );

            AddJob(Job.LongRun
                   .WithRuntime(CoreRuntime.Core80)
                   .WithId("net8")
                   .WithLaunchCount(3)
                   .WithWarmupCount(10)
                   .WithUnrollFactor(1)
                   .WithIterationCount(20)
            );

#if NET10_0
            AddJob(Job.LongRun
                   .WithId("net10")
                   .WithLaunchCount(3)
                   .WithWarmupCount(10)
                   .WithUnrollFactor(1)
                   .WithIterationCount(20)
            );
#endif

            Orderer = new DefaultOrderer(SummaryOrderPolicy.Method);
            Options |= ConfigOptions.JoinSummary;
        }
    }
}
