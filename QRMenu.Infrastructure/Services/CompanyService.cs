using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CompanyDto>> CreateAsync(CompanyDto model)
        {
            var entity = _mapper.Map<Company>(model);
            await _unitOfWork.Companies.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result<CompanyDto>.Success(_mapper.Map<CompanyDto>(entity));
        }

        public async Task<Result<CompanyDto>> UpdateAsync(int id, CompanyDto model)
        {
            var entity = await _unitOfWork.Companies.GetByIdAsync(id);
            if (entity == null)
                return Result<CompanyDto>.Failure("Company not found");

            _mapper.Map(model, entity);
            await _unitOfWork.SaveChangesAsync();
            return Result<CompanyDto>.Success(_mapper.Map<CompanyDto>(entity));
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Companies.GetByIdAsync(id);
            if (entity == null)
                return Result<bool>.Failure("Company not found");

            await _unitOfWork.Companies.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<CompanyDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Companies.GetByIdAsync(id);
            if (entity == null)
                return Result<CompanyDto>.Failure("Company not found");

            return Result<CompanyDto>.Success(_mapper.Map<CompanyDto>(entity));
        }

        public async Task<Result<PaginatedResult<CompanyDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null)
        {
            var specification = new EntityListSpecification<Company>(isActive, isDeleted);
            var entities = await _unitOfWork.Companies.ListAsync(specification);
            var dtos = _mapper.Map<List<CompanyDto>>(entities);

            var paginatedResult = PaginatedResult<CompanyDto>.Create(
                dtos,
                pageNumber,
                pageSize
            );

            return Result<PaginatedResult<CompanyDto>>.Success(paginatedResult);
        }

        public async Task<Result<List<CompanyDto>>> GetByDealerIdAsync(int dealerId)
        {
            var entities = await _repository.GetByDealerIdAsync(dealerId);
            return Result<List<CompanyDto>>.Success(_mapper.Map<List<CompanyDto>>(entities));
        }

        public async Task<Result<CompanyDto>> GetBySlugAsync(string slug)
        {
            var entity = await _repository.GetBySlugAsync(slug);
            if (entity == null)
                return Result<CompanyDto>.Failure("Company not found");

            return Result<CompanyDto>.Success(_mapper.Map<CompanyDto>(entity));
        }

        public async Task<Result<bool>> ActivateAsync(int id)
        {
            var entity = await _unitOfWork.Companies.GetByIdAsync(id);
            if (entity == null)
                return Result<bool>.Failure("Company not found");

            entity.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeactivateAsync(int id)
        {
            var entity = await _unitOfWork.Companies.GetByIdAsync(id);
            if (entity == null)
                return Result<bool>.Failure("Company not found");

            entity.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }

}
