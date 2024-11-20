using QRMenu.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Net.Codecrete.QrCodeGenerator;

namespace QRMenu.Infrastructure.Services;

public class QRCodeService : IQRCodeService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<QRCodeService> _logger;
    private readonly string _baseUrl;

    public QRCodeService(
        IFileStorageService fileStorageService,
        ILogger<QRCodeService> logger,
        IConfiguration configuration)
    {
        _fileStorageService = fileStorageService;
        _logger = logger;
        _baseUrl = configuration["Application:BaseUrl"] ?? "https://localhost";
    }

    public async Task<byte[]> GenerateQrCodeAsync(string companySlug, string branchSlug = null, int? tableNumber = null)
    {
        try
        {
            var url = GetQrCodeUrl(companySlug, branchSlug, tableNumber);
            var qr = QrCode.EncodeText(url, QrCode.Ecc.Medium);
            return qr.ToBmpBitmap(20); // 20 pixel module size
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating QR code for company: {CompanySlug}, branch: {BranchSlug}",
                companySlug, branchSlug);
            throw;
        }
    }

    public async Task<string> GenerateAndSaveQrCodeAsync(string companySlug, string branchSlug = null, int? tableNumber = null)
    {
        try
        {
            var qrCodeBytes = await GenerateQrCodeAsync(companySlug, branchSlug, tableNumber);
            var fileName = $"qr-{companySlug}-{branchSlug ?? "main"}-{tableNumber ?? 0}.png";

            using var stream = new MemoryStream(qrCodeBytes);
            return await _fileStorageService.UploadAsync(stream, fileName, "image/png");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving QR code for company: {CompanySlug}, branch: {BranchSlug}",
                companySlug, branchSlug);
            throw;
        }
    }

    public string GetQrCodeUrl(string companySlug, string branchSlug = null, int? tableNumber = null)
    {
        var url = $"{_baseUrl}/{companySlug}";

        if (!string.IsNullOrEmpty(branchSlug))
        {
            url += $"/{branchSlug}";
        }

        if (tableNumber.HasValue)
        {
            url += $"?table={tableNumber}";
        }

        return url;
    }
}