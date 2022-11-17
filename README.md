#  EntityFrameworkVsDapper.Benchmark

### Read Record

NET|    ORM |                            DB |       Mean |        Min |        Max | Allocated |
---|------- |------------------------------ |-----------:|-----------:|-----------:|----------:|
  6| Dapper |                         Local |   342.3 us |   322.9 us |   374.3 us |  15.03 KB |
  6|    EF6 |                         Local |   417.8 us |   409.9 us |   431.3 us |  70.46 KB |
  7| Dapper |                         Local |   340.9 us |   339.5 us |   345.0 us |  15.03 KB |
  7|    EF7 |                         Local |   393.7 us |   389.5 us |   399.4 us |  71.68 KB |
  6| Dapper |                        Remote |  120.79 ms |  119.39 ms |  122.93 ms |  15.45 KB |
  6|    EF6 |                        Remote |   74.66 ms |   71.63 ms |   77.28 ms |  77.74 KB |
  7| Dapper |                        Remote |  122.61 ms |  118.00 ms |  129.72 ms |  15.45 KB |
  7|    EF7 |                        Remote |   74.07 ms |   72.02 ms |   76.40 ms |  76.74 KB |

### Create Record

NET|    ORM |                            DB |       Mean |        Min |        Max | Allocated |
---|------- |------------------------------ |-----------:|-----------:|-----------:|----------:|
  6| Dapper |                         Local | 1,879.1 us | 1,829.3 us | 1,958.2 us |  28.98 KB |
  6|    EF6 |               		  Local | 2,443.2 us | 2,328.2 us | 2,580.0 us |  96.79 KB |
  7| Dapper |                         Local | 1,835.5 us | 1,808.6 us | 1,865.8 us |  28.98 KB |
  7|    EF7 |                         Local | 2,348.3 us | 2,308.6 us | 2,402.0 us | 100.88 KB |
  6| Dapper |                        Remote |  243.82 ms |  226.15 ms |  264.19 ms |  29.41 KB |
  6|    EF6 |                        Remote |  290.05 ms |  257.20 ms |  329.96 ms |  102.2 KB |
  7| Dapper |                        Remote |  226.55 ms |  223.04 ms |  230.38 ms |  29.41 KB |
  7|    EF7 |                        Remote |  261.11 ms |  259.03 ms |  264.26 ms | 106.28 KB |



## Full Benchmarks

### DOTNET 6 LOCAL DB

|    ORM |                        Method |       Mean |        Min |        Max |    Gen0 |   Gen1 | Allocated |
|------- |------------------------------ |-----------:|-----------:|-----------:|--------:|-------:|----------:|
| Dapper |               'Create record' | 1,879.1 us | 1,829.3 us | 1,958.2 us |  4.0000 |      - |  28.98 KB |
|     EF |               'Create record' | 2,443.2 us | 2,328.2 us | 2,580.0 us | 14.0000 |      - |  96.79 KB |
| Dapper |   'Create then delete record' | 3,664.5 us | 3,595.9 us | 3,721.2 us |  6.0000 | 2.0000 |  41.48 KB |
|     EF |   'Create then delete record' | 4,956.5 us | 4,866.2 us | 5,036.5 us | 30.0000 | 2.0000 | 191.96 KB |
| Dapper |   'Create then update record' | 3,821.7 us | 3,684.4 us | 3,957.4 us |  7.8125 |      - |  51.84 KB |
|     EF |   'Create then update record' | 4,921.9 us | 4,752.8 us | 5,092.1 us | 23.4375 |      - | 190.54 KB |
| Dapper |        'Read paged records T' |   462.0 us |   457.1 us |   473.3 us |  7.0000 |      - |   48.1 KB |
|     EF |        'Read paged records T' |   574.7 us |   566.7 us |   580.4 us | 16.0000 | 1.0000 |  103.7 KB |
| Dapper | 'Read paged records w/ joins' |   648.3 us |   637.6 us |   666.3 us | 12.0000 | 1.0000 |  76.26 KB |
|     EF | 'Read paged records w/ joins' |   686.1 us |   641.4 us |   812.1 us | 15.6250 | 0.9766 | 100.08 KB |
| Dapper |               'Read record T' |   356.7 us |   346.3 us |   370.1 us |  2.0000 |      - |   15.2 KB |
|     EF |               'Read record T' |   419.3 us |   402.7 us |   453.5 us | 10.7422 |      - |  70.46 KB |
| Dapper |        'Read record w/ joins' |   424.9 us |   418.7 us |   434.2 us |  5.8594 |      - |  36.55 KB |
|     EF |        'Read record w/ joins' |   447.1 us |   436.9 us |   461.1 us | 11.0000 | 1.0000 |  70.13 KB |
| Dapper |                 'Read record' |   342.3 us |   322.9 us |   374.3 us |  2.4414 |      - |  15.03 KB |
|     EF |                 'Read record' |   417.8 us |   409.9 us |   431.3 us | 10.7422 |      - |  70.46 KB |

### DOTNET 7 LOCAL DB

|    ORM |                        Method |       Mean |        Min |        Max |    Gen0 |   Gen1 | Allocated |
|------- |------------------------------ |-----------:|-----------:|-----------:|--------:|-------:|----------:|
| Dapper |               'Create record' | 1,835.5 us | 1,808.6 us | 1,865.8 us |  4.0000 |      - |  28.98 KB |
|     EF |               'Create record' | 2,348.3 us | 2,308.6 us | 2,402.0 us | 16.0000 |      - | 100.88 KB |
| Dapper |   'Create then delete record' | 3,524.5 us | 3,470.8 us | 3,590.5 us |  6.0000 | 2.0000 |  41.48 KB |
|     EF |   'Create then delete record' | 4,811.2 us | 4,719.4 us | 4,894.2 us | 30.0000 | 2.0000 | 194.16 KB |
| Dapper |   'Create then update record' | 3,650.8 us | 3,567.6 us | 3,719.3 us |  8.0000 | 2.0000 |  51.84 KB |
|     EF |   'Create then update record' | 4,672.9 us | 4,587.2 us | 4,735.9 us | 30.0000 | 2.0000 | 194.72 KB |
| Dapper |        'Read paged records T' |   443.6 us |   438.7 us |   449.4 us |  7.0000 |      - |   48.1 KB |
|     EF |        'Read paged records T' |   556.2 us |   541.4 us |   593.4 us | 16.6016 | 1.9531 | 105.64 KB |
| Dapper | 'Read paged records w/ joins' |   599.0 us |   593.3 us |   611.0 us | 12.0000 | 1.0000 |  76.26 KB |
|     EF | 'Read paged records w/ joins' |   601.7 us |   595.0 us |   607.7 us | 16.6016 | 0.9766 |  102.4 KB |
| Dapper |               'Read record T' |   341.0 us |   334.8 us |   347.7 us |  2.0000 |      - |   15.2 KB |
|     EF |               'Read record T' |   396.5 us |   385.3 us |   418.9 us | 11.0000 | 1.0000 |  71.68 KB |
| Dapper |        'Read record w/ joins' |   431.0 us |   427.5 us |   438.4 us |  5.5000 |      - |  36.55 KB |
|     EF |        'Read record w/ joins' |   423.4 us |   414.5 us |   439.3 us | 11.2305 | 0.4883 |  71.34 KB |
| Dapper |                 'Read record' |   340.9 us |   339.5 us |   345.0 us |  2.0000 |      - |  15.03 KB |
|     EF |                 'Read record' |   393.7 us |   389.5 us |   399.4 us | 11.0000 | 1.0000 |  71.68 KB |

### DOTNET 6 REMOTE DB

|    ORM |                        Method |      Mean |       Min |       Max | Allocated |
|------- |------------------------------ |----------:|----------:|----------:|----------:|
| Dapper |               'Create record' | 243.82 ms | 226.15 ms | 264.19 ms |  29.41 KB |
|     EF |               'Create record' | 290.05 ms | 257.20 ms | 329.96 ms |  102.2 KB |
| Dapper |   'Create then delete record' | 458.05 ms | 435.12 ms | 484.98 ms |  42.24 KB |
|     EF |   'Create then delete record' | 580.83 ms | 551.84 ms | 617.94 ms | 197.74 KB |
| Dapper |   'Create then update record' | 477.78 ms | 442.92 ms | 544.15 ms |  52.59 KB |
|     EF |   'Create then update record' | 542.39 ms | 518.36 ms | 558.24 ms | 197.08 KB |
| Dapper |        'Read paged records T' | 120.87 ms | 119.55 ms | 122.90 ms |  48.55 KB |
|     EF |        'Read paged records T' |  88.00 ms |  84.39 ms |  94.20 ms | 108.82 KB |
| Dapper | 'Read paged records w/ joins' | 149.12 ms | 142.47 ms | 165.76 ms |   76.7 KB |
|     EF | 'Read paged records w/ joins' |  88.35 ms |  84.11 ms |  92.30 ms | 105.08 KB |
| Dapper |               'Read record T' | 120.45 ms | 118.05 ms | 122.99 ms |  15.62 KB |
|     EF |               'Read record T' |  74.37 ms |  72.12 ms |  76.07 ms |  77.74 KB |
| Dapper |        'Read record w/ joins' | 141.84 ms | 139.09 ms | 148.53 ms |  36.97 KB |
|     EF |        'Read record w/ joins' |  85.16 ms |  82.56 ms |  86.97 ms |  75.17 KB |
| Dapper |                 'Read record' | 120.79 ms | 119.39 ms | 122.93 ms |  15.45 KB |
|     EF |                 'Read record' |  74.66 ms |  71.63 ms |  77.28 ms |  77.74 KB |

### DOTNET 7 REMOTE DB

|    ORM |                        Method |      Mean |       Min |       Max | Allocated |
|------- |------------------------------ |----------:|----------:|----------:|----------:|
| Dapper |               'Create record' | 226.55 ms | 223.04 ms | 230.38 ms |  29.41 KB |
|     EF |               'Create record' | 261.11 ms | 259.03 ms | 264.26 ms | 106.28 KB |
| Dapper |   'Create then delete record' | 509.29 ms | 470.73 ms | 594.58 ms |  42.23 KB |
|     EF |   'Create then delete record' | 575.23 ms | 554.01 ms | 618.15 ms | 200.79 KB |
| Dapper |   'Create then update record' | 460.32 ms | 445.83 ms | 490.78 ms |  52.59 KB |
|     EF |   'Create then update record' | 523.93 ms | 514.24 ms | 540.06 ms | 201.67 KB |
| Dapper |        'Read paged records T' | 121.68 ms | 119.60 ms | 123.82 ms |  48.55 KB |
|     EF |        'Read paged records T' |  88.22 ms |  85.25 ms |  93.19 ms |  110.7 KB |
| Dapper | 'Read paged records w/ joins' | 143.41 ms | 141.67 ms | 144.50 ms |   76.7 KB |
|     EF | 'Read paged records w/ joins' |  89.76 ms |  87.42 ms |  94.17 ms | 104.83 KB |
| Dapper |               'Read record T' | 118.74 ms | 116.53 ms | 120.85 ms |  15.62 KB |
|     EF |               'Read record T' |  74.35 ms |  72.48 ms |  75.68 ms |  76.74 KB |
| Dapper |        'Read record w/ joins' | 142.19 ms | 140.49 ms | 145.02 ms |  36.97 KB |
|     EF |        'Read record w/ joins' |  88.42 ms |  86.41 ms |  91.14 ms |  73.77 KB |
| Dapper |                 'Read record' | 122.61 ms | 118.00 ms | 129.72 ms |  15.45 KB |
|     EF |                 'Read record' |  74.07 ms |  72.02 ms |  76.40 ms |  76.74 KB |