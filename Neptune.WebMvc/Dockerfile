#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update && \
    apt-get install -y gnupg software-properties-common wget && \
    mkdir /etc/apt/keyrings/ && \
    wget -O /etc/apt/keyrings/qgis-archive-keyring.gpg https://download.qgis.org/downloads/qgis-archive-keyring.gpg && \
    echo "Types: deb deb-src" > /etc/apt/sources.list.d/qgis.sources && \
    echo "URIs: https://qgis.org/debian" >> /etc/apt/sources.list.d/qgis.sources && \
    echo "Suites: $(lsb_release -cs)" >> /etc/apt/sources.list.d/qgis.sources && \
    echo "Architectures: amd64" >> /etc/apt/sources.list.d/qgis.sources && \
    echo "Components: main" >> /etc/apt/sources.list.d/qgis.sources && \
    echo "Signed-By: /etc/apt/keyrings/qgis-archive-keyring.gpg" >> /etc/apt/sources.list.d/qgis.sources && \
    apt-get update && \
    apt-get install -y qgis qgis-plugin-grass && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Neptune.WebMvc/Neptune.WebMvc.csproj", "Neptune.WebMvc/"]
RUN dotnet restore "Neptune.WebMvc/Neptune.WebMvc.csproj"
COPY . .
WORKDIR "/src/Neptune.WebMvc"
RUN dotnet build "Neptune.WebMvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Neptune.WebMvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neptune.WebMvc.dll"]