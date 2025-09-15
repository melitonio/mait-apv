# Dockerfile optimizado para ASP.NET con mejor soporte ARM64
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Optimizaciones para ARM64 - configurar red y dependencias básicas
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    ca-certificates \
    && rm -rf /var/lib/apt/lists/*

HEALTHCHECK --interval=10s --timeout=3s --retries=3 --start-period=30s \
    CMD curl -f http://localhost:8080/healthz || exit 1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias primero (mejor caching)
COPY ["mait-apv.csproj", "."]

# Restore con configuraciones optimizadas para ARM64
RUN dotnet restore "mait-apv.csproj" \
    --no-cache \
    --force \
    --disable-parallel

# Copiar código fuente
COPY . .
WORKDIR "/src/."

# Build con configuraciones optimizadas
RUN dotnet build "mait-apv.csproj" \
    -c Release \
    -o /app/build \
    --no-restore

FROM build AS publish
# Publish con configuraciones optimizadas
RUN dotnet publish "mait-apv.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mait-apv.dll"]