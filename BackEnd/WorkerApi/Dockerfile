FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 5144
EXPOSE 5001

# Use a imagem base do SDK para construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /
COPY ["WorkerApi/WorkerApi.csproj", "WorkerApi/"]
RUN dotnet restore "WorkerApi/WorkerApi.csproj"
COPY . .
WORKDIR "/WorkerApi"
RUN dotnet build "WorkerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerApi.csproj" -c Release -o /app/publish

# Configuração final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerApi.dll"]
