using Dapper;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Dapper.Repositories.Base;

namespace EntityFrameworkVsDapper.Benchmark.Dapper.Repositories
{
    public sealed class BenchRepository : BaseRepository<Benches>, IBenchRepository
    {
        public BenchRepository(BenchmarkDbConnection context) : base(context) { }
                
        public Benches GetBench(int id)
        {
            const string sql =
                @$"SELECT * 
                  FROM Benches
                  WHERE id = @Id";

            var commandDefinition = new CommandDefinition(sql, new
            {
                Id = id,
            }, flags: CommandFlags.Buffered);

            return _context.connection.Query<Benches>(commandDefinition).First();
        }

        public Benches GetBenchPopulated(int id)
        {
            const string sql = 
                @$"
                SELECT 
                    benches.Id,
                    benches.MaterialId,
                    benches.StyleId,
                    benches.Name,
                    benches.Description,
                    benches.Cost,
                    benches.Height,
                    benches.Width,
                    benches.Depth,
                    materials.Id,
                    materials.Name,
                    materials.Description,
                    materials.Cost,
                    styles.Id,
                    styles.Name,
                    styles.Description,
                    styles.BrandId,
                    brands.Id,
                    brands.Name,
                    brands.Description
                FROM Benches benches 
                INNER JOIN Materials materials on benches.MaterialId = materials.Id
                INNER JOIN Styles styles on benches.StyleId = styles.Id
                INNER JOIN Brands brands on styles.BrandId = brands.Id
                WHERE benches.id = @Id";

            return _context.connection.Query<Benches, Materials, Styles, Brands, Benches>
                (
                    sql: sql,
                    map: (benches, materials, styles, brands) =>
                    {
                        benches.Material = materials;
                        benches.Style = styles;
                        benches.Style.Brand  = brands;
                        return benches;
                    },
                    param: new { Id = id }, 
                    splitOn: "Id",
                    buffered: true
                ).First();
        }

        public IEnumerable<Benches> GetBenchPopulatedPage(int page, int pageSize, int totalCount)
        {
            const string sql =
                @$"
                SELECT 
                    benches.Id,
                    benches.MaterialId,
                    benches.StyleId,
                    benches.Name,
                    benches.Description,
                    benches.Cost,
                    benches.Height,
                    benches.Width,
                    benches.Depth,
                    materials.Id,
                    materials.Name,
                    materials.Description,
                    materials.Cost,
                    styles.Id,
                    styles.Name,
                    styles.Description,
                    styles.BrandId,
                    brands.Id,
                    brands.Name,
                    brands.Description
                FROM Benches benches 
                INNER JOIN Materials materials on benches.MaterialId = materials.Id
                INNER JOIN Styles styles on benches.StyleId = styles.Id
                INNER JOIN Brands brands on styles.BrandId = brands.Id
                WHERE benches.Id <= (@TotalCount - (@PageSize * (@Page - 1)))
                ORDER BY benches.Id DESC
                LIMIT @PageSize";

            return _context.connection.Query<Benches, Materials, Styles, Brands, Benches>
                (
                    sql: sql,
                    map: (benches, materials, styles, brands) =>
                    {
                        benches.Material = materials;
                        benches.Style = styles;
                        benches.Style.Brand = brands;
                        return benches;
                    },
                    param: new {
                        Page = page,
                        PageSize = pageSize,
                        TotalCount = totalCount
                    },
                    splitOn: "Id",
                    buffered: true
                );
        }

        public Benches CreateBench(Benches bench)
        {
            const string sql =
                @$"
                    INSERT INTO Benches
                    (
                        MaterialId,
                        StyleId,
                        Name,
                        Description,
                        Cost,
                        Height,
                        Width,
                        Depth,
                        CreatedDateUtc
                    )
                    VALUES
                    (
                        @MaterialId,
                        @StyleId,
                        @Name,
                        @Description,
                        @Cost,
                        @Height,
                        @Width,
                        @Depth,
                        @CreatedDateUtc
                    );
                    SELECT LAST_INSERT_ID();";
                    

            var commandDefinition = new CommandDefinition(sql, new
            {
                MaterialId = bench.MaterialId,
                StyleId = bench.StyleId,
                Name = bench.Name,
                Description = bench.Description,
                Cost = bench.Cost,
                Height = bench.Height,
                Width = bench.Width,
                Depth = bench.Depth,
                CreatedDateUtc = DateTime.UtcNow
            });

            _context.connection.Open();
            using (var transaction = _context.connection.BeginTransaction())
            {
                try
                {                    
                    bench.Id = transaction.Connection.Query<int>(commandDefinition).First();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.connection.Close();
                }
            }

            return bench;
        }

        public void UpdateBench(Benches bench)
        {
            const string sql =
                @$"
                    UPDATE Benches
                    SET
                        MaterialId = @MaterialId,
                        StyleId = @StyleId,
                        Name = @Name,
                        Description = @Description,
                        Cost = @Cost,
                        Height = @Height,
                        Width = @Width,
                        Depth = @Depth,
                        ModifiedDateUtc = UTC_TIMESTAMP()
                    WHERE Id = @Id";

            var commandDefinition = new CommandDefinition(sql, new
            {
                Id = bench.Id,
                MaterialId = bench.MaterialId,
                StyleId = bench.StyleId,
                Name = bench.Name,
                Description = bench.Description,
                Cost = bench.Cost,
                Height = bench.Height,
                Width = bench.Width,
                Depth = bench.Depth,
                CreatedDateUtc = DateTime.UtcNow
            });

            _context.connection.Open();
            using (var transaction = _context.connection.BeginTransaction())
            {
                try
                {
                    bench.Id = transaction.Connection.Execute(commandDefinition);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.connection.Close();
                }
            }
        }

        public void DeleteBench(int id)
        {
            const string sql =
                @$"
                    DELETE FROM Benches
                    WHERE Id = @Id";


            var commandDefinition = new CommandDefinition(sql, new
            {
                Id = id
            });

            _context.connection.Open();
            using (var transaction = _context.connection.BeginTransaction())
            {
                try
                {
                    transaction.Connection.Execute(commandDefinition);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    _context.connection.Close();
                }
            }
        }      
    }
}
