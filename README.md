#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper


|                                      Method |           Mean |        Error |       StdDev |      Gen 0 |     Gen 1 |    Gen 2 |  Allocated |
|-------------------------------------------- |---------------:|-------------:|-------------:|-----------:|----------:|---------:|-----------:|
|        EntityFramework_Generic_SingleRecord |       264.7 us |      5.24 us |     10.46 us |     1.4648 |         - |        - |      11 KB |
|                 Dapper_Generic_SingleRecord |       468.5 us |     30.79 us |     90.78 us |          - |         - |        - |      16 KB |
| EntityFramework_Generic_SingleRecordLoopAll | 2,562,597.4 us | 27,092.84 us | 24,017.09 us | 18000.0000 |         - |        - | 112,735 KB |
|          Dapper_Generic_SingleRecordLoopAll | 2,971,009.5 us | 12,145.31 us | 10,141.88 us | 24000.0000 |         - |        - | 148,391 KB |
|          EntityFramework_Generic_AllRecords |    16,421.2 us |    114.11 us |    106.73 us |   906.2500 |  406.2500 | 125.0000 |   4,944 KB |
|                   Dapper_Generic_AllRecords |    19,771.2 us |    371.98 us |    725.52 us |  2656.2500 | 1062.5000 | 531.2500 |  15,066 KB |
|          EntityFramework_Bench_SingleRecord |       246.6 us |      1.66 us |      1.38 us |     1.4648 |         - |        - |      10 KB |
|                   Dapper_Bench_SingleRecord |       291.3 us |      1.76 us |      1.64 us |     1.9531 |         - |        - |      15 KB |
|   EntityFramework_Bench_SingleRecordLoopAll | 2,457,185.3 us |  9,053.89 us |  8,469.02 us | 15000.0000 |         - |        - |  97,899 KB |
|           Dappper_Bench_SingleRecordLoopAll | 2,913,433.9 us |  8,879.90 us |  7,415.11 us | 23000.0000 |         - |        - | 146,673 KB |
| EntityFramework_Bench_SingleRecordPopulated |       373.4 us |      1.36 us |      1.27 us |     7.8125 |         - |        - |      50 KB |
|          Dapper_Bench_SingleRecordPopulated |       387.8 us |      2.81 us |      2.63 us |     5.8594 |         - |        - |      37 KB |