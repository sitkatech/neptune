#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM qgis/qgis as base
# Set environment variables for QGIS
ENV QGIS_PREFIX_PATH=/usr
ENV QT_QPA_PLATFORM=offscreen
ENV XDG_RUNTIME_DIR=/tmp/runtime-root

WORKDIR /app
EXPOSE 80

# Install the .NET runtime
RUN apt-get update && \
    apt-get install -y aspnetcore-runtime-6.0

# install sql server odbc
#RUN apt install libqt5sql5-odbc
#RUN curl https://packages.microsoft.com/keys/microsoft.asc | tee /etc/apt/trusted.gpg.d/microsoft.asc
#RUN curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list
#RUN apt-get update && ACCEPT_EULA=Y apt-get install -y msodbcsql18
#
# optional: for bcp and sqlcmd
#RUN ACCEPT_EULA=Y apt-get install -y mssql-tools18
#RUN echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc

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
# copy the python scripts
COPY --from=publish /src/Neptune.QGISAPI/pyqgis_utils.py .
COPY --from=publish /src/Neptune.QGISAPI/ComputeTrashGeneratingUnits.py .
COPY --from=publish /src/Neptune.QGISAPI/ModelingOverlayAnalysis.py .

ENTRYPOINT ["dotnet", "Neptune.QGISAPI.dll"]