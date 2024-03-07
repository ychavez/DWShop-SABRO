using DWShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DWShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepositoryAsync<,>),typeof(RepositoryAsync<,>));
        }
    }
}
