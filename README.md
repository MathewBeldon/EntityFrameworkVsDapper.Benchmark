#  EntityFrameworkVsDapper.Benchmark

EntityFramework Vs Dapper


|          Method |    Mean |   Error |  StdDev |       Gen 0 |     Gen 1 | Allocated |
|---------------- |--------:|--------:|--------:|------------:|----------:|----------:|
| EntityFramework | 27.84 s | 0.530 s | 0.521 s | 176000.0000 | 2000.0000 |      1 GB |
|          Dapper | 30.86 s | 0.410 s | 0.364 s | 218000.0000 | 4000.0000 |      1 GB |