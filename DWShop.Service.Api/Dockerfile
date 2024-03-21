#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DWShop.Service.Api/DWShop.Service.Api.csproj", "DWShop.Service.Api/"]
COPY ["DWShop.Application/DWShop.Application.csproj", "DWShop.Application/"]
COPY ["DwShop.Domain/DWShop.Domain.csproj", "DwShop.Domain/"]
COPY ["DWShop.Shared/DWShop.Shared.csproj", "DWShop.Shared/"]
COPY ["DWShop.Infrastructure/DWShop.Infrastructure.csproj", "DWShop.Infrastructure/"]
RUN dotnet restore "./DWShop.Service.Api/DWShop.Service.Api.csproj"
COPY . .
WORKDIR "/src/DWShop.Service.Api"
RUN dotnet build "./DWShop.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DWShop.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DWShop.Service.Api.dll"]