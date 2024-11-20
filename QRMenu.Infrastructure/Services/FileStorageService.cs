using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QRMenu.Application.Interfaces;

namespace QRMenu.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _uploadPath;
    private readonly ILogger<FileStorageService> _logger;

    public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
    {
        _uploadPath = configuration["Storage:UploadPath"] ?? "uploads";
        _logger = logger;

        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }


    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        try
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(_uploadPath, uniqueFileName);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fs);
            }

            return GetFileUrl(uniqueFileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file: {FileName}", fileName);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string fileUrl)
    {
        try
        {
            var fileName = Path.GetFileName(fileUrl);
            var filePath = Path.Combine(_uploadPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file: {FileUrl}", fileUrl);
            return false;
        }
    }



    public async Task<string> GetSignedUrlAsync(string fileUrl, TimeSpan expiration)
    {
        try
        {
            var fileName = Path.GetFileName(fileUrl);
            var filePath = Path.Combine(_uploadPath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", fileName);
            }

            // Bu basit implementasyonda sadece dosya URL'ini döndürüyoruz
            // Gerçek bir imzalı URL implementasyonu için token-based bir sistem eklenebilir
            return GetFileUrl(fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting signed URL: {FileUrl}", fileUrl);
            throw;
        }
    }

    private string GetFileUrl(string fileName)
    {
        return $"/uploads/{fileName}";
    }
}