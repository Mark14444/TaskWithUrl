
using FluentValidation;
using Inforce.Service.Dto.AuthorizationDtos;

namespace Inforce.Service.Validators.Authorization
{
    public class AuthRequestDtoValidator:AbstractValidator<AuthRequestDto>
    {
        public AuthRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
