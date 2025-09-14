FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

HEALTHCHECK --interval=10s --timeout=3s --retries=3 --start-period=30s \
    CMD curl -f http://localhost:8080/healthz || exit 1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["mait-apv.csproj", "."]
RUN dotnet restore "mait-apv.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "mait-apv.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mait-apv.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mait-apv.dll"]