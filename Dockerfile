# Use the .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy each csproj file and restore as distinct layers
COPY ./PreOrderPlatform.Services/PreOrderPlatform.Service.csproj ./PreOrderPlatform.Services/
COPY ./PreOrderPlatform.Entity/PreOrderPlatform.Entity.csproj ./PreOrderPlatform.Entity/
COPY ./PreOrderPlatform.API/PreOrderPlatform.API.csproj ./PreOrderPlatform.API/

RUN dotnet restore ./PreOrderPlatform.Services/PreOrderPlatform.Service.csproj
RUN dotnet restore ./PreOrderPlatform.Entity/PreOrderPlatform.Entity.csproj 
RUN dotnet restore ./PreOrderPlatform.API/PreOrderPlatform.API.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish ./PreOrderPlatform.API/PreOrderPlatform.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PreOrderPlatform.API.dll"]