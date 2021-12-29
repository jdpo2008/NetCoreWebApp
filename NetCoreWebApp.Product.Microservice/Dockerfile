#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NetCoreWebApp.Product.Microservice/NetCoreWebApp.Product.Microservice.csproj", "NetCoreWebApp.Product.Microservice/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Infrastructure.Shared/NetCoreWebApp.Infrastructure.Shared.csproj", "NetCoreWebApp/NetCoreWebApp.Infrastructure.Shared/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Application/NetCoreWebApp.Application.csproj", "NetCoreWebApp/NetCoreWebApp.Application/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Domain/NetCoreWebApp.Domain.csproj", "NetCoreWebApp/NetCoreWebApp.Domain/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Infrastructure.Persistence/NetCoreWebApp.Infrastructure.Persistence.csproj", "NetCoreWebApp/NetCoreWebApp.Infrastructure.Persistence/"]
RUN dotnet restore "NetCoreWebApp.Product.Microservice/NetCoreWebApp.Product.Microservice.csproj"
COPY . .
WORKDIR "/src/NetCoreWebApp.Product.Microservice"
RUN dotnet build "NetCoreWebApp.Product.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreWebApp.Product.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreWebApp.Product.Microservice.dll"]
