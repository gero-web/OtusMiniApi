using Infrastructors.Contexts;
using Infrastructors.Repositories; 
using Microsoft.Extensions.DependencyInjection; 

namespace Infrastructors
{
    public static class DependancyInjectscs
    {
        public static void AddInfrastraction(this IServiceCollection services)
        {
            services.AddDbContext<UsersDbCOntext>();

            services.AddScoped<IRepository, UserRposries>();

 
        }
    }
}
