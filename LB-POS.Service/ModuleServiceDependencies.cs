using LB_POS.Service.IService;
using LB_POS.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LB_POS.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IBranchService, BranchService>();
            return services;
        }
    }
}
