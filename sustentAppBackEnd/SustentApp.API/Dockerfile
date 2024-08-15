#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SustentApp.API/SustentApp.API.csproj", "SustentApp.API/"]
RUN dotnet restore "./SustentApp.API/SustentApp.API.csproj"
COPY . .
WORKDIR "/src/SustentApp.API"
RUN dotnet build "./SustentApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SustentApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false



FROM base AS final
ARG ENVIRONMENT
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SustentApp.API.dll"]