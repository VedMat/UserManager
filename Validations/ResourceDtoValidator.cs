using FluentValidation;
using UserManager.DTOs;

namespace UserManager.Validations
{
    public class ResourceDtoValidator : AbstractValidator<ResourceDto>
    {
        public ResourceDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Url).NotEmpty().WithMessage("URL is required").Must(BeAValidUrl).WithMessage("Invalid URL");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
