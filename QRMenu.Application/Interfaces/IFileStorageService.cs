 
namespace QRMenu.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
        Task<bool> DeleteAsync(string fileUrl);
        Task<string> GetSignedUrlAsync(string fileUrl, TimeSpan expiration);
    }
}
