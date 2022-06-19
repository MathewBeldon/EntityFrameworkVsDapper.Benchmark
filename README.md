#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper


|    ORM |                               Method |     Mean |   StdDev |    Error |      Min |      Max |   Gen 0 |  Gen 1 | Allocated |
|------- |------------------------------------- |---------:|---------:|---------:|---------:|---------:|--------:|-------:|----------:|
|     EF |          'Single record (interface)' | 208.8 us |  1.96 us |  3.30 us | 206.8 us | 213.1 us |  0.7500 |      - |      6 KB |
|     EF |      'Single record <T> (interface)' | 227.5 us |  3.52 us |  5.92 us | 221.1 us | 232.2 us |  0.7500 |      - |      6 KB |
|     EF | 'Single record w/ joins (interface)' | 250.8 us |  0.89 us |  1.35 us | 249.4 us | 252.1 us |  1.0000 |      - |      7 KB |
| Dapper |          'Single record (interface)' | 313.4 us | 11.22 us | 18.86 us | 299.8 us | 334.0 us |  2.0000 |      - |     15 KB |
| Dapper |      'Single record <T> (interface)' | 335.7 us | 12.44 us | 20.90 us | 314.1 us | 353.4 us |  2.0000 |      - |     15 KB |
|     EF |      'Paged records <T> (interface)' | 351.1 us |  2.86 us |  5.46 us | 348.0 us | 355.9 us |  3.5000 |      - |     22 KB |
| Dapper |      'Paged records <T> (interface)' | 421.7 us |  8.67 us | 13.10 us | 411.5 us | 437.0 us |  7.5000 |      - |     48 KB |
| Dapper | 'Single record w/ joins (interface)' | 442.7 us | 18.64 us | 28.18 us | 418.4 us | 473.2 us |  5.0000 |      - |     37 KB |
|     EF | 'Paged records w/ joins (interface)' | 463.9 us |  3.82 us |  5.77 us | 458.0 us | 470.0 us |  6.0000 |      - |     40 KB |
| Dapper | 'Paged records w/ joins (interface)' | 667.6 us | 10.77 us | 16.28 us | 653.1 us | 683.2 us | 13.0000 | 1.0000 |     81 KB |