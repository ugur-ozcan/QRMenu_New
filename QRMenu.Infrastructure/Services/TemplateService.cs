using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Template;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
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
            try
            {
                var template = _mapper.Map<Template>(model);
                await _unitOfWork.TemplateRepository.AddAsync(template);
                await _unitOfWork.SaveChangesAsync();

                var resultDto = _mapper.Map<TemplateDto>(template);
                return Result<TemplateDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<TemplateDto>.Failure($"Error creating template: {ex.Message}");
            }
        }

        public async Task<Result<TemplateDto>> UpdateAsync(int id, TemplateDto model)
        {
            try
            {
                var template = await _unitOfWork.TemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    return Result<TemplateDto>.Failure("Template not found.");
                }

                _mapper.Map(model, template);
                _unitOfWork.TemplateRepository.Update(template);
                await _unitOfWork.SaveChangesAsync();

                var resultDto = _mapper.Map<TemplateDto>(template);
                return Result<TemplateDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<TemplateDto>.Failure($"Error updating template: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var template = await _unitOfWork.TemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    return Result<bool>.Failure("Template not found.");
                }

                _unitOfWork.TemplateRepository.Delete(template);
                await _unitOfWork.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting template: {ex.Message}");
            }
        }

        public async Task<Result<TemplateDto>> GetByIdAsync(int id)
        {
            try
            {
                var template = await _unitOfWork.TemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    return Result<TemplateDto>.Failure("Template not found.");
                }

                var resultDto = _mapper.Map<TemplateDto>(template);
                return Result<TemplateDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<TemplateDto>.Failure($"Error retrieving template: {ex.Message}");
            }
        }

        public async Task<Result<List<TemplateDto>>> GetAllActiveAsync()
        {
            try
            {
                var templates = await _unitOfWork.TemplateRepository.GetAllAsync(t => t.IsActive);
                var resultDtoList = _mapper.Map<List<TemplateDto>>(templates);
                return Result<List<TemplateDto>>.Success(resultDtoList);
            }
            catch (Exception ex)
            {
                return Result<List<TemplateDto>>.Failure($"Error retrieving templates: {ex.Message}");
            }
        }

        public async Task<Result<bool>> ActivateAsync(int id)
        {
            try
            {
                var template = await _unitOfWork.TemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    return Result<bool>.Failure("Template not found.");
                }

                template.IsActive = true;
                _unitOfWork.TemplateRepository.Update(template);
                await _unitOfWork.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error activating template: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeactivateAsync(int id)
        {
            try
            {
                var template = await _unitOfWork.TemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    return Result<bool>.Failure("Template not found.");
                }

                template.IsActive = false;
                _unitOfWork.TemplateRepository.Update(template);
                await _unitOfWork.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deactivating template: {ex.Message}");
            }
        }
    }
}
