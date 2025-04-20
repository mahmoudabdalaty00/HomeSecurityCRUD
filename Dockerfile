# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj and restore as distinct layers
COPY Server/*.csproj ./Server/
RUN dotnet restore ./Server/HomeSecurityCRUD.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish ./Server/HomeSecurityCRUD.csproj -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "HomeSecurityCRUD.dll"]
