using Core;
using Infrastructors.Contexts;
using Infrastructors.RabbitMqServices.Interfaces;
using Infrastructors.RabbitMqServices.Options;
using Infrastructors.RabbitMqServices.RabbitMqConfig.Interfase;
using Infrastructors.RabbitMqServices.RabbitMqConfig.Service;
using Infrastructors.RabbitMqServices.Services;
using Infrastructors.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructors
{
    public static class DependancyInjectscs
    {
        public static void AddInfrastraction(this IServiceCollection services)
        {
            services.AddDbContext<UsersDbCOntext>();

            services.AddScoped<IRepository, UserRposries>();

            services.AddDataProtection() 
                    .PersistKeysToFileSystem(new DirectoryInfo(@"keyFile")) // Or other shared storage
                    .SetApplicationName("SharedCookieApp");

            services.AddSingleton<IChannelRabbitMq, RabbitChannelService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();

            services.AddAuthentication()
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = ".AspNet.SharedCookie";
                        options.Cookie.Domain = "arch.homework";
                        options.Cookie.Path = "/";
                        options.Cookie.SameSite = SameSiteMode.None;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        options.Cookie.HttpOnly = true;

                    });
             

            services.AddIdentity<User, IdentityRole>(opt =>

            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<UsersDbCOntext>();
        }

    }
}
