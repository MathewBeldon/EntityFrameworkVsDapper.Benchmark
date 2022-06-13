// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EntityFrameworkVsDapper.Benchmark.EntityFramework;
using Microsoft.Extensions.Configuration;
using EntityFrameworkVsDapper.Benchmark.Domain.Entities;
using EntityFrameworkVsDapper.Benchmark.Domain.Contracts.Repository;
using System.ComponentModel.Design;
using EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories;
using Microsoft.Extensions.Hosting.Internal;

namespace EntityFrameworkVsDapper.Benchmark.Program
{
    [MemoryDiagnoser]
    public class DatabaseBenchmarks 
    {

        //[Benchmark]
        //public void Dapper()
        //{
        //    throw new NotImplementedException();
        //}

        [Benchmark]
        public void EntityFramework()
        {
            using (var context = new BenchmarkDbContext(Database.GetOptions()))
            {
                IBaseRepository baseRepository = new BaseRepository(context);
                for (int i = 0; i < 10000; i++)
                {
                    _ = baseRepository.GetById(i);
                }
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DatabaseBenchmarks>();
        }
    }

    public class Database
    {
        public static DbContextOptions<BenchmarkDbContext> GetOptions()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
            var serverVersion = new MySqlServerVersion("8.0.0");
            builder.UseMySql("Server=localhost;Database=ef_dapper_benchmark;Uid=sa;Pwd=MyPassword;", serverVersion);

            return (DbContextOptions<BenchmarkDbContext>)builder.Options;
        }
    }

    
}
