FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Rich.Order.Host/Rich.Order.Host.csproj", "Rich.Order.Host/"]
RUN dotnet restore "Rich.Order.Host/Rich.Order.Host.csproj"
COPY . .
WORKDIR "/src/Rich.Order.Host"
RUN dotnet build "Rich.Order.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Rich.Order.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rich.Order.Host.dll"]