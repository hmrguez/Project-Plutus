# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Project-Plutus/*.fsproj .
RUN dotnet restore

# copy everything else and build app
COPY Project-Plutus/. .
WORKDIR /source/
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Project-Plutus.dll"]













# # Build stage
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /source
# COPY . .
# RUN dotnet restore "./Project-Plutus/Project-Plutus.fsproj" --disable-parallel
# RUN dotnet publish "./Project-Plutus/Project-Plutus.fsproj" -c release -o /app --no-restore
# # RUN dotnet restore "./Project-Plutus/Project-Plutus.fsproj" --disable-parallel
# # RUN dotnet publish "./Project-Plutus/Project-Plutus.fsproj" -c release -o /app --no-restore

# # Serve Stage
# FROM mcr.microsoft.com/dotnet/aspnet:7.0
# WORKDIR /app
# COPY --from=build /app ./

# EXPOSE 5000

# ENTRYPOINT ["dotnet", "Project-Plutus.dll"]