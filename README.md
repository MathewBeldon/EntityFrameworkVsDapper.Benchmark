#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper


|                             Method |           Mean |        Error |       StdDev |      Gen 0 |  Allocated |
|----------------------------------- |---------------:|-------------:|-------------:|-----------:|-----------:|
|        EntityFrameworkSingleRecord |       259.2 us |      4.40 us |      3.90 us |     1.4648 |      11 KB |
|                 DapperSingleRecord |       311.0 us |      6.08 us |      8.33 us |     1.9531 |      15 KB |
| EntityFrameworkSingleRecordLoopAll | 2,558,236.5 us | 16,983.33 us | 14,181.85 us | 17000.0000 | 108,054 KB |
|          DapperSingleRecordLoopAll | 3,050,702.3 us |  7,961.20 us |  6,215.58 us | 24000.0000 | 148,391 KB |