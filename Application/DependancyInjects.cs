using Application.Interfaces;
using Application.Services;
using Infrastructors;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace Application
{
   public static class DependancyInjects
    {
        public static void AddApplicationDI(this IServiceCollection services)
        {
            services.AddInfrastraction();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserManager, UserService>();

           
        }
    }
}
