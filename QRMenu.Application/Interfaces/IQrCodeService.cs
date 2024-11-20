 
namespace QRMenu.Application.Interfaces
{
    public interface IQRCodeService
    {
        Task<byte[]> GenerateQrCodeAsync(string companySlug, string branchSlug = null, int? tableNumber = null);
        Task<string> GenerateAndSaveQrCodeAsync(string companySlug, string branchSlug = null, int? tableNumber = null);
    }
}
