﻿using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository;
using EntityFrameworkVsDapper.Benchmark.Core.Contracts.Repository.Base;
using EntityFrameworkVsDapper.Benchmark.Core.Entities;
using EntityFrameworkVsDapper.Benchmark.Program.Constants;

namespace EntityFrameworkVsDapper.Benchmark.Program.Benchmarks
{
    public class BenchmarkBase
    {
        protected IBaseRepository<Benches> _baseGenericBenchRepository;
        protected IBenchRepository _benchRepository;
        private Random random = new Random(); 

        protected int iterator = 1;
        protected void IncrementIterator()
        {
            iterator++;
            if (iterator > DatabaseConstants.RecordCount) iterator = 1;
        }

        protected int page = 1;
        protected const int totalPages = DatabaseConstants.RecordCount / 20;
        protected void IncrementPage()
        {
            page++;
            if (page > totalPages) page = 1;
        }

        protected void GenericSingleRecordShared()
        {
            IncrementIterator();
            var result = _baseGenericBenchRepository.GetById(iterator);
            if (result is not null && result.Id != iterator) throw new NullReferenceException();
        }

        protected void GenericPagedRecordsShared()
        {
            IncrementPage();
            var result = _baseGenericBenchRepository.GetByIdPaged(page: page, pageSize: 20, totalCount: DatabaseConstants.RecordCount);
            if (result.Count() != 20) throw new NullReferenceException();
        }

        protected void SingleRecordShared()
        {
            IncrementIterator();
            var result = _benchRepository.GetBench(iterator);
            if (result is not null && result.Id != iterator) throw new NullReferenceException();
        }

        protected void SingleRecordPopulatedShared()
        {
            IncrementIterator();
            var result = _benchRepository.GetBenchPopulated(iterator);
            if (result is not null && result.Id != iterator) throw new NullReferenceException();
        }

        protected void PagedRecordsPopulatedShared()
        {
            IncrementPage();
            var result = _benchRepository.GetBenchPopulatedPage(page: page, pageSize: 20, totalCount: DatabaseConstants.RecordCount);
            if (result.Count() != 20) throw new NullReferenceException();
        }

        protected void CreateRecordShared()
        {
            _benchRepository.CreateBench(new Benches
            {
                MaterialId = random.Next(1, 10001),
                StyleId = random.Next(1, 10001),
                Name = "Created Name",
                Description = "Created Description",
                Cost = random.Next(1, 1000),
                Height = random.Next(1, 1000),
                Width = random.Next(1, 1000),
                Depth = random.Next(1, 1000)
            });
        }
    }
}
