#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."

echo "==> tools"
command -v docker >/dev/null || { echo "Docker is not installed"; exit 1; }
docker compose version >/dev/null 2>&1 || { echo "Docker Compose v2 is missing"; exit 1; }

echo "==> Environment"
if [ ! -f .env ]; then
  cp .env.example .env
  echo "Created .env from .env.example"
else
  echo ".env already exists, skipping"
fi

echo "==> Building images"
docker compose pull postgres
docker compose build

echo "Run 'make dev' or ./scripts/dev.sh to start dev stack"