FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Rich.Order.Web.Host/Rich.Order.Web.Host.csproj", "Rich.Order.Web.Host/"]
RUN dotnet restore "Rich.Order.Web.Host/Rich.Order.Web.Host.csproj"
COPY . .
WORKDIR "/src/Rich.Order.Web.Host"
RUN dotnet build "Rich.Order.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Rich.Order.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rich.Order.Web.Host.dll"]