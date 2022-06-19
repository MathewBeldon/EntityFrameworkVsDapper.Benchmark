using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework
{
    public sealed class BenchmarkDbContext : DbContext
    {
        public BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options)
           : base(options)
        {
        }

        public DbSet<Benches> Benches { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Styles> Styles { get; set; }
    }
}