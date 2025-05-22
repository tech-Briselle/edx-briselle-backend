# Adjusted Dockerfile to be run from edx-briselle.Server folder

FROM debian:stable-slim
FROM debian:12


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["edx-briselle.Server.csproj", "./"]

RUN dotnet restore "./edx-briselle.Server.csproj"

COPY . .

WORKDIR "/src"
RUN dotnet build "edx-briselle.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "edx-briselle.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "edx-briselle.Server.dll"]
