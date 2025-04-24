# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project and restore
COPY RythuKooliAPI.sln ./
COPY Core/Core.csproj Core/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY RythuKooliAPI/RythuKooliAPI.csproj RythuKooliAPI/
RUN dotnet restore RythuKooliAPI.sln

# Copy everything else
COPY . .

# Publish the app
WORKDIR /src/RythuKooliAPI
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "RythuKooliAPI.dll"]
