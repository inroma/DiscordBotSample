##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7 AS base #image en arm pour déploiement raspberry
RUN apt-get update && apt-get install -y myLib #Pour installer des packages nécéssaire sur le container de prod
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DiscordBotSample/DiscordBotSample.csproj", "DiscordBotSample/"]
RUN dotnet restore "DiscordBotSample/DiscordBotSample.csproj" -r linux-arm
COPY . .
WORKDIR "/src/DiscordBotSample"
RUN dotnet build "DiscordBotSample.csproj" -r linux-arm -c Release -o /app/build #image en arm pour déploiement raspberry

FROM build AS publish
RUN dotnet publish "DiscordBotSample.csproj" -r linux-arm -c Release -o /app/publish #image en arm pour déploiement raspberry

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscordBotSample.dll"]