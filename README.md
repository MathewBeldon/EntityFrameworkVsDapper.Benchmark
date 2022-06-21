#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper

|    ORM |                        Method |        Mean |         Min |         Max |   Gen 0 |  Gen 1 | Allocated |
|------- |------------------------------ |------------:|------------:|------------:|--------:|-------:|----------:|
| Dapper |               'Create record' |  3,064.1 us |  2,936.3 us |  3,218.1 us |  4.0000 |      - |     29 KB |
|     EF |               'Create record' |  3,423.9 us |  3,280.0 us |  3,556.3 us | 14.0000 |      - |     97 KB |
| Dapper |   'Create then delete record' |  6,687.1 us |  6,177.7 us |  8,844.4 us |  6.0000 | 2.0000 |     41 KB |
|     EF |   'Create then delete record' |  7,511.7 us |  7,234.5 us |  7,707.6 us | 30.0000 | 2.0000 |    191 KB |
| Dapper |   'Create then update record' | 19,256.6 us | 18,188.3 us | 20,278.9 us |  8.0000 | 2.0000 |     52 KB |
|     EF |   'Create then update record' | 20,598.0 us | 19,807.5 us | 21,561.0 us | 30.0000 | 2.0000 |    190 KB |
| Dapper |        'Read paged records T' |    412.4 us |    407.5 us |    415.3 us |  8.5000 |      - |     55 KB |
|     EF |        'Read paged records T' |    479.4 us |    472.3 us |    485.4 us | 17.0000 | 2.0000 |    105 KB |
| Dapper | 'Read paged records w/ joins' |    548.1 us |    542.6 us |    552.8 us | 13.0000 | 1.0000 |     80 KB |
|     EF | 'Read paged records w/ joins' |    516.8 us |    502.3 us |    530.0 us | 16.0000 | 2.0000 |    103 KB |
| Dapper |               'Read record T' |    296.4 us |    289.4 us |    302.2 us |  2.5000 |      - |     16 KB |
|     EF |               'Read record T' |    326.6 us |    324.6 us |    329.6 us | 11.5000 | 1.0000 |     70 KB |
| Dapper |        'Read record w/ joins' |    365.3 us |    364.1 us |    366.8 us |  5.5000 |      - |     37 KB |
|     EF |        'Read record w/ joins' |    344.3 us |    341.5 us |    347.0 us | 11.0000 | 0.5000 |     70 KB |
| Dapper |                 'Read record' |    283.6 us |    282.0 us |    286.8 us |  2.5000 |      - |     15 KB |
|     EF |                 'Read record' |    326.4 us |    324.4 us |    328.6 us | 11.5000 | 1.0000 |     70 KB |