FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ./RythuKooliAPI/RythuKooliAPI.csproj ./RythuKooliAPI/
COPY ./Core/Core.csproj ./Core/
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/

RUN dotnet restore ./RythuKooliAPI/RythuKooliAPI.csproj

COPY . .

WORKDIR /src/RythuKooliAPI

RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "RythuKooliAPI.dll"]
