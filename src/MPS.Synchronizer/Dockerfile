#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MPS.Synchronizer/MPS.Synchronizer.csproj", "MPS.Synchronizer/"]
RUN dotnet restore "./MPS.Synchronizer/MPS.Synchronizer.csproj"
COPY . .
WORKDIR "/src/MPS.Synchronizer"
RUN dotnet build "./MPS.Synchronizer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MPS.Synchronizer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MPS.Synchronizer.dll"]