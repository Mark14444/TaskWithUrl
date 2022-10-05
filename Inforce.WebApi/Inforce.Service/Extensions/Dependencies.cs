using AutoMapper;
using FluentValidation.AspNetCore;
using Inforce.Domain.Entities;
using Inforce.Repository;
using Inforce.Repository.Repo;
using Inforce.Service.Profiles;
using Inforce.Service.Services.Implementation;
using Inforce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Inforce.Services.Extensions
{
    public static class Dependencies
    {
        public static void AddDb(this IServiceCollection services)
        {
            services.AddDbContext<InforceContext>();
        }
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Url>, Repository<Url>>();
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new List<Profile> { new UserProfile(), new UrlProfile()});
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<UserService>()); 
        }
        public static void AddJwtValidation(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
