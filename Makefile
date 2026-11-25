.DEFAULT_GOAL := help
.PHONY: help dev init

help: ## Show this help
	@grep -E '^[a-zA-Z0-9_-]+:.*?## ' $(MAKEFILE_LIST) | \
		awk 'BEGIN {FS = ":.*?## "}; {printf "  \033[36m%-12s\033[0m %s\n", $$1, $$2}'

dev: ## Start the dev stack
	@bash scripts/dev.sh

init: ## Initialize the project
	@bash scripts/init.sh

down: ## down the dev stack
	@bash scripts/dev-down.sh

log: ## log service or all
	@bash scripts/log.sh

restart: ## restart service or all
	@bash scripts/restart.sh