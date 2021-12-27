#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NetCoreWebApp/NetCoreWebApp.Identity.Microservice/NetCoreWebApp.Identity.Microservice.csproj", "NetCoreWebApp/NetCoreWebApp.Identity.Microservice/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Application/NetCoreWebApp.Application.csproj", "NetCoreWebApp/NetCoreWebApp.Application/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Domain/NetCoreWebApp.Domain.csproj", "NetCoreWebApp/NetCoreWebApp.Domain/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Infrastructure.Identity/NetCoreWebApp.Infrastructure.Identity.csproj", "NetCoreWebApp/NetCoreWebApp.Infrastructure.Identity/"]
COPY ["NetCoreWebApp/NetCoreWebApp.Infrastructure.Shared/NetCoreWebApp.Infrastructure.Shared.csproj", "NetCoreWebApp/NetCoreWebApp.Infrastructure.Shared/"]
RUN dotnet restore "NetCoreWebApp/NetCoreWebApp.Identity.Microservice/NetCoreWebApp.Identity.Microservice.csproj"
COPY . .
WORKDIR "/src/NetCoreWebApp/NetCoreWebApp.Identity.Microservice"
RUN dotnet build "NetCoreWebApp.Identity.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreWebApp.Identity.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreWebApp.Identity.Microservice.dll"]
