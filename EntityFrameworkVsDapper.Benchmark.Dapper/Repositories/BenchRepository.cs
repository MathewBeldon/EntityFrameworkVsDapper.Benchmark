﻿using Dapper;
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

        public void CreateBench(Benches benche)
        {
            throw new NotImplementedException();
        }

        public void DeleteBench(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBench(Benches benche)
        {
            throw new NotImplementedException();
        }        
    }
}
