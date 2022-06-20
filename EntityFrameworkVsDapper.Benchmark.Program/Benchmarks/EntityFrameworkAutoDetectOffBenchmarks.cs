using BenchmarkDotNet.Attributes;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    [Description("EF (No Tracking)")]
    public class EntityFrameworkAutoDetectOffBenchmarks : BenchmarkBase
    {
        private BenchmarkDbContext _context;

        [GlobalSetup]
        public void GlobalSetupEntityFramework()
        {
            var serverVersion = new MySqlServerVersion(DatabaseConstants.MySqlVersion);

            var builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
            builder.UseMySql(DatabaseConstants.ConnectionString, serverVersion);
            _context = new BenchmarkDbContext(builder.Options);
            _baseGenericBenchRepository = new EntityFramework.Repositories.Base.BaseRepository<Benches>(_context);
            _benchRepository = new EntityFramework.Repositories.BenchRepository(_context);
        }

        [GlobalCleanup]
        public void GlobalCleanupEntityFramework()
        {
            var delete = _context.Benches.Where(x => x.Id > 10000);
            _context.RemoveRange(delete);
            _context.Dispose();
        }

        [Benchmark(Description = "Create record (AutoDetectChanges Off)")]
        public void CreateRecordAutoDetectChangesOff()
        {
            var bench = new Benches
            {
                MaterialId = random.Next(1, 10001),
                StyleId = random.Next(1, 10001),
                Name = "Created Name",
                Description = "Created Description",
                Cost = random.Next(1, 1000),
                Height = random.Next(1, 1000),
                Width = random.Next(1, 1000),
                Depth = random.Next(1, 1000)
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;
                    _context.Benches.Add(bench);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }

        [Benchmark(Description = "Create then delete record (AutoDetectChanges Off)")]
        public void CreateDeleteRecordAutoDetectChangesOff()
        {
            var bench = new Benches
            {
                MaterialId = random.Next(1, 10001),
                StyleId = random.Next(1, 10001),
                Name = "Created Name",
                Description = "Created Description",
                Cost = random.Next(1, 1000),
                Height = random.Next(1, 1000),
                Width = random.Next(1, 1000),
                Depth = random.Next(1, 1000)
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;
                    _context.Benches.Add(bench);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;
                    _context.Remove(_context.Benches.First(x => x.Id == bench.Id));
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }

        [Benchmark(Description = "Create then update record (AutoDetectChanges Off)")]
        public void CreateUpdateRecordAutoDetectChangesOff()
        {
            var bench = new Benches
            {
                MaterialId = random.Next(1, 10001),
                StyleId = random.Next(1, 10001),
                Name = "Created Name",
                Description = "Created Description",
                Cost = random.Next(1, 1000),
                Height = random.Next(1, 1000),
                Width = random.Next(1, 1000),
                Depth = random.Next(1, 1000)
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;
                    _context.Benches.Add(bench);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }

            bench.MaterialId = random.Next(1, 10001);
            bench.StyleId = random.Next(1, 10001);
            bench.Name = "Created Name New";
            bench.Description = "Created Description New";
            bench.Cost = random.Next(1, 1000);
            bench.Height = random.Next(1, 1000);
            bench.Width = random.Next(1, 1000);
            bench.Depth = random.Next(1, 1000);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;
                    _context.Update(bench);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }
    }
}
