# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "D:/a/1/s/Project-Plutus/Project-Plutus.fsproj" --disable-parallel
RUN dotnet publish "D:/a/1/s/Project-Plutus/Project-Plutus.fsproj" -c release -o /app --no-restore
# RUN dotnet restore "./Project-Plutus/Project-Plutus.fsproj" --disable-parallel
# RUN dotnet publish "./Project-Plutus/Project-Plutus.fsproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Project-Plutus.dll"]