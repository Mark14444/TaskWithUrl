using FluentValidation;
using Inforce.Service.Dto.UrlDtos;

namespace Inforce.Service.Validators.Url
{
    public class CreateShortUrlDtoValidator:AbstractValidator<CreateShortUrlDto>
    {
        public CreateShortUrlDtoValidator()
        {
            RuleFor(x => x.OriginalUrl).NotEmpty();
        }
    }
}
