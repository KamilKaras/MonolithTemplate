# ===== STAGE 1: build =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY MonolithTemplate.sln ./
COPY apps/api/Api/Api.csproj apps/api/Api/

RUN dotnet restore apps/api/Api/Api.csproj

COPY . .

RUN dotnet publish apps/api/Api/Api.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
