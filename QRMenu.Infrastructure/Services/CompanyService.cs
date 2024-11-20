using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Company;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
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

        Task<Result<bool>> ICompanyService.ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<CompanyDto>> ICompanyService.CreateAsync(CompanyDto model)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> ICompanyService.DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> ICompanyService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<PaginatedResult<CompanyDto>>> ICompanyService.GetAllAsync(int pageNumber, int pageSize, bool? isActive, bool? isDeleted)
        {
            throw new NotImplementedException();
        }

        Task<Result<List<CompanyDto>>> ICompanyService.GetByDealerIdAsync(int dealerId)
        {
            throw new NotImplementedException();
        }

        Task<Result<CompanyDto>> ICompanyService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<CompanyDto>> ICompanyService.GetBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        Task<Result<CompanyDto>> ICompanyService.UpdateAsync(int id, CompanyDto model)
        {
            throw new NotImplementedException();
        }

        // ICompanyService implementasyonu
    }

}
