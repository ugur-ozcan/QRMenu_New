using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.User;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Result<bool>> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDto>> CreateAsync(UserDto model, string password)
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

        public Task<Result<PaginatedResult<UserDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDto>> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ResetPasswordAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDto>> UpdateAsync(int id, UserDto model)
        {
            throw new NotImplementedException();
        }

        // IUserService implementasyonu
    }
}
