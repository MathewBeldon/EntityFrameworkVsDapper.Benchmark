using EntityFrameworkVsDapper.Benchmark.Core.Entities.Common;

namespace EntityFrameworkVsDapper.Benchmark.Core.Entities
{
    public sealed class Brands : BaseEntity
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
