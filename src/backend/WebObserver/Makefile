MIGRATION_NAME ?= Init
PROJECT ?= ./WebObserver.Main.Infrastructure/WebObserver.Main.Infrastructure.csproj
STARTUP_PROJECT ?= ./WebObserver.Main.API/WebObserver.Main.API.csproj
CONSOLE_PROJECT ?= ./WebObserver.Main.Console/WebObserver.Main.Console.csproj

run-console:
	@echo "Запуск консольного приложения"
	@dotnet run --project $(CONSOLE_PROJECT)

add:
	@echo "Создание миграции: $(MIGRATION_NAME)"
	@dotnet ef migrations add $(MIGRATION_NAME) --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

remove:
	@echo "Удаление последней миграции"
	@dotnet ef migrations remove --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

apply:
	@echo "Применение миграций"
	@dotnet ef database update --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

revert:
	@echo "Откат миграций"
	@dotnet ef database update 0 --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

.PHONY: create remove apply revert
