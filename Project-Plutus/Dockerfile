FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Project-Plutus/Project-Plutus.fsproj", "Project-Plutus/"]
RUN dotnet restore "Project-Plutus/Project-Plutus.fsproj"
COPY . .
WORKDIR "/src/Project-Plutus"
RUN dotnet build "Project-Plutus.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project-Plutus.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project-Plutus.dll"]
