using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Services
{
    public class CompanyThemeService : ICompanyThemeService
    {
        private readonly IBaseRepository<CompanyTheme> _companyThemeRepository;
        private readonly IBaseRepository<Theme> _themeRepository;
        private readonly IBaseRepository<Template> _templateRepository;

        public CompanyThemeService(
            IBaseRepository<CompanyTheme> companyThemeRepository,
            IBaseRepository<Theme> themeRepository,
            IBaseRepository<Template> templateRepository)
        {
            _companyThemeRepository = companyThemeRepository;
            _themeRepository = themeRepository;
            _templateRepository = templateRepository;
        }

        public async Task<Result<CompanyThemeDto>> CreateAsync(CompanyThemeDto model)
        {
            var theme = await _themeRepository.GetByIdAsync(model.ThemeId);
            var template = await _templateRepository.GetByIdAsync(model.TemplateId);

            if (theme == null || template == null)
            {
                return Result<CompanyThemeDto>.Failure("Theme or Template not found");
            }

            var companyTheme = new CompanyTheme
            {
                CompanyId = model.CompanyId,
                ThemeId = model.ThemeId,
                TemplateId = model.TemplateId,
                AppliedAt = DateTime.UtcNow
            };

            await _companyThemeRepository.AddAsync(companyTheme);

            model.Id = companyTheme.Id;
            model.AppliedAt = companyTheme.AppliedAt;
            model.Theme = MapThemeToDto(theme);
            model.Template = MapTemplateToDto(template);

            return Result<CompanyThemeDto>.Success(model);
        }

        public async Task<Result<CompanyThemeDto>> UpdateAsync(int companyId, CompanyThemeDto model)
        {
            var existingTheme = await _companyThemeRepository.GetEntityWithSpec(new CompanyThemeSpecification(companyId));
            if (existingTheme == null)
            {
                return Result<CompanyThemeDto>.Failure("Company theme not found");
            }

            existingTheme.ThemeId = model.ThemeId;
            existingTheme.TemplateId = model.TemplateId;
            existingTheme.AppliedAt = DateTime.UtcNow;

            await _companyThemeRepository.UpdateAsync(existingTheme);

            var updatedTheme = await _themeRepository.GetByIdAsync(model.ThemeId);
            var updatedTemplate = await _templateRepository.GetByIdAsync(model.TemplateId);

            model.AppliedAt = existingTheme.AppliedAt;
            model.Theme = MapThemeToDto(updatedTheme);
            model.Template = MapTemplateToDto(updatedTemplate);

            return Result<CompanyThemeDto>.Success(model);
        }

        public async Task<Result<CompanyThemeDto>> GetByCompanyIdAsync(int companyId)
        {
            var companyTheme = await _companyThemeRepository.GetEntityWithSpec(new CompanyThemeSpecification(companyId));
            if (companyTheme == null)
            {
                return Result<CompanyThemeDto>.Failure("Company theme not found");
            }

            var theme = await _themeRepository.GetByIdAsync(companyTheme.ThemeId);
            var template = await _templateRepository.GetByIdAsync(companyTheme.TemplateId);

            var dto = new CompanyThemeDto
            {
                Id = companyTheme.Id,
                CompanyId = companyTheme.CompanyId,
                ThemeId = companyTheme.ThemeId,
                TemplateId = companyTheme.TemplateId,
                AppliedAt = companyTheme.AppliedAt,
                Theme = MapThemeToDto(theme),
                Template = MapTemplateToDto(template)
            };

            return Result<CompanyThemeDto>.Success(dto);
        }

        private ThemeDto MapThemeToDto(Theme theme)
        {
            return new ThemeDto
            {
                Id = theme.Id,
                Name = theme.Name,
                PrimaryColor = theme.PrimaryColor,
                SecondaryColor = theme.SecondaryColor,
                BackgroundColor = theme.BackgroundColor,
                ButtonColor = theme.ButtonColor,
                TitleFontFamily = theme.TitleFontFamily,
                ContentFontFamily = theme.ContentFontFamily,
                IsBold = theme.IsBold,
                IsItalic = theme.IsItalic,
                FontSize = theme.FontSize
            };
        }

        private TemplateDto MapTemplateToDto(Template template)
        {
            return new TemplateDto
            {
                Id = template.Id,
                Name = template.Name,
                HtmlTemplate = template.HtmlTemplate,
                CssTemplate = template.CssTemplate
            };
        }
    }
}