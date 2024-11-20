using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Template;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;

public class TemplateService : ITemplateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TemplateService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TemplateDto>> CreateAsync(TemplateDto model)
    {
        var entity = _mapper.Map<Template>(model);
        await _unitOfWork.Templates.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<TemplateDto>.Success(_mapper.Map<TemplateDto>(entity));
    }

    public async Task<Result<TemplateDto>> UpdateAsync(int id, TemplateDto model)
    {
        var entity = await _unitOfWork.Templates.GetByIdAsync(id);
        if (entity == null)
            return Result<TemplateDto>.Failure("Template not found");

        _mapper.Map(model, entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<TemplateDto>.Success(_mapper.Map<TemplateDto>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Templates.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Template not found");

        await _unitOfWork.Templates.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<TemplateDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Templates.GetByIdAsync(id);
        if (entity == null)
            return Result<TemplateDto>.Failure("Template not found");

        return Result<TemplateDto>.Success(_mapper.Map<TemplateDto>(entity));
    }

    public async Task<Result<List<TemplateDto>>> GetAllActiveAsync()
    {
        var entities = await _unitOfWork.Templates.GetActiveAsync();
        return Result<List<TemplateDto>>.Success(_mapper.Map<List<TemplateDto>>(entities));
    }

    public async Task<Result<bool>> ActivateAsync(int id)
    {
        var entity = await _unitOfWork.Templates.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Template not found");

        entity.IsActive = true;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeactivateAsync(int id)
    {
        var entity = await _unitOfWork.Templates.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("Template not found");

        entity.IsActive = false;
        await _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}