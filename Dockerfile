# Use the .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy each csproj file and restore as distinct layers
COPY ./PreorderPlatform.Services/PreorderPlatform.Service.csproj ./PreorderPlatform.Services/
COPY ./PreorderPlatform.Entity/PreorderPlatform.Entity.csproj ./PreorderPlatform.Entity/
COPY ./PreorderPlatform.API/PreorderPlatform.API.csproj ./PreorderPlatform.API/

RUN dotnet restore ./PreorderPlatform.Services/PreorderPlatform.Service.csproj
RUN dotnet restore ./PreorderPlatform.Entity/PreorderPlatform.Entity.csproj 
RUN dotnet restore ./PreorderPlatform.API/PreorderPlatform.API.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish ./PreorderPlatform.API/PreorderPlatform.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PreorderPlatform.API.dll"]