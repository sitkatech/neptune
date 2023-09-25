FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY ./Neptune.Models ./Neptune.Models
COPY ./Neptune.EFModels ./Neptune.EFModels
COPY ./Neptune.API ./Neptune.API

RUN dotnet publish ./Neptune.API/Neptune.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app /app
EXPOSE 80
ENTRYPOINT ["dotnet", "Neptune.API.dll"]
