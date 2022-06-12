using EntityFrameworkVsDapper.Benchmark.Domain.Common;
using EntityFrameworkVsDapper.Benchmark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkVsDapper.Benchmark.Persistence
{
    public sealed class BenchmarkDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options)
           : base(options)
        {
        }

        public DbSet<Benches> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateUtc = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDateUtc = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}