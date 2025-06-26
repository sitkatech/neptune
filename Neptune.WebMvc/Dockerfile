#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.22-amd64 AS base
#https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md#alpine-images
#Some Alpine Images are by design released without globalization. In order for our application to run, we need to add it manually. 
#Done to reduce vulnerabilities
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false LC_ALL=en_US.UTF-8 LANG=en_US.UTF-8
RUN apk add --no-cache icu-data-full icu-libs
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.22-amd64 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Neptune.WebMvc/Neptune.WebMvc.csproj", "Neptune.WebMvc/"]

RUN dotnet restore "./Neptune.WebMvc/./Neptune.WebMvc.csproj"
COPY . .
WORKDIR "/src/Neptune.WebMvc"
RUN dotnet build "./Neptune.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Neptune.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neptune.WebMvc.dll"]