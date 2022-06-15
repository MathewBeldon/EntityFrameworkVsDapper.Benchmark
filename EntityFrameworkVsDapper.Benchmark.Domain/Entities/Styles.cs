using EntityFrameworkVsDapper.Benchmark.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkVsDapper.Benchmark.Core.Entities
{
    public sealed class Styles : BaseEntity
    {
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public Brands Brand { get; set; }

        public string Name { get; init; }
        public string Description { get; init; }
    }
}
