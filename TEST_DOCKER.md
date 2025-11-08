# Guía Rápida: Probar Docker Compose

Esta guía te ayudará a probar que Docker Compose funciona correctamente en tu proyecto.

## 📍 Paso 1: Abrir la Terminal

### Opción A: Desde Visual Studio Code
1. Abre Visual Studio Code en la carpeta `Proyecto_Prueba_API`
2. Presiona `Ctrl + Ñ` (o `Ctrl + J`) para abrir la terminal integrada
3. Asegúrate de estar en la raíz del proyecto (deberías ver `Proyecto_Prueba_API` en la ruta)

### Opción B: Desde PowerShell o CMD
1. Abre PowerShell o CMD
2. Navega a la carpeta del proyecto:
   ```powershell
   cd C:\Users\AndresFelipeAlarconT\source\repos\Proyecto_Prueba_API
   ```

### Opción C: Desde el Explorador de Archivos
1. Navega a la carpeta `Proyecto_Prueba_API`
2. Haz clic derecho en la carpeta
3. Selecciona "Abrir en Terminal" o "Open PowerShell window here"

## ✅ Paso 2: Verificar que Docker está ejecutándose

Ejecuta este comando para verificar que Docker Desktop está corriendo:

```powershell
docker --version
```

Deberías ver algo como: `Docker version 24.x.x`

Si no funciona, abre **Docker Desktop** y espera a que esté completamente iniciado (el ícono de Docker en la bandeja del sistema debe estar verde).

## 🏗️ Paso 3: Construir las imágenes de Docker

Este paso construye las imágenes de Docker (puede tardar varios minutos la primera vez):

```powershell
docker-compose build
```

**Qué esperar:**
- Verás mensajes de descarga de imágenes base (.NET SDK, SQL Server)
- Verás la compilación de tu proyecto
- Al final deberías ver mensajes de éxito como "Successfully built" y "Successfully tagged"

**⏱️ Tiempo estimado:** 5-10 minutos la primera vez (depende de tu conexión a internet)

## 🚀 Paso 4: Iniciar los servicios

Una vez que las imágenes estén construidas, inicia los contenedores:

```powershell
docker-compose up -d
```

El flag `-d` ejecuta los contenedores en segundo plano (detached mode).

**Qué esperar:**
- Verás mensajes como "Creating network", "Creating volume", "Creating container"
- Al final deberías ver "sales-sqlserver" y "sales-api" creados

## 📊 Paso 5: Verificar que los contenedores están ejecutándose

Ejecuta este comando para ver el estado de los contenedores:

```powershell
docker-compose ps
```

**Resultado esperado:**
```
NAME                IMAGE                          STATUS
sales-api           proyectopruebaapi-sales-api    Up X seconds
sales-sqlserver     mcr.microsoft.com/mssql/server  Up X seconds (healthy)
```

Ambos contenedores deben estar en estado "Up" y SQL Server debe mostrar "(healthy)".

## 📝 Paso 6: Ver los logs de la API

Para verificar que la API está funcionando correctamente, revisa los logs:

```powershell
docker-compose logs sales-api
```

O para ver los logs en tiempo real:

```powershell
docker-compose logs -f sales-api
```

**Qué buscar:**
- Mensajes como "Now listening on: http://[::]:8080"
- No deberías ver errores críticos
- Si ves errores de conexión a SQL Server, espera unos segundos más (SQL Server tarda en iniciar)

## 🌐 Paso 7: Probar la API en el navegador

Abre tu navegador y visita:

1. **Swagger UI**: http://localhost:8080/swagger
   - Deberías ver la documentación de la API
   - Esto confirma que la API está funcionando

2. **Health Check** (si lo tienes): http://localhost:8080/api/health
   - O simplemente prueba cualquier endpoint

## 🔍 Paso 8: Verificar SQL Server

Para verificar que SQL Server está funcionando, puedes ver sus logs:

```powershell
docker-compose logs sqlserver
```

Deberías ver mensajes como "SQL Server is now ready for client connections".

## 🧪 Paso 9: Probar un endpoint de la API

### Opción A: Desde Swagger
1. Ve a http://localhost:8080/swagger
2. Expande el endpoint `/api/Auth/Login`
3. Haz clic en "Try it out"
4. Ingresa las credenciales:
   ```json
   {
     "username": "pruebaindigo",
     "password": "pruebaindigo12345"
   }
   ```
5. Haz clic en "Execute"
6. Deberías recibir un token JWT como respuesta

### Opción B: Desde PowerShell con curl
```powershell
curl -X POST http://localhost:8080/api/Auth/Login -H "Content-Type: application/json" -d '{\"username\":\"pruebaindigo\",\"password\":\"pruebaindigo12345\"}'
```

## 🛑 Paso 10: Detener los servicios

Cuando termines de probar, detén los contenedores:

```powershell
docker-compose down
```

Esto detiene y elimina los contenedores, pero **mantiene los datos** en el volumen de SQL Server.

Si quieres eliminar también los datos:

```powershell
docker-compose down -v
```

## ❌ Solución de Problemas Comunes

### Error: "docker-compose: command not found"
**Solución:** Asegúrate de que Docker Desktop esté instalado y ejecutándose. En Windows, Docker Compose viene incluido con Docker Desktop.

### Error: "Cannot connect to the Docker daemon" o "request returned 500 Internal Server Error"
**Solución:** 
1. Abre Docker Desktop
2. Espera a que esté completamente iniciado (ícono verde)
3. Si el error persiste:
   - Cierra Docker Desktop completamente
   - Reinicia Docker Desktop como Administrador
   - Espera a que Docker Desktop esté completamente iniciado
   - Intenta de nuevo
4. Si aún no funciona:
   - Ve a Docker Desktop > Settings > General
   - Haz clic en "Restart Docker Desktop"
   - O reinicia tu computadora

### Error: "Port 1433 is already allocated"
**Solución:** Tienes SQL Server ejecutándose localmente. Opciones:
1. Detén tu instancia local de SQL Server
2. O cambia el puerto en `docker-compose.yml` (línea 13) a otro puerto, por ejemplo: `"1434:1433"`

### Error: "Port 8080 is already allocated"
**Solución:** Tienes otra aplicación usando el puerto 8080. Opciones:
1. Detén la otra aplicación
2. O cambia el puerto en `docker-compose.yml` (línea 42) a otro puerto, por ejemplo: `"8082:8080"`

### Error: "Build failed" o errores de compilación
**Solución:**
1. Verifica que todos los proyectos compilen localmente:
   ```powershell
   dotnet build ProductSales.sln
   ```
2. Si hay errores, corrígelos primero
3. Luego intenta construir Docker de nuevo

### La API no puede conectarse a SQL Server
**Solución:**
1. Verifica que SQL Server esté saludable:
   ```powershell
   docker-compose ps
   ```
2. Espera unos segundos más (SQL Server puede tardar 30-60 segundos en iniciar completamente)
3. Revisa los logs de SQL Server:
   ```powershell
   docker-compose logs sqlserver
   ```

### Error: "No space left on device"
**Solución:** Docker está usando mucho espacio. Limpia imágenes y contenedores no utilizados:
```powershell
docker system prune -a
```

## 📋 Comandos de Referencia Rápida

```powershell
# Construir imágenes
docker-compose build

# Iniciar servicios
docker-compose up -d

# Ver estado
docker-compose ps

# Ver logs de la API
docker-compose logs -f sales-api

# Ver logs de SQL Server
docker-compose logs -f sqlserver

# Detener servicios
docker-compose down

# Detener y eliminar datos
docker-compose down -v

# Reconstruir desde cero
docker-compose build --no-cache
docker-compose up -d

# Ver uso de recursos
docker stats
```

## ✅ Checklist de Verificación

Marca cada paso cuando lo completes:

- [ ] Docker Desktop está ejecutándose
- [ ] `docker --version` funciona
- [ ] `docker-compose build` completó sin errores
- [ ] `docker-compose up -d` inició los contenedores
- [ ] `docker-compose ps` muestra ambos contenedores "Up"
- [ ] SQL Server muestra estado "(healthy)"
- [ ] Puedo acceder a http://localhost:8080/swagger
- [ ] El endpoint de Login funciona correctamente
- [ ] Los logs no muestran errores críticos

Si todos los pasos están marcados, ¡Docker está funcionando correctamente! 🎉

