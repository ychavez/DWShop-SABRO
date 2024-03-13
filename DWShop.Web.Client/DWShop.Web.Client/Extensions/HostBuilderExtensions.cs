
using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Managers;
using DWShop.Client.Infrastructure.Routes;
using DWShop.Web.Infrastructure.Authtentication;
using DWShop.Web.Infrastructure.Services;

namespace DWShop.Web.Client.Extensions
{
    public static class HostBuilderExtensions
    {

        public static WebApplicationBuilder AddClientServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ClientPreferenceServices>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddManagers();
            builder.Services.AddTransient<AuthenticationHeaderHandler>();
            builder.Services.AddHttpClient(BaseConfiguration.BaseAddress, x=> { })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();


             return builder;

        }

        public static IServiceCollection AddManagers(this IServiceCollection services) 
        {
            var managers = typeof(IManager);

            var types = managers.Assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new
                {
                    Service = x.GetInterface($"I{x.Name}"),
                    Implementation = x
                })
                .Where(x => x.Service is not null);

            foreach (var type in types)
                if (managers.IsAssignableFrom(type.Service))
               services.AddTransient(type.Service, type.Implementation);

            return services;
        }
    }
}
