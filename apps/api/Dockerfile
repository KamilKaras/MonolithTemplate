FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY MonolithTemplate.sln ./
COPY apps/api ./apps/api

RUN dotnet restore apps/api/MonolithTemplate.Api/MonolithTemplate.Api.csproj

RUN dotnet publish apps/api/MonolithTemplate.Api/MonolithTemplate.Api.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MonolithTemplate.Api.dll"]