using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Theme;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Result<bool>> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ThemeDto>> CreateAsync(ThemeDto model)
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

        public Task<Result<List<ThemeDto>>> GetAllActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<ThemeDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ThemeDto>> UpdateAsync(int id, ThemeDto model)
        {
            throw new NotImplementedException();
        }

        // IThemeService implementasyonu
    }
}
