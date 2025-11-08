# Sales WinForms - Frontend

Aplicación de escritorio Windows Forms para gestionar productos y ventas, consumiendo la API de Sales.

## Características

- ✅ CRUD completo de Productos
- ✅ Carga y visualización de imágenes
- ✅ Interfaz intuitiva y moderna
- ✅ Validación de datos
- ✅ Manejo de errores

## Requisitos

- .NET 8.0
- Visual Studio 2022 o superior
- La API de Sales debe estar ejecutándose

## Configuración

### URL de la API

La URL base de la API se configura en `Services/ApiService.cs`. Por defecto está configurada para:

```
https://localhost:7263/api
```

Si necesitas cambiarla, modifica la constante `BaseUrl` en el constructor de `ApiService` o pasa la URL como parámetro.

## Estructura del Proyecto

```
SalesWinForms/
├── Forms/
│   └── ProductsForm.cs          # Formulario principal de productos
├── Models/
│   └── InventoryProduct.cs      # Modelos y DTOs
├── Services/
│   └── ApiService.cs            # Servicio para consumir la API
└── Program.cs                   # Punto de entrada
```

## Funcionalidades del Formulario de Productos

### Lista de Productos
- Muestra todos los productos en una tabla (DataGridView)
- Selección de producto para ver/editar detalles
- Botón de actualizar para refrescar la lista

### Detalles del Producto
- **ID**: Identificador único (solo lectura)
- **Nombre**: Nombre del producto (requerido)
- **Precio**: Precio del producto (opcional)
- **Stock**: Cantidad en inventario (opcional)
- **URL Imagen**: URL de la imagen almacenada en Blob Storage (solo lectura)
- **Ruta Imagen**: Ruta local del archivo de imagen seleccionado
- **Vista Previa**: PictureBox que muestra la imagen del producto

### Operaciones CRUD

1. **Crear (Nuevo)**: Limpia el formulario para crear un nuevo producto
2. **Guardar**: Crea o actualiza el producto según el contexto
3. **Editar**: Habilita la edición del producto seleccionado
4. **Eliminar**: Elimina el producto seleccionado (con confirmación)

### Carga de Imágenes

- Botón "Seleccionar" para elegir una imagen local
- Formatos soportados: JPG, JPEG, PNG, GIF, WEBP
- Vista previa de la imagen antes de guardar
- La imagen se sube automáticamente al Blob Storage al guardar

## Uso

1. Asegúrate de que la API esté ejecutándose
2. Ejecuta el proyecto `SalesWinForms`
3. El formulario de productos se abrirá automáticamente
4. Usa los botones para realizar las operaciones CRUD

## Notas Importantes

- La aplicación ignora certificados SSL en desarrollo (solo para localhost)
- Los errores se muestran en MessageBox para facilitar el debugging
- La validación se realiza antes de enviar datos a la API

