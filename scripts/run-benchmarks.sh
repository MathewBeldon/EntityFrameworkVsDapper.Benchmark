#!/usr/bin/env bash
set -euo pipefail

usage() {
  cat <<'EOF'
Usage:
  ./scripts/run-benchmarks.sh -c "<connection-string>" [--config Release] [--framework net10.0] [--skip-restore] [--no-build] [--install-dotnet] [--dotnet-dir ~/.dotnet]
EOF
}

CONFIGURATION="Release"
FRAMEWORK="net10.0"
SKIP_RESTORE="false"
NO_BUILD="false"
CONNECTION_STRING=""
INSTALL_DOTNET="false"
DOTNET_DIR="${HOME}/.dotnet"

while [[ $# -gt 0 ]]; do
  case "$1" in
    -c|--connection-string)
      CONNECTION_STRING="$2"
      shift 2
      ;;
    --dotnet-dir)
      DOTNET_DIR="$2"
      shift 2
      ;;
    --config)
      CONFIGURATION="$2"
      shift 2
      ;;
    --framework)
      FRAMEWORK="$2"
      shift 2
      ;;
    --skip-restore)
      SKIP_RESTORE="true"
      shift
      ;;
    --no-build)
      NO_BUILD="true"
      shift
      ;;
    --install-dotnet)
      INSTALL_DOTNET="true"
      shift
      ;;
    -h|--help)
      usage
      exit 0
      ;;
    *)
      echo "Unknown option: $1"
      usage
      exit 1
      ;;
  esac
done

if [[ -z "$CONNECTION_STRING" ]]; then
  echo "Missing required -c|--connection-string."
  usage
  exit 1
fi

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

if ! command -v dotnet >/dev/null 2>&1; then
  if [[ "$INSTALL_DOTNET" != "true" ]]; then
    INSTALL_DOTNET="true"
  fi
fi

if [[ "$INSTALL_DOTNET" == "true" ]]; then
  INSTALL_SCRIPT="$REPO_ROOT/scripts/dotnet-install.sh"
  if [[ ! -f "$INSTALL_SCRIPT" ]]; then
    if command -v curl >/dev/null 2>&1; then
      curl -fsSL https://dot.net/v1/dotnet-install.sh -o "$INSTALL_SCRIPT"
    elif command -v wget >/dev/null 2>&1; then
      wget -q -O "$INSTALL_SCRIPT" https://dot.net/v1/dotnet-install.sh
    else
      echo "curl or wget is required to download dotnet-install.sh"
      exit 1
    fi
    chmod +x "$INSTALL_SCRIPT"
  fi

  "$INSTALL_SCRIPT" --channel 6.0 --install-dir "$DOTNET_DIR" --no-path
  "$INSTALL_SCRIPT" --channel 8.0 --install-dir "$DOTNET_DIR" --no-path
  "$INSTALL_SCRIPT" --channel 10.0 --install-dir "$DOTNET_DIR" --no-path
fi

export DOTNET_ROOT="$DOTNET_DIR"
export PATH="$DOTNET_DIR:$PATH"

if ! command -v dotnet >/dev/null 2>&1; then
  echo "dotnet SDK is required. Install it from https://dotnet.microsoft.com/download"
  exit 1
fi

if [[ "$SKIP_RESTORE" != "true" ]]; then
  dotnet restore "$REPO_ROOT/EntityFrameworkVsDapper.Benchmark.sln"
fi

if [[ "$NO_BUILD" != "true" ]]; then
  dotnet build "$REPO_ROOT/EntityFrameworkVsDapper.Benchmark.sln" -c "$CONFIGURATION"
fi

export BENCHMARK_CONNECTION_STRING="$CONNECTION_STRING"

SEED_ARGS=(run -c "$CONFIGURATION" --project "$REPO_ROOT/EntityFrameworkVsDapper.Benchmark.Seed/EntityFrameworkVsDapper.Benchmark.Seed.csproj")
if [[ "$NO_BUILD" == "true" ]]; then
  SEED_ARGS+=(--no-build)
fi
dotnet "${SEED_ARGS[@]}"

BENCH_ARGS=(run -c "$CONFIGURATION" -f "$FRAMEWORK" --project "$REPO_ROOT/EntityFrameworkVsDapper.Benchmark.Program/EntityFrameworkVsDapper.Benchmark.Program.csproj")
if [[ "$NO_BUILD" == "true" ]]; then
  BENCH_ARGS+=(--no-build)
fi
dotnet "${BENCH_ARGS[@]}"
