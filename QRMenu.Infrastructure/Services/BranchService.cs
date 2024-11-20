using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Branch;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Result<bool>> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BranchDto>> CreateAsync(BranchDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PaginatedResult<BranchDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<BranchDto>>> GetByCompanyIdAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BranchDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BranchDto>> GetBySlugAsync(string companySlug, string branchSlug)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BranchDto>> UpdateAsync(int id, BranchDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> UpdateSyncScheduleAsync(int id, string syncInterval)
        {
            throw new NotImplementedException();
        }

        // IBranchService implementasyonu
    }
}
