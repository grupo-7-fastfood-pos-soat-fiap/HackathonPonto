#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/HackathonPonto.Services.API/HackathonPonto.Services.API.csproj", "src/HackathonPonto.Services.API/"]
RUN dotnet restore "src/HackathonPonto.Services.API/HackathonPonto.Services.API.csproj"
COPY . .
WORKDIR "/src/src/HackathonPonto.Services.API"
RUN dotnet build "HackathonPonto.Services.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackathonPonto.Services.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackathonPonto.Services.API.dll"]
