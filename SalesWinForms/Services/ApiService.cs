using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net;
using SalesWinForms.Models;

namespace SalesWinForms.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private string? _token;

    public ApiService(string? baseUrl = null)
    {
        // Base URL sin /api porque las rutas ya lo incluyen
        _baseUrl = baseUrl ?? "https://localhost:7263";
        
        var handler = new HttpClientHandler
        {
            // Ignorar certificados SSL en desarrollo (solo para desarrollo)
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(_baseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };
        
        // Agregar headers comunes
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public void SetToken(string token)
    {
        _token = token;
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    public void ClearToken()
    {
        _token = null;
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
    }

    // ========== PRODUCTOS ==========

    public async Task<List<InventoryProduct>> GetProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/InventoryProduct/GetList");
            
            // Verificar el código de estado antes de EnsureSuccessStatusCode
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)response.StatusCode} {response.StatusCode}): {errorContent}");
            }
            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<InventoryProduct>>();
            return result ?? new List<InventoryProduct>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error de conexión HTTP: {ex.Message}\n\n" +
                              $"Verifica que la API esté ejecutándose en {_baseUrl}", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception($"Timeout al conectar con la API.\n\n" +
                              $"Verifica que la API esté ejecutándose en {_baseUrl}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener productos: {ex.Message}", ex);
        }
    }

    public async Task<InventoryProduct?> GetProductByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/InventoryProduct/GetById?id={id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InventoryProduct>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener producto: {ex.Message}", ex);
        }
    }

    public async Task<InventoryProduct> CreateProductAsync(CreateInventoryProductDto product, string? imagePath)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(product.Name), "Name");
            
            if (product.Price.HasValue)
                content.Add(new StringContent(product.Price.Value.ToString()), "Price");
            
            if (product.Stock.HasValue)
                content.Add(new StringContent(product.Stock.Value.ToString()), "Stock");

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(imageContent, "imageFile", Path.GetFileName(imagePath));
            }

            var response = await _httpClient.PostAsync("/api/InventoryProduct/Add", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InventoryProduct>() 
                ?? throw new Exception("No se recibió respuesta del servidor");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear producto: {ex.Message}", ex);
        }
    }

    public async Task<InventoryProduct> UpdateProductAsync(int id, UpdateInventoryProductDto product, string? imagePath)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            
            if (!string.IsNullOrEmpty(product.Name))
                content.Add(new StringContent(product.Name), "Name");
            
            if (product.Price.HasValue)
                content.Add(new StringContent(product.Price.Value.ToString()), "Price");
            
            if (product.Stock.HasValue)
                content.Add(new StringContent(product.Stock.Value.ToString()), "Stock");

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                var imageContent = new ByteArrayContent(imageBytes);
                var contentType = GetContentType(imagePath);
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                content.Add(imageContent, "imageFile", Path.GetFileName(imagePath));
            }

            var response = await _httpClient.PutAsync($"/api/InventoryProduct/Update?id={id}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InventoryProduct>() 
                ?? throw new Exception("No se recibió respuesta del servidor");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar producto: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/InventoryProduct/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar producto: {ex.Message}", ex);
        }
    }

    private string GetContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => "image/jpeg"
        };
    }

    // ========== VENTAS ==========

    public async Task<List<Sale>> GetSalesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/Sale/GetList");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)response.StatusCode} {response.StatusCode}): {errorContent}");
            }
            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<Sale>>();
            return result ?? new List<Sale>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error de conexión HTTP: {ex.Message}\n\n" +
                              $"Verifica que la API esté ejecutándose en {_baseUrl}", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception($"Timeout al conectar con la API.\n\n" +
                              $"Verifica que la API esté ejecutándose en {_baseUrl}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener ventas: {ex.Message}", ex);
        }
    }

    public async Task<Sale?> GetSaleByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/Sale/GetById?id={id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Sale>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener venta: {ex.Message}", ex);
        }
    }

    public async Task<Sale> CreateSaleAsync(CreateSaleDto saleDto)
    {
        try
        {
            var jsonContent = JsonContent.Create(saleDto);
            var response = await _httpClient.PostAsync("/api/Sale/Add", jsonContent);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)response.StatusCode} {response.StatusCode}): {errorContent}");
            }
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Sale>() 
                ?? throw new Exception("No se recibió respuesta del servidor");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear venta: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteSaleAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/Sale/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar venta: {ex.Message}", ex);
        }
    }

    public async Task<List<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var startDateStr = startDate.ToString("yyyy-MM-dd");
            var endDateStr = endDate.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetAsync($"/api/Sale/GetByDateRange?startDate={startDateStr}&endDate={endDateStr}");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)response.StatusCode} {response.StatusCode}): {errorContent}");
            }
            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<Sale>>();
            return result ?? new List<Sale>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error de conexión HTTP: {ex.Message}\n\n" +
                              $"Verifica que la API esté ejecutándose en {_baseUrl}", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception($"Timeout al conectar con la API.\n\n" +
                              $"Verifica que la API esté ejecutándose en {_baseUrl}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener ventas por rango de fechas: {ex.Message}", ex);
        }
    }

    // ========== AUTENTICACIÓN ==========

    public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var jsonContent = JsonContent.Create(loginDto);
            var response = await _httpClient.PostAsync("/api/Auth/Login", jsonContent);
            
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return null;
                    
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)response.StatusCode} {response.StatusCode}): {errorContent}");
            }
            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            
            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                SetToken(result.Token);
            }
            
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al iniciar sesión: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}

