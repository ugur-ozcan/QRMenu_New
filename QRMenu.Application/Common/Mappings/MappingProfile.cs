using AutoMapper;
using QRMenu.Application.DTOs;
using QRMenu.Application.DTOs.Company;
using QRMenu.Core.Entities;

namespace QRMenu.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => s.LanguagesSupported.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

        CreateMap<CompanyDto, Company>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => string.Join(",", s.LanguagesSupported)));

        CreateMap<Branch, BranchDto>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => s.LanguagesSupported.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

        CreateMap<BranchDto, Branch>()
            .ForMember(d => d.LanguagesSupported, opt =>
                opt.MapFrom(s => string.Join(",", s.LanguagesSupported)));

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<Dealer, DealerDto>();
        CreateMap<DealerDto, Dealer>();

        CreateMap<Theme, ThemeDto>();
        CreateMap<ThemeDto, Theme>();

        CreateMap<Template, TemplateDto>();
        CreateMap<TemplateDto, Template>();

        CreateMap<CompanyTheme, CompanyThemeDto>();
        CreateMap<CompanyThemeDto, CompanyTheme>();
    }
}