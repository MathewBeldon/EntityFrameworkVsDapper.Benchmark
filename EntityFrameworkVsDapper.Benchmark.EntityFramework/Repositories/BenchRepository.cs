using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories
{
    public sealed class BenchRepository : BaseRepository<Benches>, IBenchRepository
    {
        public BenchRepository(BenchmarkDbContext context) : base(context) { }

        private static readonly Func<BenchmarkDbContext, int, Benches> GetBenchCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int id) => context.Benches.First(p => p.Id == id));
        public Benches GetBench(int id)
        {
            return GetBenchCompiled(_context, id);
        }

        private static readonly Func<BenchmarkDbContext, int, Benches> GetBenchPopulatedCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int id) => 
                context
                    .Benches
                    .Join(context.Materials,
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
                    .Join(context.Styles,
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
                    .Join(context.Brands,
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
                    ).First(x => x.Id == id));
        public Benches GetBenchPopulated(int id)
        {
            return GetBenchPopulatedCompiled(_context, id);
        }

        private static readonly Func<BenchmarkDbContext, int, int, int, IEnumerable<Benches>> GetBenchPopulatedPagedCompiled =
            EF.CompileQuery((BenchmarkDbContext context, int page, int pageSize, int totalCount) =>
                context
                    .Benches
                    .Join(context.Materials,
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
                    .Join(context.Styles,
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
                    .Join(context.Brands,
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
                    )
                    .OrderByDescending(x => x.Id)
                    .Where(a => a.Id <= totalCount - (pageSize * (page - 1)))
                    .Take(pageSize));
        public IEnumerable<Benches> GetBenchPopulatedPage(int page, int pageSize, int totalCount)
        {
            return GetBenchPopulatedPagedCompiled(_context, page, pageSize, totalCount);
        }

        public Benches CreateBench(Benches bench)
        {            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Benches.Add(bench);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return bench;
        }

        public void UpdateBench(Benches bench)
        {
            throw new NotImplementedException();
        }

        public void DeleteBench(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {                   
                    _context.Remove(_context.Benches.First(x => x.Id == id));
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
