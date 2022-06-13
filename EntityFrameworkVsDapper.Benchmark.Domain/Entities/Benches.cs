using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkVsDapper.Benchmark.Domain.Entities
{
    public sealed class Benches
    {
        [Key]
        public int BenchId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Cost { get; init; }
        public int Height { get; init; }
        public int Width { get; init; }
        public int Depth { get; init; }
    }
}
