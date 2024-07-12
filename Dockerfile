
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY CooperationHullMainSite/CooperationHullMainSite.csproj CooperationHullMainSite/
RUN dotnet restore "./CooperationHullMainSite/CooperationHullMainSite.csproj"
COPY . .
WORKDIR "/src/CooperationHullMainSite"
RUN dotnet build "./CooperationHullMainSite.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CooperationHullMainSite.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "CooperationHullMainSite.dll" ]
