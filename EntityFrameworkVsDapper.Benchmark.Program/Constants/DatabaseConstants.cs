using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkVsDapper.Benchmark.Program.Constants
{
    internal static class DatabaseConstants
    {
        public const string ConnectionString = "Server=localhost;Database=ef_dapper_benchmark;Uid=sa;Pwd=MyPassword;";
        public const string MySqlVersion = "8.0.0";
        public const int RecordCount = 10000;
    }
}
