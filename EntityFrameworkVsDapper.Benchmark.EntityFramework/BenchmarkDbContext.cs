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
                .UseNpgsql("Server=localhost;Port=5432;Database=benchmark;Uid=postgres;Pwd=password;")
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<Benches> Benches { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Styles> Styles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Convert all table names to lowercase
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).ToTable(entity.GetTableName().ToLowerInvariant());
            }
        }
    }
}