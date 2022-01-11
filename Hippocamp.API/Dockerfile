FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY ./Hippocamp.Models ./Hippocamp.Models
COPY ./Hippocamp.EFModels ./Hippocamp.EFModels
COPY ./Hippocamp.API ./Hippocamp.API

RUN dotnet publish ./Hippocamp.API/Hippocamp.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS final
WORKDIR /app
COPY --from=build /app /app
RUN ls -la
EXPOSE 80
ENTRYPOINT ["dotnet", "Hippocamp.API.dll"]
