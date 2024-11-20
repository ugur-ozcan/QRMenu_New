using AutoMapper;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;
using System.Security.Cryptography;
using System.Text;

namespace QRMenu.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> CreateAsync(UserDto model, string password)
    {
        var emailExists = await GetByEmailAsync(model.Email);
        if (emailExists.IsSuccess)
            return Result<UserDto>.Failure("Email already exists");

        var entity = _mapper.Map<User>(model);
        entity.PasswordHash = HashPassword(password);

        await _unitOfWork.Users.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<UserDto>.Success(_mapper.Map<UserDto>(entity));
    }

    public async Task<Result<UserDto>> UpdateAsync(int id, UserDto model)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        if (entity == null)
            return Result<UserDto>.Failure("User not found");

        if (model.Email != entity.Email)
        {
            var emailExists = await GetByEmailAsync(model.Email);
            if (emailExists.IsSuccess)
                return Result<UserDto>.Failure("Email already exists");
        }

        _mapper.Map(model, entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<UserDto>.Success(_mapper.Map<UserDto>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("User not found");

        await _unitOfWork.Users.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        return entity == null
            ? Result<UserDto>.Failure("User not found")
            : Result<UserDto>.Success(_mapper.Map<UserDto>(entity));
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        var specification = new UserSpecification(email);
        var entity = await _unitOfWork.Users.GetEntityWithSpec(specification);
        return entity == null
            ? Result<UserDto>.Failure("User not found")
            : Result<UserDto>.Success(_mapper.Map<UserDto>(entity));
    }

    public async Task<Result<PaginatedResult<UserDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null)
    {
        var specification = new EntityListSpecification<User>(isActive, isDeleted);
        var entities = await _unitOfWork.Users.ListAsync(specification);
        var mappedEntities = _mapper.Map<List<UserDto>>(entities);

        var paginatedResult = PaginatedResult<UserDto>.Create(
            mappedEntities,
            pageNumber,
            pageSize
        );

        return Result<PaginatedResult<UserDto>>.Success(paginatedResult);
    }

    public async Task<Result<bool>> ChangePasswordAsync(int id, string currentPassword, string newPassword)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("User not found");

        if (!VerifyPassword(currentPassword, entity.PasswordHash))
            return Result<bool>.Failure("Current password is incorrect");

        entity.PasswordHash = HashPassword(newPassword);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ResetPasswordAsync(int id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("User not found");

        var newPassword = GenerateRandomPassword();
        entity.PasswordHash = HashPassword(newPassword);
        await _unitOfWork.SaveChangesAsync();

        // TODO: Send email with new password
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ActivateAsync(int id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("User not found");

        entity.IsActive = true;
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeactivateAsync(int id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);
        if (entity == null)
            return Result<bool>.Failure("User not found");

        entity.IsActive = false;
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@#$%^&*()";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 12)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}