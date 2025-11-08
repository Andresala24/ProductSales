using Microsoft.AspNetCore.Http;

namespace Domain.Entities.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(IFormFile file, string fileName);
    Task<string> UploadImageAsync(IFormFile file, string fileName, string? existingImageUrl);
    Task<bool> DeleteImageAsync(string blobUrl);
}

