#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM qgis/qgis as base
# Set environment variables for QGIS
#ENV QGIS_PREFIX_PATH=/usr
#ENV QT_QPA_PLATFORM=offscreen
#ENV XDG_RUNTIME_DIR=/tmp/runtime-root

WORKDIR /app
EXPOSE 80

# Install the .NET runtime
RUN apt-get update && \
    apt-get install -y aspnetcore-runtime-6.0

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Neptune.QGISAPI/Neptune.QGISAPI.csproj", "Neptune.QGISAPI/"]
RUN dotnet restore "Neptune.QGISAPI/Neptune.QGISAPI.csproj"
COPY . .
WORKDIR "/src/Neptune.QGISAPI"
RUN dotnet build "Neptune.QGISAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Neptune.QGISAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neptune.QGISAPI.dll"]