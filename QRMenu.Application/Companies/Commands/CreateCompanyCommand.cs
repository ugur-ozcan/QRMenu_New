using MediatR;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.DTOs.Company;

namespace QRMenu.Application.Companies.Commands;

public record CreateCompanyCommand : IRequest<Result<CompanyDto>>
{
    public string Name { get; init; }
    public string Slug { get; init; }
    public int DealerId { get; init; }
    public string Logo { get; init; }
    public string PhoneNumber { get; init; }
    public LocationDto Location { get; init; }
    public string CategoryView { get; init; }
    public string ProductView { get; init; }
    public bool UseBranches { get; init; }
    public bool ShowTableNumbers { get; init; }
    public List<string> LanguagesSupported { get; init; }
    public string DefaultLanguage { get; init; }
    public List<BusinessHoursDto> BusinessHours { get; init; }
}