using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;

public class DealerService : IDealerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DealerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        _mapper.Map(model, entity);
        await _unitOfWork.SaveChangesAsync();
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