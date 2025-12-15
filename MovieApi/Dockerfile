FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render provides PORT (default 10000). Bind Kestrel to it.
ENTRYPOINT ["sh","-c","ASPNETCORE_URLS=http://+:${PORT:-10000} dotnet MovieApi.dll"]
