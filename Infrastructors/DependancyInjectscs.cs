using Core;
using Infrastructors.Contexts;
using Infrastructors.Repositories;
using Microsoft.AspNetCore.Builder;
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
