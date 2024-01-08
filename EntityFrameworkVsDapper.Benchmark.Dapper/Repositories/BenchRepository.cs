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
                  FROM benches
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
                    benches.id,
                    benches.material_id,
                    benches.style_id,
                    benches.name,
                    benches.description,
                    benches.cost,
                    benches.height,
                    benches.width,
                    benches.depth,
                    materials.id,
                    materials.name,
                    materials.description,
                    materials.cost,
                    styles.id,
                    styles.name,
                    styles.description,
                    styles.brand_id,
                    brands.id,
                    brands.name,
                    brands.description
                FROM Benches benches 
                INNER JOIN materials materials on benches.material_id = materials.id
                INNER JOIN styles styles on benches.style_id = styles.id
                INNER JOIN brands brands on styles.brand_id = brands.id
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
                    splitOn: "id",
                    buffered: true
                ).First();
        }

        public IEnumerable<Benches> GetBenchPopulatedPage(int page, int pageSize, int totalCount)
        {
            const string sql =
                @$"
                SELECT 
                    benches.id,
                    benches.material_id,
                    benches.style_id,
                    benches.name,
                    benches.description,
                    benches.cost,
                    benches.height,
                    benches.width,
                    benches.depth,
                    materials.id,
                    materials.name,
                    materials.description,
                    materials.cost,
                    styles.id,
                    styles.name,
                    styles.description,
                    styles.brand_id,
                    brands.id,
                    brands.name,
                    brands.description
                FROM benches benches 
                INNER JOIN materials materials on benches.material_id = materials.id
                INNER JOIN styles styles on benches.style_id = styles.id
                INNER JOIN brands brands on styles.brand_id = brands.id
                WHERE benches.id <= (@TotalCount - (@PageSize * (@Page - 1)))
                ORDER BY benches.id DESC
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
                        material_id,
                        style_id,
                        name,
                        description,
                        cost,
                        height,
                        width,
                        depth,
                        created_date_utc
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
                        material_id = @MaterialId,
                        style_id = @StyleId,
                        name = @Name,
                        description = @Description,
                        cost = @Cost,
                        height = @Height,
                        width = @Width,
                        depth = @Depth,
                        modified_date_utc = UTC_TIMESTAMP()
                    WHERE id = @Id";

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
                    DELETE FROM benches
                    WHERE id = @Id";


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
