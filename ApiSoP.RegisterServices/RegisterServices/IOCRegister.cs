using APiSoP.Domain.Contracts.CRUD;
using APiSoP.Domain.Services;
using APiSoP.Services.CustomIdentity;
using APISoP.Data.Contracts.CRUD;
using APISoP.Data.Repositories.CRUD;
using Microsoft.Extensions.DependencyInjection; 

namespace ApiSoP.RegisterServices.RegisterServices
{
    public static class IOCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterOthers(services);

            return services;
        }

        private static IServiceCollection RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<ICashRegisterRepository, CashRegisterRepository>();
            services.AddTransient<IEnterpriseRepository, EnterpriseRepository>();
            services.AddTransient<IMembershipRepository, MembershipRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IProfilePermissionRepository, ProfilePermissionsRepository>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICashRegisterService, CashRegisterService>();
            services.AddTransient<IEnterpriseService, EnterpriseService>();
            services.AddTransient<IMembershipService, MembershipService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IProfilePermissionService, ProfilePermissionsService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IUsersService, UserService>();
            return services;
        }

        private static IServiceCollection RegisterOthers(IServiceCollection services)
        {
            services.AddScoped<IUserIdentity, UserIdentityService>();

            return services;
        }
    }
}
