# Usar la imagen base de .NET 8.0 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["Sales/Sales.API.csproj", "Sales/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain.Entities/Domain.Entities.csproj", "Domain.Entities/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Sales/Sales.API.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/Sales"
RUN dotnet build "Sales.API.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "Sales.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.API.dll"]

