FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY ./Hippocamp.Models ./Hippocamp.Models
COPY ./Hippocamp.EFModels ./Hippocamp.EFModels
COPY ./Hippocamp.API ./Hippocamp.API

RUN dotnet publish ./Hippocamp.API/Hippocamp.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app /app
EXPOSE 80
ENTRYPOINT ["dotnet", "Hippocamp.API.dll"]
