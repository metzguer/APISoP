using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.Config.RegisterServices
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
            //services.AddTransient<ICashRegisterRepository, CashRegisterRepository>();
            //services.AddTransient<IEnterpriseRepository, EnterpriseRepository>();
            //services.AddTransient<IMembershipRepository, MembershipRepository>();
            //services.AddTransient<IPermissionRepository, PermissionRepository>();
            //services.AddTransient<IProfilePermissionRepository, ProfilePermissionsRepository>();
            //services.AddTransient<IProfileRepository, ProfileRepository>();
            //services.AddTransient<IStoreRepository, StoreRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection RegisterServices(IServiceCollection services)
        {
            //services.AddTransient<ICashRegisterService, CashRegisterService>();
            //services.AddTransient<IEnterpriseService, EnterpriseService>();
            //services.AddTransient<IMembershipService, MembershipService>();
            //services.AddTransient<IPermissionService, PermissionService>();
            //services.AddTransient<IProfilePermissionService, ProfilePermissionsService>();
            //services.AddTransient<IProfileService, ProfileService>();
            //services.AddTransient<IStoreService, StoreService>();
            //services.AddTransient<IUsersService, UserService>();
            return services;
        }

        private static IServiceCollection RegisterOthers(IServiceCollection services)
        {
            //services.AddTransient<IAppConfig, AppConfig>();

            return services;
        }
    }
}
