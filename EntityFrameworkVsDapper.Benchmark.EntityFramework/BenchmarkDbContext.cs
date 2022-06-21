using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework
{
    public sealed class BenchmarkDbContext : DbContext
    {
        public BenchmarkDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(DatabaseConstants.ConnectionString, new MySqlServerVersion(DatabaseConstants.MySqlVersion));

        public DbSet<Benches> Benches { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Styles> Styles { get; set; }
    }
}