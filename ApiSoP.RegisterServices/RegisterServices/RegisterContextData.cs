using APISoP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 

namespace ApiSoP.RegisterServices.RegisterServices
{
    public static class RegisterContextData
    {
        
        public static IServiceCollection AddRegistration(this IServiceCollection services, string con)
        {
            services.AddDbContext<ApiSoPDbContext>(options => options.UseSqlServer(con)); 
            return services;
        }
    }
}
