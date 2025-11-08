using Azure.Storage.Blobs;
using System.Security.Cryptography;
using Domain.Entities.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly string _blobSasUrl;

    public BlobStorageService(IConfiguration configuration)
    {
        _blobSasUrl = configuration["Storage:BlobSasUrl"] ?? throw new InvalidOperationException("BlobSasUrl no configurado");
    }

    private async Task<string> CalculateFileHashAsync(Stream stream)
    {
        using var sha256 = SHA256.Create();
        stream.Position = 0;
        var hashBytes = await sha256.ComputeHashAsync(stream);
        stream.Position = 0;
        return Convert.ToBase64String(hashBytes);
    }

    public async Task<string> UploadImageAsync(IFormFile file, string fileName)
    {
        return await UploadImageAsync(file, fileName, null);
    }

    public async Task<string> UploadImageAsync(IFormFile file, string fileName, string? existingImageUrl)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("El archivo está vacío o es nulo");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
            throw new ArgumentException($"Tipo de archivo no permitido. Solo se permiten: {string.Join(", ", allowedExtensions)}");

        const long maxFileSize = 5 * 1024 * 1024;
        if (file.Length > maxFileSize)
            throw new ArgumentException("El archivo excede el tamaño máximo permitido de 5MB");

        try
        {
            if (!string.IsNullOrWhiteSpace(existingImageUrl))
            {
                var isSameFile = await CompareFileHashAsync(file, existingImageUrl);
                if (isSameFile)
                {
                    return existingImageUrl;
                }
            }

            var containerClient = new BlobContainerClient(new Uri(_blobSasUrl));
            var uniqueFileName = fileName;
            var blobPath = $"aalarcon/{uniqueFileName}";
            var blobClient = containerClient.GetBlobClient(blobPath);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al subir la imagen al Blob Storage: {ex.Message}", ex);
        }
    }

    public async Task<bool> CompareFileHashAsync(IFormFile file, string blobUrl)
    {
        try
        {
            using var newFileStream = file.OpenReadStream();
            var newFileHash = await CalculateFileHashAsync(newFileStream);

            var containerClient = new BlobContainerClient(new Uri(_blobSasUrl));
            var uri = new Uri(blobUrl);
            var segments = uri.Segments.Skip(1).ToArray();
            var blobPath = string.Join("", segments);
            var blobClient = containerClient.GetBlobClient(blobPath);

            if (!await blobClient.ExistsAsync())
                return false;

            using var blobStream = await blobClient.OpenReadAsync();
            var existingFileHash = await CalculateFileHashAsync(blobStream);

            return newFileHash == existingFileHash;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteImageAsync(string blobUrl)
    {
        if (string.IsNullOrWhiteSpace(blobUrl))
            return false;

        try
        {
            var containerClient = new BlobContainerClient(new Uri(_blobSasUrl));
            var uri = new Uri(blobUrl);
            var segments = uri.Segments;
            
            if (segments.Length < 2)
            {
                System.Diagnostics.Debug.WriteLine($"URL de blob inválida: {blobUrl}");
                return false;
            }

            var blobPath = string.Join("", segments.Skip(2));
            blobPath = blobPath.Trim('/');

            if (string.IsNullOrWhiteSpace(blobPath))
            {
                System.Diagnostics.Debug.WriteLine($"No se pudo extraer la ruta del blob de la URL: {blobUrl}");
                return false;
            }

            System.Diagnostics.Debug.WriteLine($"Intentando eliminar blob: {blobPath} de URL: {blobUrl}");

            var blobClient = containerClient.GetBlobClient(blobPath);
            var deleted = await blobClient.DeleteIfExistsAsync();
            return deleted.Value;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al eliminar imagen del blob storage: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"URL: {blobUrl}");
            throw new Exception($"Error al eliminar la imagen del Blob Storage: {ex.Message}", ex);
        }
    }
}

