using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Dealer;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class DealerService : IDealerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        Task<Result<bool>> IDealerService.ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<DealerDto>> IDealerService.CreateAsync(DealerDto model)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> IDealerService.DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> IDealerService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> IDealerService.ExtendLicenseAsync(int id, DateTime newExpiryDate)
        {
            throw new NotImplementedException();
        }

        Task<Result<PaginatedResult<DealerDto>>> IDealerService.GetAllAsync(int pageNumber, int pageSize, bool? isActive, bool? isDeleted)
        {
            throw new NotImplementedException();
        }

        Task<Result<DealerDto>> IDealerService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Result<DealerDto>> IDealerService.UpdateAsync(int id, DealerDto model)
        {
            throw new NotImplementedException();
        }

        // IDealerService implementasyonu
    }
}
