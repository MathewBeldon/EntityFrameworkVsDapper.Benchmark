using EntityFrameworkVsDapper.Benchmark.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkVsDapper.Benchmark.Core.Entities
{
    public sealed class Benches : BaseEntity
    {
        [ForeignKey(nameof(Material))]
        public int MaterialId { get; set; }
        public Materials Material { get; set; }

        [ForeignKey(nameof(Style))]
        public int StyleId { get; set; }
        public Styles Style { get; set; }

        public string Name { get; init; }
        public string Description { get; init; }
        public int Cost { get; init; }
        public int Height { get; init; }
        public int Width { get; init; }
        public int Depth { get; init; }
    }
}
