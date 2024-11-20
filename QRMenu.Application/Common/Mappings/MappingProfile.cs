using AutoMapper;
using QRMenu.Application.DTOs;
 
using QRMenu.Core.Entities;
using QRMenu.Core.ValueObjects;

namespace QRMenu.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Company Mappings
        CreateMap<Company, CompanyDto>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => s.LanguagesSupported?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>()))
            .ForMember(d => d.Branches, opt => opt.ExplicitExpansion())
            .ForMember(d => d.BusinessHours, opt => opt.ExplicitExpansion());

        CreateMap<CompanyDto, Company>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => string.Join(",", s.LanguagesSupported ?? new List<string>())));

        // Branch Mappings
        CreateMap<Branch, BranchDto>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => s.LanguagesSupported?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>()))
            .ForMember(d => d.BusinessHours, opt => opt.ExplicitExpansion());

        CreateMap<BranchDto, Branch>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => string.Join(",", s.LanguagesSupported ?? new List<string>())));

        // User Mappings
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        // Dealer Mappings
        CreateMap<Dealer, DealerDto>()
            .ForMember(d => d.Companies, opt => opt.ExplicitExpansion());
        CreateMap<DealerDto, Dealer>();

        // Theme Mappings
        CreateMap<Theme, ThemeDto>()
            .ForMember(d => d.CompanyThemes, opt => opt.ExplicitExpansion());
        CreateMap<ThemeDto, Theme>();

        // Template Mappings
        CreateMap<Template, TemplateDto>()
            .ForMember(d => d.CompanyThemes, opt => opt.ExplicitExpansion());
        CreateMap<TemplateDto, Template>();

        // CompanyTheme Mappings
        CreateMap<CompanyTheme, CompanyThemeDto>()
            .ForMember(d => d.Theme, opt => opt.ExplicitExpansion())
            .ForMember(d => d.Template, opt => opt.ExplicitExpansion());
        CreateMap<CompanyThemeDto, CompanyTheme>();

        // Value Objects
        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<BusinessHours, BusinessHoursDto>().ReverseMap();
    }
}