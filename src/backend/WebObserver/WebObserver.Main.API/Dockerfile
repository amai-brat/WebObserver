FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebObserver.Main.API/WebObserver.Main.API.csproj", "WebObserver.Main.API/"]
COPY ["WebObserver.Main.Domain/WebObserver.Main.Domain.csproj", "WebObserver.Main.Domain/"]
COPY ["WebObserver.Main.Application/WebObserver.Main.Application.csproj", "WebObserver.Main.Application/"]
COPY ["WebObserver.Main.Infrastructure/WebObserver.Main.Infrastructure.csproj", "WebObserver.Main.Infrastructure/"]
RUN dotnet restore "WebObserver.Main.API/WebObserver.Main.API.csproj"
COPY . .
WORKDIR "/src/WebObserver.Main.API"
RUN dotnet build "WebObserver.Main.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebObserver.Main.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebObserver.Main.API.dll"]
