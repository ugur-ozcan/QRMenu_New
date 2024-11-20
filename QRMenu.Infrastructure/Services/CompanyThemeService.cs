using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using Microsoft.Extensions.Logging;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Specifications;

namespace QRMenu.Infrastructure.Services;

public class CompanyThemeService : ICompanyThemeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CompanyThemeService> _logger;

    public CompanyThemeService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CompanyThemeService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<CompanyThemeDto>> CreateAsync(CompanyThemeDto model)
    {
        try
        {
            var entity = _mapper.Map<CompanyTheme>(model);
            entity.AppliedAt = DateTime.UtcNow;

            await _unitOfWork.CompanyThemes.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Result<CompanyThemeDto>.Success(_mapper.Map<CompanyThemeDto>(entity));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating company theme");
            return Result<CompanyThemeDto>.Failure("Error creating company theme");
        }
    }

    public async Task<Result<CompanyThemeDto>> UpdateAsync(int companyId, CompanyThemeDto model)
    {
        try
        {
            var entity = await GetCompanyThemeByCompanyId(companyId);
            if (entity == null)
                return Result<CompanyThemeDto>.Failure("Company theme not found");

            _mapper.Map(model, entity);
            entity.AppliedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return Result<CompanyThemeDto>.Success(_mapper.Map<CompanyThemeDto>(entity));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating company theme for company {CompanyId}", companyId);
            return Result<CompanyThemeDto>.Failure("Error updating company theme");
        }
    }

    public async Task<Result<CompanyThemeDto>> GetByCompanyIdAsync(int companyId)
    {
        try
        {
            var entity = await GetCompanyThemeByCompanyId(companyId);
            if (entity == null)
                return Result<CompanyThemeDto>.Failure("Company theme not found");

            return Result<CompanyThemeDto>.Success(_mapper.Map<CompanyThemeDto>(entity));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting company theme for company {CompanyId}", companyId);
            return Result<CompanyThemeDto>.Failure("Error getting company theme");
        }
    }

    private async Task<CompanyTheme> GetCompanyThemeByCompanyId(int companyId)
    {
        var specification = new CompanyThemeSpecification(companyId);
        var entities = await _unitOfWork.CompanyThemes.ListAsync(specification);
        return entities.FirstOrDefault();
    }
}