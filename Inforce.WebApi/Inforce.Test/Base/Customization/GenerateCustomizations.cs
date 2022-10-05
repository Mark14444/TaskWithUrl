using AutoFixture;
using Inforce.Service.Dto.AuthorizationDtos;
using Inforce.Service.Dto.UrlDtos;
using Inforce.Service.Dto.UserDtos;
using System.Net.Mail;

namespace Fragments.Test.Base.Customization
{
    public class GenerateCustomizations : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CreateUserDto>(composer =>
            composer
            .With(x =>
            x.Email,
            fixture.Create<MailAddress>().ToString())
            .With(x =>
            x.Password,
            fixture.Create<string>())
            );

            fixture.Customize<CreateShortUrlDto>(composer =>
            composer
            .With(x =>
            x.OriginalUrl,
            fixture.Create<string>())
            );

            fixture.Customize<AuthRequestDto>(composer =>
            composer
            .With(x =>
            x.Email,
            fixture.Create<MailAddress>().ToString())
            .With(x =>
            x.Password,
            fixture.Create<string>())
            );
        }
    }
}
