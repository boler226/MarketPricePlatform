# ================================
# BUILD MARKET PRICE
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./src/ ./src/
WORKDIR /src/src

RUN dotnet restore MarketPrice.API/MarketPrice.API.csproj
RUN dotnet publish MarketPrice.API/MarketPrice.API.csproj -c Release -o /app

# ========================================
# FINAL IMAGE FOR MARKET PRICE SERVICE API
# ========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MarketPrice.API.dll"]