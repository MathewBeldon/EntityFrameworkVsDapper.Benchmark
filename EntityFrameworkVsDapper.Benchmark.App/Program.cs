using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using DapperNS = EntityFrameworkVsDapper.Benchmark.Dapper.Repositories;
using EntityNS = EntityFrameworkVsDapper.Benchmark.EntityFramework.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(DapperNS.Base.BaseRepository<>));
//builder.Services.AddScoped<IBenchRepository, DapperNS.BenchRepository>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(EntityNS.Base.BaseRepository<>));
builder.Services.AddScoped<IBenchRepository, EntityNS.BenchRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
