using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;

public class ThemeService : IThemeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ThemeDto>> CreateAsync(ThemeDto model)
    {
        var entity = _mapper.Map<Theme>(model);
        await _unitOfWork.Themes.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<ThemeDto>.Success(_mapper.Map<ThemeDto>(entity));
    }

    public async Task<Result<ThemeDto>> UpdateAsync(int id, ThemeDto model)
    {
        var entity = await _unitOfWork.Themes.GetByIdAsync(id);
        if (entity == null)
            return Result<ThemeDto>.Failure("Theme not found");

        _mapper.Map(model, entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<ThemeDto>.Success(_mapper.Map<ThemeDto>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Themes.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Theme not found");

        await _unitOfWork.Themes.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<ThemeDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Themes.GetByIdAsync(id);
        if (entity == null)
            return Result<ThemeDto>.Failure("Theme not found");

        return Result<ThemeDto>.Success(_mapper.Map<ThemeDto>(entity));
    }

    public async Task<Result<List<ThemeDto>>> GetAllActiveAsync()
    {
        var entities = await _unitOfWork.Themes.GetActiveAsync();
        return Result<List<ThemeDto>>.Success(_mapper.Map<List<ThemeDto>>(entities));
    }

    public async Task<Result<bool>> ActivateAsync(int id)
    {
        var entity = await _unitOfWork.Themes.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Theme not found");

        entity.IsActive = true;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeactivateAsync(int id)
    {
        var entity = await _unitOfWork.Themes.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Theme not found");

        entity.IsActive = false;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}