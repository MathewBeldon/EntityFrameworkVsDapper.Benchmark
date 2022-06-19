#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper

|    ORM |                                  Method |        Mean |        Min |         Max |     Gen 0 |   Gen 1 | Allocated |
|------- |---------------------------------------- |------------:|-----------:|------------:|----------:|--------:|----------:|
|     EF |             'Single record (interface)' |    209.3 us |   208.2 us |    211.3 us |    0.7500 |       - |      6 KB |
|     EF |           'Single record T (interface)' |    209.8 us |   208.2 us |    211.2 us |    0.5000 |       - |      6 KB |
|     EF |    'Single record w/ joins (interface)' |    245.4 us |   244.2 us |    246.6 us |    1.0000 |       - |      7 KB |
| Dapper |           'Single record T (interface)' |    308.0 us |   297.7 us |    322.9 us |    2.0000 |       - |     15 KB |
| Dapper |             'Single record (interface)' |    316.1 us |   305.1 us |    334.9 us |    2.0000 |       - |     15 KB |
|     EF |           'Paged records T (interface)' |    319.7 us |   316.0 us |    323.0 us |    2.5000 |       - |     18 KB |
| Dapper |    'Single record w/ joins (interface)' |    424.0 us |   408.3 us |    440.6 us |    5.5000 |       - |     37 KB |
| Dapper |           'Paged records T (interface)' |    443.9 us |   424.2 us |    472.5 us |    7.5000 |       - |     48 KB |
|     EF |    'Paged records w/ joins (interface)' |    461.6 us |   448.4 us |    489.4 us |    6.0000 |       - |     40 KB |
| Dapper |    'Paged records w/ joins (interface)' |    663.5 us |   636.1 us |    692.7 us |   13.0000 |  1.0000 |     81 KB |
| Dapper |             'Create record (interface)' |  3,148.3 us | 3,040.1 us |  3,293.6 us |    4.0000 |       - |     29 KB |
| Dapper | 'Create then delete record (interface)' | 10,753.3 us | 6,713.6 us | 19,387.6 us |    6.0000 |  2.0000 |     41 KB |
|     EF |             'Create record (interface)' | 13,078.6 us | 8,459.5 us | 17,677.6 us | 1890.0000 | 72.0000 | 11,585 KB |
|     EF | 'Create then delete record (interface)' | 15,710.1 us | 7,916.8 us | 20,895.6 us |    8.0000 |       - |     59 KB |