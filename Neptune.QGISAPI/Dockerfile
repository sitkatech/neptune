#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM qgis/qgis as base
# Set environment variables for QGIS
ENV QGIS_PREFIX_PATH=/usr
ENV QT_QPA_PLATFORM=offscreen
ENV XDG_RUNTIME_DIR=/tmp/runtime-root

WORKDIR /app
EXPOSE 80

RUN apt-get update && \
    apt-get install -y wget

# Download Microsoft signing key and repository
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

# Install Microsoft signing key and repository
RUN dpkg -i packages-microsoft-prod.deb

# Clean up
RUN rm packages-microsoft-prod.deb

# Install the .NET runtime
RUN apt-get update && \
    apt-get install -y aspnetcore-runtime-8.0


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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
# copy the python scripts
COPY --from=publish /src/Neptune.QGISAPI/pyqgis_utils.py .
COPY --from=publish /src/Neptune.QGISAPI/ComputeTrashGeneratingUnits.py .
COPY --from=publish /src/Neptune.QGISAPI/ModelingOverlayAnalysis.py .

ENTRYPOINT ["dotnet", "Neptune.QGISAPI.dll"]