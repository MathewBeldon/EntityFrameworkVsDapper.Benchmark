namespace EntityFrameworkVsDapper.Benchmark.Core.Constants
{
    public static class DatabaseConstants
    {
        public const string ConnectionString = "Server=localhost;Port=5432;Database=benchmark;User Id=postgres;Pwd=password;";
        public const string MySqlVersion = "8.0.0";
        public const int RecordCount = 10000;
    }
}
