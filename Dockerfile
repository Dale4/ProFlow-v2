# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/ProFlow.Api/ProFlow.Api.csproj", "src/ProFlow.Api/"]
RUN dotnet restore "src/ProFlow.Api/ProFlow.Api.csproj"

COPY . .
RUN dotnet publish "src/ProFlow.Api/ProFlow.Api.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProFlow.Api.dll"]
