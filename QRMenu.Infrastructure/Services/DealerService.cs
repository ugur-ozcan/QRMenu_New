using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;
using QRMenu.Infrastructure.Services;

public class DealerService : IDealerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogService _logService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;


    public DealerService(IUnitOfWork unitOfWork, IMapper mapper, ILogService logService, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logService = logService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<DealerDto>> CreateAsync(DealerDto model)
    {
        var entity = _mapper.Map<Dealer>(model);
        await _unitOfWork.Dealers.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<DealerDto>.Success(_mapper.Map<DealerDto>(entity));
    }

    public async Task<Result<DealerDto>> UpdateAsync(int id, DealerDto model)
    {
        var entity = await _unitOfWork.Dealers.GetByIdAsync(id);
        if (entity == null)
            return Result<DealerDto>.Failure("Dealer not found");

        // Değişiklik öncesi durumu kaydet
        var oldValue = new
        {
            entity.Name,
            entity.ContactPerson,
            entity.Email,
            entity.PhoneNumber,
            entity.Address
        };

        // Değişiklikleri uygula
        _mapper.Map(model, entity);
        await _unitOfWork.SaveChangesAsync();

        // Değişiklik sonrası durumu kaydet
        var newValue = new
        {
            entity.Name,
            entity.ContactPerson,
            entity.Email,
            entity.PhoneNumber,
            entity.Address
        };

        // Değişiklik logunu kaydet
        await _logService.LogChangeAsync(
            module: "Dealer",
            action: "Update",
            details: $"Bayi güncellendi: {entity.Name}",
            oldValue: oldValue,
        newValue: newValue,
            userId: _currentUserService.UserId);

        return Result<DealerDto>.Success(_mapper.Map<DealerDto>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Dealers.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Dealer not found");

        await _unitOfWork.Dealers.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<DealerDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Dealers.GetByIdAsync(id);
        if (entity == null)
            return Result<DealerDto>.Failure("Dealer not found");

        return Result<DealerDto>.Success(_mapper.Map<DealerDto>(entity));
    }

    public async Task<Result<PaginatedResult<DealerDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null)
    {
        var specification = new EntityListSpecification<Dealer>(isActive, isDeleted);
        var entities = await _unitOfWork.Dealers.ListAsync(specification);
        var mappedEntities = _mapper.Map<List<DealerDto>>(entities);

        var paginatedResult = PaginatedResult<DealerDto>.Create(
            mappedEntities,
            pageNumber,
            pageSize
        );

        return Result<PaginatedResult<DealerDto>>.Success(paginatedResult);
    }

    public async Task<Result<bool>> ActivateAsync(int id)
    {
        var entity = await _unitOfWork.Dealers.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Dealer not found");

        entity.IsActive = true;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeactivateAsync(int id)
    {
        var entity = await _unitOfWork.Dealers.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Dealer not found");

        entity.IsActive = false;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExtendLicenseAsync(int id, DateTime newExpiryDate)
    {
        var entity = await _unitOfWork.Dealers.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Dealer not found");

        if (newExpiryDate <= DateTime.UtcNow)
            return Result<bool>.Failure("New expiry date must be in the future");

        entity.LicenseExpiryDate = newExpiryDate;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}