using EntityFrameworkVsDapper.Benchmark.Core.Entities.Common;
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

        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
    }
}
