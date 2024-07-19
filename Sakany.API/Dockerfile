# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj and restore dependencies
COPY ["Sakany.API/Sakany.API.csproj", "Sakany.API/"]
COPY ["Sakany.Presentation/Sakany.Presentation.csproj", "Sakany.Presentation/"]
COPY ["Sakany.Application/Sakany.Application.csproj", "Sakany.Application/"]
COPY ["Sakany.Domain/Sakany.Domain.csproj", "Sakany.Domain/"]
COPY ["Sakany.Shared/Sakany.Shared.csproj", "Sakany.Shared/"]
COPY ["Sakany.Infrastructure/Sakany.Infrastructure.csproj", "Sakany.Infrastructure/"]
COPY ["Sakany.Persistence/Sakany.Persistence.csproj", "Sakany.Persistence/"]
RUN dotnet restore "Sakany.API/Sakany.API.csproj"

# Copy the remaining source code and build the app
COPY . .
WORKDIR "/src/Sakany.API"
RUN dotnet build "./Sakany.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app to the /app/publish directory
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sakany.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Copy the published app to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Sakany.API.dll"]
