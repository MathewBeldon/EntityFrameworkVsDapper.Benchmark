using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkVsDapper.Benchmark.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly IBenchRepository _benchRepository;
        private readonly IBaseRepository<Benches> _baseRepository;

        public DatabaseController(
            IBenchRepository benchRepository, 
            IBaseRepository<Benches> baseRepository)
        {
            _benchRepository = benchRepository;
            _baseRepository = baseRepository;
        }

        [HttpGet("{benchId:int}")]
        public async Task<IActionResult> GetById(int benchId)
        {
            var result = await _baseRepository.GetByIdAsync(benchId);
            return Ok(result);
        }

        [HttpGet("test")]
        public async Task<IActionResult> GetByIdTest()
        {
           // var result = await _baseRepository.GetByIdAsync(benchId);
            return Ok(new Benches() { Name = "test", Description = "test"});
        }
    }
}
