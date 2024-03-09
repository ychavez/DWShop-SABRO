using DWShop.Application.Interfaces;
using DWShop.Infrastructure.Repositories;
using DWShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DWShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            return services.AddScoped(typeof(IRepositoryAsync<,>),typeof(RepositoryAsync<,>));

        }
    }
}
