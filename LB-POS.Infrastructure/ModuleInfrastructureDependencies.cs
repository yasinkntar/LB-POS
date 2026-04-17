using LB_POS.Infrastructure.Abstracts;
using LB_POS.Infrastructure.BaseInfrastructure;
using LB_POS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LB_POS.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<ISectionsRepository, SectionsRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
