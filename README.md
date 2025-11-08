# Sistema de Gestión de Ventas e Inventario

Sistema completo de gestión de ventas e inventario desarrollado con **ASP.NET Core Web API** (Backend) y **Windows Forms** (Frontend), implementando **Clean Architecture** y autenticación **JWT**.

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Requisitos Previos](#-requisitos-previos)
- [Configuración](#-configuración)
- [Pasos de Ejecución](#-pasos-de-ejecución)
- [Usuarios de Prueba](#-usuarios-de-prueba)
- [Endpoints de la API](#-endpoints-de-la-api)
- [Estructura de la Base de Datos](#-estructura-de-la-base-de-datos)
- [Pruebas Unitarias](#-pruebas-unitarias)
- [Logging](#-logging)

## ✨ Características

- ✅ **CRUD completo** de productos de inventario
- ✅ **CRUD completo** de ventas
- ✅ **Gestión de imágenes** con Azure Blob Storage
- ✅ **Validación de stock** al realizar ventas
- ✅ **Actualización automática** del precio del producto al vender
- ✅ **Reducción automática de stock** al crear ventas
- ✅ **Eliminación automática de imágenes** del blob storage al eliminar productos
- ✅ **Reportes de ventas** por rango de fechas
- ✅ **Exportación a Excel** de reportes
- ✅ **Autenticación JWT** para seguridad
- ✅ **Interfaz Windows Forms** intuitiva y moderna
- ✅ **Logging estructurado** con Serilog
- ✅ **Pruebas unitarias** con xUnit y Moq

## 🛠️ Tecnologías Utilizadas

### Backend
- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **SQL Server**
- **Azure Blob Storage**
- **JWT Authentication**
- **Serilog** (Logging estructurado)

### Frontend
- **Windows Forms (.NET 8.0)**
- **EPPlus** (Exportación a Excel)
- **HttpClient** para consumo de API

### Testing
- **xUnit** (Framework de pruebas)
- **Moq** (Mocking framework)

### Arquitectura
- **Clean Architecture** (Domain, Application, Infrastructure, API)

## 📁 Estructura del Proyecto

```
Proyecto_Prueba_API/
├── Sales/                          # Capa de Presentación (API)
│   ├── Controllers/                # Controladores de la API
│   │   ├── AuthController.cs
│   │   ├── InventoryProductController.cs
│   │   └── SaleController.cs
│   ├── Middlewares/                # Middlewares personalizados
│   ├── Program.cs                  # Configuración de la aplicación
│   ├── appsettings.json           # Configuración (NO incluir en Git)
│   ├── appsettings.example.json   # Plantilla de configuración
│   └── Sales.API.csproj
│
├── Application/                    # Capa de Aplicación
│   ├── DTOs/                       # Data Transfer Objects
│   │   ├── CreateInventoryProductDto.cs
│   │   ├── CreateSaleDto.cs
│   │   ├── LoginDto.cs
│   │   └── UpdateInventoryProductDto.cs
│   ├── Interfaces/                 # Interfaces de servicios
│   │   ├── IAuthService.cs
│   │   ├── IInventoryProductService.cs
│   │   └── ISaleService.cs
│   ├── Services/                   # Implementación de servicios
│   │   ├── AuthService.cs
│   │   ├── InventoryProductService.cs
│   │   └── SaleService.cs
│   └── Application.csproj
│
├── Domain.Entities/                # Capa de Dominio
│   ├── Entities/                  # Entidades del dominio
│   │   ├── InventoryProduct.cs
│   │   ├── Sale.cs
│   │   └── SalesDetail.cs
│   ├── Interfaces/                # Interfaces de repositorios y servicios
│   │   ├── IBlobStorageService.cs
│   │   ├── IInventoryProductRepository.cs
│   │   ├── IRepository.cs
│   │   └── ISaleRepository.cs
│   └── Domain.Entities.csproj
│
├── Infrastructure/                # Capa de Infraestructura
│   ├── Data/                      # DbContext y Repositorios
│   │   ├── PruebaIndigoContext.cs
│   │   └── Repositories/
│   │       ├── InventoryProductRepository.cs
│   │       └── SaleRepository.cs
│   ├── Services/                  # Servicios externos
│   │   └── BlobStorageService.cs
│   └── Infrastructure.csproj
│
├── SalesWinForms/                 # Aplicación Windows Forms
│   ├── Forms/                     # Formularios de la aplicación
│   │   ├── LoginForm.cs
│   │   ├── MainForm.cs
│   │   ├── ProductsForm.cs
│   │   ├── SalesForm.cs
│   │   └── SalesReportForm.cs
│   ├── Models/                    # Modelos/DTOs del frontend
│   │   ├── InventoryProduct.cs
│   │   ├── LoginDto.cs
│   │   └── Sale.cs
│   ├── Services/                  # Servicio para consumo de API
│   │   └── ApiService.cs
│   ├── Program.cs                 # Punto de entrada
│   ├── appsettings.json          # Configuración de URL de API
│   └── SalesWinForms.csproj
│
├── UnitTesting/                   # Proyecto de Pruebas Unitarias
│   ├── Services/
│   │   ├── AuthServiceTests.cs
│   │   ├── InventoryProductServiceTests.cs
│   │   └── SaleServiceTests.cs
│   └── UnitTesting.csproj
│
├── ProductSales.sln               # Solución de Visual Studio
├── docker-compose.yml             # Configuración de Docker Compose
├── docker-compose.override.yml.example  # Plantilla para configuración personalizada
├── Sales/Dockerfile               # Dockerfile para la API
├── .dockerignore                 # Archivos ignorados en el contexto de Docker
├── .gitignore                    # Archivos ignorados por Git
├── README.md                     # Este archivo
└── DOCKER.md                     # Guía detallada de Docker
```

## 📋 Requisitos Previos

### Requisitos Mínimos:
- **.NET 8.0 SDK** o superior (si ejecutas sin Docker)
- **Visual Studio 2022** o **Visual Studio Code** (si ejecutas sin Docker)
- **SQL Server** (local o remoto) - O usar Docker Compose que incluye SQL Server
- **Cuenta de Azure** (para Blob Storage) - Opcional si se configura
- **Git** (para clonar el repositorio)

### Para Docker (Recomendado):
- **Docker Desktop** instalado y ejecutándose
- **Docker Compose** (incluido en Docker Desktop)
- Al menos **4GB de RAM** disponibles para Docker

## ⚙️ Configuración

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd Proyecto_Prueba_API
```

### 2. Configurar la Base de Datos

1. Copiar `Sales/appsettings.example.json` a `Sales/appsettings.json`
2. Actualizar la cadena de conexión en `ConnectionStrings:Conexion`:

```json
{
  "ConnectionStrings": {
    "Conexion": "Server=TU_SERVIDOR;Database=prueba_indigo;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;TrustServerCertificate=True;"
  }
}
```

**⚠️ IMPORTANTE:** El archivo `appsettings.json` está en `.gitignore` para proteger información sensible. **NUNCA** subas credenciales reales al repositorio. Usa `appsettings.example.json` como plantilla.

### 3. Configurar Azure Blob Storage (Opcional)

Si deseas usar Azure Blob Storage para imágenes:

1. Obtén una URL SAS de tu contenedor de Azure Blob Storage con permisos de lectura, escritura y eliminación (`racwdli`)
2. Actualiza `Sales/appsettings.json`:

```json
{
  "Storage": {
    "BlobSasUrl": "TU_URL_SAS_AQUI",
    "BlobContainer": "nombre-del-contenedor"
  }
}
```

**Nota:** Si no configuras Blob Storage, las imágenes no se guardarán, pero el resto de la funcionalidad funcionará.

### 4. Configurar JWT

El proyecto requiere una clave secreta JWT. Configúrala en `Sales/appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "TU_CLAVE_SECRETA_SUPER_SEGURA_DE_AL_MENOS_32_CARACTERES",
    "Issuer": "SalesAPI",
    "Audience": "SalesClient",
    "ExpirationMinutes": 60
  }
}
```

**⚠️ IMPORTANTE:** En producción, usa una clave secreta fuerte y segura de al menos 32 caracteres. Genera una clave aleatoria y segura.

### 5. Configurar URL de la API en WinForms

Si la API se ejecuta en un puerto diferente, actualiza `SalesWinForms/appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7263/api"
  }
}
```

## 🚀 Pasos de Ejecución

### Opción 1: Ejecutar con Docker Compose (Recomendado)

La forma más fácil de ejecutar toda la aplicación es usando Docker Compose:

```bash
# Construir y levantar todos los servicios (SQL Server + API)
docker-compose up -d

# Ver los logs
docker-compose logs -f sales-api

# Detener los servicios
docker-compose down
```

**Acceso:**
- API: http://localhost:8080
- Swagger UI: http://localhost:8080/swagger
- SQL Server: localhost:1433

**Configuración personalizada:**
1. Copia `docker-compose.override.yml.example` a `docker-compose.override.yml`
2. Edita `docker-compose.override.yml` con tus valores (Azure Blob Storage, JWT, etc.)

Para más detalles sobre Docker, consulta [DOCKER.md](DOCKER.md).

### Opción 2: Ejecutar desde Visual Studio

1. **Abrir la solución:**
   - Abrir `ProductSales.sln` en Visual Studio 2022

2. **Restaurar paquetes NuGet:**
   - Click derecho en la solución → **Restaurar paquetes NuGet**
   - O ejecutar: `dotnet restore`

3. **Configurar proyectos de inicio múltiples:**
   - Click derecho en la solución → **Propiedades**
   - En **Proyectos de inicio**, seleccionar:
     - `Sales.API` - **Iniciar**
     - `SalesWinForms` - **Iniciar** (con retraso de 3 segundos)

4. **Ejecutar la aplicación:**
   - Presionar **F5** o **Iniciar**
   - La API se iniciará en `https://localhost:7263`
   - La aplicación WinForms se abrirá automáticamente después de 3 segundos

### Opción 2: Ejecutar desde la Terminal

#### Terminal 1 - Ejecutar la API:

```bash
cd Sales
dotnet restore
dotnet run --project Sales.API.csproj
```

La API estará disponible en:
- HTTPS: `https://localhost:7263`
- Swagger UI: `https://localhost:7263/swagger`

#### Terminal 2 - Ejecutar WinForms:

```bash
cd SalesWinForms
dotnet restore
dotnet run
```

### Opción 3: Compilar y Ejecutar Manualmente

```bash
# Restaurar paquetes
dotnet restore ProductSales.sln

# Compilar la solución
dotnet build ProductSales.sln

# Ejecutar la API
cd Sales
dotnet run

# En otra terminal, ejecutar WinForms
cd SalesWinForms
dotnet run
```

## 👤 Usuarios de Prueba

El sistema incluye usuarios predefinidos para pruebas:

| Usuario          | Contraseña           |
|------------------|----------------------|
| `pruebaindigo`   | `pruebaindigo12345`  |
| `usuario`        | `usuario123`         |
| `test`           | `test123`            |

**⚠️ Nota:** Estos usuarios están hardcodeados en `Application/Services/AuthService.cs`

## 🔌 Endpoints de la API

### Autenticación

- **POST** `/api/Auth/Login` - Iniciar sesión y obtener token JWT
  ```json
  {
    "username": "pruebaindigo",
    "password": "pruebaindigo12345"
  }
  ```
  **Respuesta:**
  ```json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2025-01-15T10:30:00Z"
  }
  ```

### Productos (Requiere Autenticación)

- **GET** `/api/InventoryProduct/GetList` - Obtener todos los productos
- **GET** `/api/InventoryProduct/GetById?id={id}` - Obtener producto por ID
- **POST** `/api/InventoryProduct/Add` - Crear producto (multipart/form-data)
  - Campos: `Name`, `Price`, `Stock`, `imageFile` (opcional)
- **PUT** `/api/InventoryProduct/Update?id={id}` - Actualizar producto (multipart/form-data)
  - Campos: `Name`, `Price`, `Stock`, `imageFile` (opcional)
- **DELETE** `/api/InventoryProduct/Delete/{id}` - Eliminar producto (también elimina la imagen del blob storage)

### Ventas (Requiere Autenticación)

- **GET** `/api/Sale/GetList` - Obtener todas las ventas
- **GET** `/api/Sale/GetById?id={id}` - Obtener venta por ID
- **POST** `/api/Sale/Add` - Crear venta
  ```json
  {
    "creationUser": "pruebaindigo",
    "details": [
      {
        "productId": 1,
        "quantity": 2,
        "unitPrice": 15000.00
      }
    ]
  }
  ```
- **DELETE** `/api/Sale/Delete/{id}` - Eliminar venta
- **GET** `/api/Sale/GetByDateRange?startDate={fecha}&endDate={fecha}` - Obtener ventas por rango de fechas
  - Formato de fecha: `yyyy-MM-dd` (ejemplo: `2025-01-01`)

### Documentación Swagger

Una vez ejecutada la API, accede a la documentación interactiva en:
```
https://localhost:7263/swagger
```

**Nota:** Para probar endpoints protegidos en Swagger:
1. Primero haz login en `/api/Auth/Login`
2. Copia el token de la respuesta
3. Haz clic en el botón **"Authorize"** en Swagger
4. Ingresa: `Bearer {tu-token}`

## 🗄️ Estructura de la Base de Datos

### Tabla: InventoryProduct
- `Id` (int, PK, Identity)
- `Name` (string, max 400)
- `Price` (decimal, nullable) - Precio del producto
- `Stock` (int, nullable) - Stock disponible
- `Image` (string, nullable) - URL de la imagen en Blob Storage

### Tabla: Sales
- `Id` (int, PK, Identity)
- `CreationDate` (datetime) - Fecha de creación de la venta
- `CreationUser` (string, max 50) - Usuario que creó la venta
- `Total` (decimal) - Total de la venta

### Tabla: SalesDetails
- `Id` (int, PK, Identity)
- `SaleId` (int, FK) - Referencia a Sales
- `ProductId` (int, FK) - Referencia a InventoryProduct
- `Quantity` (int) - Cantidad vendida
- `UnitPrice` (decimal) - Precio unitario al momento de la venta

## 🧪 Pruebas Unitarias

El proyecto incluye pruebas unitarias usando **xUnit** y **Moq**. Para ejecutar las pruebas:

```bash
cd UnitTesting
dotnet test
```

O desde Visual Studio:
- Click derecho en el proyecto `UnitTesting` → **Ejecutar Pruebas**

### Cobertura de Pruebas

Las pruebas unitarias cubren:
- ✅ `AuthService` - Autenticación y generación de tokens JWT
- ✅ `InventoryProductService` - CRUD de productos, manejo de imágenes
- ✅ `SaleService` - Creación de ventas, validación de stock, filtrado por fechas

## 📊 Logging

El proyecto utiliza **Serilog** para logging estructurado. Los logs se configuran en `Sales/appsettings.json`:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

Los logs incluyen:
- Información de inicio de la aplicación
- Errores de autenticación
- Operaciones de productos y ventas
- Errores de blob storage

## 🔐 Seguridad

- ✅ Todos los endpoints (excepto Login) requieren autenticación JWT
- ✅ El token JWT tiene una validez de 60 minutos (configurable)
- ✅ Validación de Issuer y Audience en tokens JWT
- ✅ Las contraseñas están hardcodeadas (⚠️ cambiar en producción)
- ✅ Se recomienda usar HTTPS en producción
- ✅ `appsettings.json` está en `.gitignore` para proteger credenciales

## 📝 Notas Importantes

1. **Primera Ejecución:**
   - Asegúrate de que la base de datos exista y esté accesible
   - Las tablas se crearán automáticamente si usas Code First Migrations
   - Copia `Sales/appsettings.example.json` a `Sales/appsettings.json` y configura tus valores

2. **Archivos Sensibles:**
   - `Sales/appsettings.json` está en `.gitignore` para proteger información sensible
   - `Sales/appsettings.Development.json` también está ignorado
   - Usa `Sales/appsettings.example.json` como plantilla
   - **⚠️ NUNCA subas credenciales reales al repositorio**

3. **Certificados SSL:**
   - En desarrollo, la aplicación WinForms ignora errores de certificados SSL
   - En producción, configura certificados válidos

4. **Azure Blob Storage:**
   - Las imágenes se guardan en la carpeta `aalarcon@indigo.tech/`
   - Al eliminar un producto, su imagen también se elimina del blob storage
   - La SAS URL debe tener permisos de eliminación (`d`) para que funcione correctamente
   - Si no configuras Blob Storage, el campo Image quedará vacío

5. **Múltiples Proyectos de Inicio:**
   - Visual Studio puede configurarse para iniciar ambos proyectos simultáneamente
   - El archivo `ProductSales.slnLaunch.user` contiene la configuración de retraso (está en `.gitignore`)

6. **Validaciones de Negocio:**
   - El sistema valida que el stock sea suficiente antes de crear una venta
   - El stock se reduce automáticamente al crear una venta
   - El precio del producto se actualiza automáticamente con el último precio de venta

## 🐛 Solución de Problemas

### Error: "No se puede establecer una conexión"
- Verifica que la API esté ejecutándose
- Verifica la URL en `SalesWinForms/appsettings.json` (por defecto: `https://localhost:7263/api`)
- Verifica que el firewall permita la conexión
- Verifica que el certificado SSL sea válido (en desarrollo se ignoran errores)

### Error: "401 Unauthorized"
- Verifica que hayas iniciado sesión correctamente
- Verifica que el token no haya expirado (60 minutos)
- Cierra y vuelve a abrir la aplicación para obtener un nuevo token
- Verifica que el token tenga el formato correcto: `Bearer {token}`

### Error: "Stock insuficiente"
- Verifica que el producto tenga stock disponible
- El sistema valida el stock antes de crear la venta
- El stock debe ser mayor o igual a la cantidad solicitada

### Error: "No se pudo eliminar la imagen del blob storage"
- Verifica que la SAS URL tenga permisos de eliminación (`d`)
- Verifica que la URL de la imagen sea válida
- Revisa los logs de la aplicación para más detalles

### Error: "JWT Audience está vacío"
- Verifica que `JwtSettings:Audience` esté configurado en `appsettings.json`
- Verifica que `JwtSettings:Issuer` esté configurado
- Asegúrate de que ambos valores coincidan con la configuración de validación

## 📄 Licencia

Este proyecto es de uso interno.

## 👥 Autor

Desarrollado para Indigo Tech

---

**Última actualización:** Enero 2025
