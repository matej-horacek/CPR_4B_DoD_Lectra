#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."

[ -f .env ] || { echo "Missing .env. Run 'make init'."; exit 1; }

echo "API:      http://localhost:${API_PORT:-5125}/scalar"
echo "Postgres: localhost:${POSTGRES_PORT:-5432}"
docker compose up --build -d