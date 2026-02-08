$ErrorActionPreference = "Stop"

param(
    [Parameter(Mandatory = $true)]
    [string]$ConnectionString,
    [string]$Configuration = "Release",
    [string]$Framework = "net10.0",
    [switch]$SkipRestore,
    [switch]$NoBuild
)

$repoRoot = Split-Path -Parent $PSScriptRoot

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw "dotnet SDK is required. Install it from https://dotnet.microsoft.com/download"
}

if (-not $SkipRestore) {
    dotnet restore "$repoRoot\EntityFrameworkVsDapper.Benchmark.sln"
}

if (-not $NoBuild) {
    dotnet build "$repoRoot\EntityFrameworkVsDapper.Benchmark.sln" -c $Configuration
}

$env:BENCHMARK_CONNECTION_STRING = $ConnectionString

$seedArgs = @(
    "run",
    "-c", $Configuration,
    "--project", "$repoRoot\EntityFrameworkVsDapper.Benchmark.Seed\EntityFrameworkVsDapper.Benchmark.Seed.csproj"
)
if ($NoBuild) { $seedArgs += "--no-build" }
dotnet @seedArgs

$benchArgs = @(
    "run",
    "-c", $Configuration,
    "-f", $Framework,
    "--project", "$repoRoot\EntityFrameworkVsDapper.Benchmark.Program\EntityFrameworkVsDapper.Benchmark.Program.csproj"
)
if ($NoBuild) { $benchArgs += "--no-build" }
dotnet @benchArgs
