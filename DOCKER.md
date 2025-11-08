# Guía de Docker para Sales API

Esta guía explica cómo usar Docker y Docker Compose para ejecutar la aplicación Sales API.

## 📋 Requisitos Previos

- **Docker Desktop** instalado y ejecutándose
- **Docker Compose** (incluido en Docker Desktop)
- Al menos **4GB de RAM** disponibles para Docker

## 🚀 Inicio Rápido

### 1. Construir y ejecutar con Docker Compose

```bash
# Construir y levantar todos los servicios
docker-compose up -d

# Ver los logs
docker-compose logs -f sales-api

# Detener los servicios
docker-compose down
```

### 2. Acceder a la aplicación

Una vez que los contenedores estén ejecutándose:

- **API**: http://localhost:8080
- **Swagger UI**: http://localhost:8080/swagger
- **SQL Server**: localhost:1433

## ⚙️ Configuración

### Variables de Entorno

El archivo `docker-compose.yml` incluye configuraciones por defecto. Para personalizar la configuración:

1. Copia `docker-compose.override.yml.example` a `docker-compose.override.yml`:
   ```bash
   cp docker-compose.override.yml.example docker-compose.override.yml
   ```

2. Edita `docker-compose.override.yml` con tus valores:
   ```yaml
   services:
     sales-api:
       environment:
         - ConnectionStrings__Conexion=Server=TU_SERVIDOR;Database=prueba_indigo;...
         - Storage__BlobSasUrl=TU_URL_SAS
         - JwtSettings__SecretKey=TU_CLAVE_SECRETA
   ```

**Nota:** `docker-compose.override.yml` está en `.gitignore` y no se subirá al repositorio.

### Configuración de SQL Server

Por defecto, el contenedor de SQL Server usa:
- **Usuario**: `sa`
- **Contraseña**: `YourStrong@Password123`
- **Puerto**: `1433`
- **Base de datos**: Se creará automáticamente al ejecutar migraciones

Para cambiar la contraseña de SQL Server, edita `docker-compose.yml`:

```yaml
sqlserver:
  environment:
    - SA_PASSWORD=TuNuevaContraseñaSegura123
```

Y actualiza la cadena de conexión en `docker-compose.override.yml`:

```yaml
sales-api:
  environment:
    - ConnectionStrings__Conexion=Server=sqlserver;Database=prueba_indigo;User Id=sa;Password=TuNuevaContraseñaSegura123;TrustServerCertificate=True;
```

## 📦 Servicios Incluidos

### 1. SQL Server (`sqlserver`)
- **Imagen**: `mcr.microsoft.com/mssql/server:2022-latest`
- **Puerto**: `1433`
- **Volumen**: `sqlserver_data` (persistencia de datos)

### 2. Sales API (`sales-api`)
- **Puerto**: `8080` (HTTP)
- **Puerto**: `8081` (HTTPS)
- **Depende de**: `sqlserver`

## 🔧 Comandos Útiles

### Construir y ejecutar

```bash
# Construir las imágenes
docker-compose build

# Levantar los servicios en segundo plano
docker-compose up -d

# Levantar y ver los logs
docker-compose up

# Reconstruir y levantar
docker-compose up -d --build
```

### Gestión de contenedores

```bash
# Ver contenedores en ejecución
docker-compose ps

# Ver logs de un servicio específico
docker-compose logs -f sales-api
docker-compose logs -f sqlserver

# Detener servicios
docker-compose stop

# Detener y eliminar contenedores
docker-compose down

# Detener y eliminar contenedores, volúmenes e imágenes
docker-compose down -v --rmi all
```

### Base de datos

```bash
# Ejecutar migraciones (si las tienes)
docker-compose exec sales-api dotnet ef database update

# Conectarse a SQL Server desde el host
# Usa SQL Server Management Studio o Azure Data Studio
# Servidor: localhost,1433
# Usuario: sa
# Contraseña: YourStrong@Password123
```

### Limpieza

```bash
# Eliminar contenedores detenidos
docker-compose rm

# Eliminar imágenes no utilizadas
docker image prune

# Limpieza completa (¡cuidado! elimina todo)
docker system prune -a --volumes
```

## 🗄️ Persistencia de Datos

Los datos de SQL Server se almacenan en un volumen de Docker llamado `sqlserver_data`. Esto significa que los datos persisten incluso si detienes y eliminas los contenedores.

Para eliminar los datos:

```bash
docker-compose down -v
```

## 🔍 Solución de Problemas

### El contenedor de SQL Server no inicia

1. Verifica que el puerto 1433 no esté en uso:
   ```bash
   netstat -an | findstr 1433
   ```

2. Verifica los logs:
   ```bash
   docker-compose logs sqlserver
   ```

3. Asegúrate de que Docker tenga suficientes recursos asignados (mínimo 4GB RAM).

### La API no puede conectarse a SQL Server

1. Verifica que SQL Server esté saludable:
   ```bash
   docker-compose ps
   ```

2. Verifica la cadena de conexión en las variables de entorno.

3. Espera unos segundos después de que SQL Server inicie antes de iniciar la API.

### Error de permisos en Windows

Si tienes problemas con permisos en Windows:

1. Ejecuta Docker Desktop como administrador.
2. Verifica que WSL 2 esté habilitado (Docker Desktop > Settings > General > Use WSL 2 based engine).

### Reconstruir desde cero

Si necesitas empezar de nuevo:

```bash
# Detener y eliminar todo
docker-compose down -v

# Eliminar imágenes
docker rmi $(docker images -q sales-api)

# Reconstruir
docker-compose build --no-cache
docker-compose up -d
```

## 📝 Notas Importantes

1. **Seguridad**: Las contraseñas por defecto son solo para desarrollo. En producción, usa contraseñas fuertes y variables de entorno seguras.

2. **Puertos**: Si los puertos 1433, 8080 o 8081 están en uso, cámbialos en `docker-compose.yml`.

3. **Base de datos**: La base de datos se crea automáticamente si usas Code First Migrations. Si no, créala manualmente.

4. **Azure Blob Storage**: Si no configuras `Storage__BlobSasUrl`, las imágenes no se guardarán, pero el resto de la funcionalidad funcionará.

5. **Logs**: Los logs de la aplicación se pueden ver con `docker-compose logs -f sales-api`.

## 🔗 Enlaces Útiles

- [Documentación de Docker Compose](https://docs.docker.com/compose/)
- [Imagen de SQL Server en Docker Hub](https://hub.docker.com/_/microsoft-mssql-server)
- [.NET Docker Hub](https://hub.docker.com/_/microsoft-dotnet)

