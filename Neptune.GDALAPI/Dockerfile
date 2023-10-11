#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM osgeo/gdal:ubuntu-full-latest as base
WORKDIR /app
EXPOSE 80

# Install the .NET runtime
RUN apt-get update && \
    apt-get install -y aspnetcore-runtime-6.0

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Neptune.GDALAPI/Neptune.GDALAPI.csproj", "Neptune.GDALAPI/"]
RUN dotnet restore "Neptune.GDALAPI/Neptune.GDALAPI.csproj"
COPY . .
WORKDIR "/src/Neptune.GDALAPI"
RUN dotnet build "Neptune.GDALAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Neptune.GDALAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neptune.GDALAPI.dll"]