using AutoMapper;
using MediatR;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
 using QRMenu.Application.Interfaces;
 using QRMenu.Core.Interfaces;
using QRMenu.Core.Security;
using QRMenu.Core.ValueObjects;

namespace QRMenu.Application.Companies.Commands;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<CompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Result<CompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.HasPermission(Permission.CompanyCreate))
            return Result<CompanyDto>.Failure("Insufficient permissions");

        var entity = new Company
        {
            Name = request.Name,
            Slug = request.Slug,
            DealerId = request.DealerId,
            Logo = request.Logo,
            PhoneNumber = request.PhoneNumber,
            CategoryView = request.CategoryView,
            ProductView = request.ProductView,
            UseBranches = request.UseBranches,
            ShowTableNumbers = request.ShowTableNumbers,
            LanguagesSupported = string.Join(",", request.LanguagesSupported),
            DefaultLanguage = request.DefaultLanguage,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var location = Location.Create(
            request.Location.Latitude,
            request.Location.Longitude,
            request.Location.FormattedAddress,
            request.Location.City,
            request.Location.District,
            request.Location.Country,
            request.Location.PostalCode,
            request.Location.BuildingNumber,
            request.Location.Street
        );
        entity.SetLocation(location);

        foreach (var hours in request.BusinessHours)
        {
            var businessHours = BusinessHours.Create(
                hours.DayOfWeek,
                hours.OpenTime,
                hours.CloseTime
            );
            entity.AddBusinessHours(businessHours);
        }

        await _companyRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<CompanyDto>(entity);
        return Result<CompanyDto>.Success(dto, "Company created successfully");
    }
}