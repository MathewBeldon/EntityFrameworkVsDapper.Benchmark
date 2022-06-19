#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper

|    ORM |                                      Method |       Mean |        Min |        Max |   Gen 0 |  Gen 1 | Allocated |
|------- |-------------------------------------------- |-----------:|-----------:|-----------:|--------:|-------:|----------:|
|     EF |                 'Single record (interface)' |   215.7 us |   213.7 us |   217.6 us |  0.7500 |      - |      6 KB |
|     EF |             'Single record <T> (interface)' |   219.6 us |   218.3 us |   220.8 us |  0.7500 |      - |      6 KB |
|     EF |        'Single record w/ joins (interface)' |   257.6 us |   252.6 us |   266.4 us |  1.0000 |      - |      7 KB |
| Dapper |             'Single record <T> (interface)' |   315.0 us |   299.7 us |   343.6 us |  2.0000 |      - |     15 KB |
| Dapper |                 'Single record (interface)' |   326.9 us |   304.9 us |   355.3 us |  2.0000 |      - |     15 KB |
|     EF |             'Paged records <T> (interface)' |   332.2 us |   329.0 us |   337.5 us |  3.5000 |      - |     22 KB |
| Dapper |             'Paged records <T> (interface)' |   408.0 us |   406.4 us |   410.5 us |  7.5000 |      - |     48 KB |
| Dapper |        'Single record w/ joins (interface)' |   458.3 us |   425.5 us |   525.8 us |  5.5000 |      - |     37 KB |
|     EF |        'Paged records w/ joins (interface)' |   478.2 us |   474.2 us |   483.2 us |  6.0000 |      - |     40 KB |
| Dapper |        'Paged records w/ joins (interface)' |   662.9 us |   654.3 us |   674.7 us | 13.0000 | 1.0000 |     81 KB |
| Dapper | 'Create record w/ transactions (interface)' | 3,142.0 us | 2,970.1 us | 3,289.2 us |  4.0000 |      - |     27 KB |
|     EF | 'Create record w/ transactions (interface)' | 3,427.0 us | 3,272.8 us | 3,560.4 us |  4.0000 |      - |     29 KB |