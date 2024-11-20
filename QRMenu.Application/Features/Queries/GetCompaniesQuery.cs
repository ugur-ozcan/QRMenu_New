using MediatR;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
 
namespace QRMenu.Application.Companies.Queries;

public record GetCompaniesQuery : IRequest<Result<PaginatedResult<CompanyDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public bool? IsActive { get; init; }
    public bool? IsDeleted { get; init; }
    public int? DealerId { get; init; }
}