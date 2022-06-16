using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories.Base;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories
{
    public sealed class BenchRepository : BaseRepository<Benches>, IBenchRepository
    {
        public BenchRepository(BenchmarkDbContext context) : base(context) { }

        public Benches GetBenchById(int id)
        {
            return _context.Benches.Single(x => x.Id == id);
        }

        public Benches GetBenchByIdPopulated(int id)
        {
            return _context.Benches
                .Join(_context.Materials,
                    bench => bench.MaterialId,
                    material => material.Id,
                    (bench, material) => new Benches
                    {
                        Id = bench.Id,
                        MaterialId = material.Id,
                        Material = new Materials
                        {
                            Id = material.Id,
                            Name = material.Name,
                            Description = material.Description,
                            Cost = material.Cost
                        },
                        StyleId = bench.StyleId,
                        Name = bench.Name,
                        Description = bench.Description,
                        Cost = bench.Cost,
                        Height = bench.Height,
                        Width = bench.Width,
                        Depth = bench.Depth
                    }
                )
                .Join(_context.Styles,
                    bench => bench.StyleId,
                    style => style.Id,
                    (bench, style) => new Benches
                    {
                        Id = bench.Id,
                        MaterialId = bench.MaterialId,
                        Material = new Materials
                        {
                            Id = bench.Material.Id,
                            Name = bench.Material.Name,
                            Description = bench.Material.Description,
                            Cost = bench.Material.Cost
                        },
                        StyleId = style.Id,
                        Style = new Styles
                        {
                            Id = style.Id,
                            BrandId = style.BrandId,
                            Name = style.Name,
                            Description = style.Description
                        },
                        Name = bench.Name,
                        Description = bench.Description,
                        Cost = bench.Cost,
                        Height = bench.Height,
                        Width = bench.Width,
                        Depth = bench.Depth
                    }
                )
                .Join(_context.Brands,
                    bench => bench.Style.BrandId,
                    brand => brand.Id,
                    (bench, brand) => new Benches
                    {
                        Id = bench.Id,
                        MaterialId = bench.MaterialId,
                        Material = new Materials
                        {
                            Id = bench.Material.Id,
                            Name = bench.Material.Name,
                            Description = bench.Material.Description,
                            Cost = bench.Material.Cost
                        },
                        StyleId = bench.StyleId,
                        Style = new Styles
                        {
                            Id = bench.StyleId,
                            BrandId = brand.Id,
                            Brand = new Brands
                            {
                                Id = brand.Id,
                                Name = brand.Name,
                                Description = brand.Description
                            }
                        },
                        Name = bench.Name,
                        Description = bench.Description,
                        Cost = bench.Cost,
                        Height = bench.Height,
                        Width = bench.Width,
                        Depth = bench.Depth
                    }
                ).Single(x => x.Id == id);
        }
    }
}
