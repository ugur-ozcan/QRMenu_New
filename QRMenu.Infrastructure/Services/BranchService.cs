using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Branch;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;

namespace QRMenu.Infrastructure.Services;

public class BranchService : IBranchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<BranchDto>> CreateAsync(BranchDto model)
    {
        var entity = _mapper.Map<Branch>(model);
        await _unitOfWork.Branches.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(entity));
    }

    public async Task<Result<BranchDto>> UpdateAsync(int id, BranchDto model)
    {
        var entity = await _unitOfWork.Branches.GetByIdAsync(id);
        if (entity == null)
            return Result<BranchDto>.Failure("Branch not found");

        _mapper.Map(model, entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Branches.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Branch not found");

        await _unitOfWork.Branches.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<BranchDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Branches.GetByIdAsync(id);
        if (entity == null)
            return Result<BranchDto>.Failure("Branch not found");

        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(entity));
    }

    public async Task<Result<PaginatedResult<BranchDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null)
    {
        var specification = new EntityListSpecification<Branch>(isActive, isDeleted);
        var entities = await _unitOfWork.Branches.ListAsync(specification);
        var mappedEntities = _mapper.Map<List<BranchDto>>(entities);

        var paginatedResult = PaginatedResult<BranchDto>.Create(
            mappedEntities,
            pageNumber,
            pageSize
        );

        return Result<PaginatedResult<BranchDto>>.Success(paginatedResult);
    }

    public async Task<Result<List<BranchDto>>> GetByCompanyIdAsync(int companyId)
    {
        var specification = new BranchSpecification(companyId);
        var entities = await _unitOfWork.Branches.ListAsync(specification);
        return Result<List<BranchDto>>.Success(_mapper.Map<List<BranchDto>>(entities));
    }

    public async Task<Result<BranchDto>> GetBySlugAsync(string companySlug, string branchSlug)
    {
        var specification = new BranchSpecification(companySlug, branchSlug);
        var entity = await _unitOfWork.Branches.GetEntityWithSpec(specification);
        if (entity == null)
            return Result<BranchDto>.Failure("Branch not found");

        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(entity));
    }

    public async Task<Result<bool>> ActivateAsync(int id)
    {
        var entity = await _unitOfWork.Branches.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Branch not found");

        entity.IsActive = true;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeactivateAsync(int id)
    {
        var entity = await _unitOfWork.Branches.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Branch not found");

        entity.IsActive = false;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UpdateSyncScheduleAsync(int id, string syncInterval)
    {
        var entity = await _unitOfWork.Branches.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Branch not found");

        entity.SyncInterval = syncInterval;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}