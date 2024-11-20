using FluentValidation;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;


namespace QRMenu.Application.Companies.Commands
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

            RuleFor(v => v.Slug)
                .NotEmpty().WithMessage("Slug is required")
                .MaximumLength(200).WithMessage("Slug must not exceed 200 characters")
                .MustAsync(BeUniqueSlug).WithMessage("The specified slug already exists");

            RuleFor(v => v.DealerId)
                .NotEmpty().WithMessage("Dealer Id is required");

            RuleFor(v => v.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid");

            RuleFor(v => v.DefaultLanguage)
                .NotEmpty().WithMessage("Default language is required")
                .Must((command, defaultLang) => command.LanguagesSupported.Contains(defaultLang))
                .WithMessage("Default language must be one of the supported languages");
        }

        private async Task<bool> BeUniqueSlug(string slug, CancellationToken cancellationToken)
        {
            return !await _companyRepository.SlugExistsAsync(slug);
        }
    }
}
