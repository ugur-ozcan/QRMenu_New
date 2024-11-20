using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
        Task<bool> DeleteAsync(string fileUrl);
        Task<string> GetSignedUrlAsync(string fileUrl, TimeSpan expiration);
    }
}
