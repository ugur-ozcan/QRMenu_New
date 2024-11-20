using AutoMapper;
using MediatR;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Company;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Security;
using QRMenu.Core.Specifications;
 
namespace QRMenu.Application.Companies.Queries
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, Result<PaginatedResult<CompanyDto>>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetCompaniesQueryHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<PaginatedResult<CompanyDto>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.HasPermission(Permission.CompanyRead))
                return Result<PaginatedResult<CompanyDto>>.Failure("Insufficient permissions");

            // Kullanıcının rolüne göre filtreleme
            var dealerId = _currentUserService.Role switch
            {
                UserRole.Admin => request.DealerId,
                UserRole.DealerAdmin => _currentUserService.DealerId,
                UserRole.CompanyAdmin => null,
                _ => null
            };

            if (dealerId == null && _currentUserService.Role != UserRole.Admin)
                return Result<PaginatedResult<CompanyDto>>.Failure("Invalid dealer id");

            var specification = new CompanySpecification(
                dealerId.Value,
                request.IsActive,
                request.IsDeleted);

            var companies = await _companyRepository.ListAsync(specification);

            var mappedCompanies = _mapper.Map<List<CompanyDto>>(companies);
            var paginatedResult = PaginatedResult<CompanyDto>.Create(
                mappedCompanies,
                request.PageNumber,
                request.PageSize);

            return Result<PaginatedResult<CompanyDto>>.Success(paginatedResult);
        }
    }
}
