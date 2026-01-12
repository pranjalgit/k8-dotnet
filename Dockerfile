# =========================
# Build Stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# =========================
# Runtime Stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Expose API port
EXPOSE 8080

# Copy published output
COPY --from=build /app/publish .

# Run application
ENTRYPOINT ["dotnet", "Media-MS.dll"]
