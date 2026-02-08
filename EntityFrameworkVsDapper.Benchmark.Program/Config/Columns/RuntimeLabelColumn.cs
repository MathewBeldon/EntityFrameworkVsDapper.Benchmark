using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace EntityFrameworkVsDapper.Benchmark.Program.Config.Columns
{
    internal sealed class RuntimeLabelColumn : IColumn
    {
        public string Id => nameof(RuntimeLabelColumn);
        public string ColumnName { get; } = "TFM";
        public string Legend => "Target framework label for the job/runtime";

        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
        public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
        {
            return benchmarkCase.Job.Id ?? benchmarkCase.Job.DisplayInfo ?? "default";
        }

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);

        public bool IsAvailable(Summary summary) => true;
        public bool AlwaysShow => true;
        public ColumnCategory Category => ColumnCategory.Job;
        public int PriorityInCategory => -9;
        public bool IsNumeric => false;
        public UnitType UnitType => UnitType.Dimensionless;
        public override string ToString() => ColumnName;
    }
}
