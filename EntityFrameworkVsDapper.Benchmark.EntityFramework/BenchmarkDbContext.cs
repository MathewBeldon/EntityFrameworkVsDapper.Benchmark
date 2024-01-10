using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework
{
    public sealed class BenchmarkDbContext : DbContext
    {
        public BenchmarkDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(Environment.GetEnvironmentVariable("BENCHMARK_CONNECTION_STRING"))
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<Benches> Benches { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Styles> Styles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}