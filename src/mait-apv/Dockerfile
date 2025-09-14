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

# Configurar variables de entorno para ARM64 antes del restore
ENV DOTNET_SYSTEM_NET_HTTP_SOCKETSHTTPHANDLER_HTTP2SUPPORT=false
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=false
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
ENV NUGET_HTTP_CACHE_PATH=/tmp/nuget-cache

# Crear cache directory para NuGet
RUN mkdir -p /tmp/nuget-cache

# Configurar fuentes de NuGet de forma explícita con HTTP/1.1
RUN dotnet nuget remove source nuget.org || true
RUN dotnet nuget add source https://api.nuget.org/v3/index.json --name nuget.org

# Copiar y restaurar dependencias primero (mejor caching)
COPY ["mait-apv.csproj", "."]

# Restore con configuraciones optimizadas para ARM64
RUN dotnet restore "mait-apv.csproj" \
    --verbosity normal \
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
    --no-restore \
    --verbosity normal

FROM build AS publish
# Publish con configuraciones optimizadas
RUN dotnet publish "mait-apv.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    --verbosity normal

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mait-apv.dll"]