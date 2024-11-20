using AutoMapper;
using QRMenu.Application.DTOs;
using QRMenu.Core.Entities;

namespace QRMenu.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Company
        CreateMap<Company, CompanyDto>();
        CreateMap<CompanyDto, Company>();

        // Branch
        CreateMap<Branch, BranchDto>();
        CreateMap<BranchDto, Branch>();

        // User
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        // Dealer
        CreateMap<Dealer, DealerDto>();
        CreateMap<DealerDto, Dealer>();

        // Theme
        CreateMap<Theme, ThemeDto>();
        CreateMap<ThemeDto, Theme>();

        // Template
        CreateMap<Template, TemplateDto>();
        CreateMap<TemplateDto, Template>();

        // CompanyTheme
        CreateMap<CompanyTheme, CompanyThemeDto>();
        CreateMap<CompanyThemeDto, CompanyTheme>();
    }
}