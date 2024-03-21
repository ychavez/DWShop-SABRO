
using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Managers;
using DWShop.Client.Infrastructure.Routes;
using DWShop.Web.Infrastructure.Authtentication;
using DWShop.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace DWShop.Web.Client.Extensions
{
    public static class HostBuilderExtensions
    {

        public static WebApplicationBuilder AddClientServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ClientPreferenceServices>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<DWStateProvider>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<AuthenticationStateProvider, DWStateProvider>();
            builder.Services.AddManagers();
            builder.Services.AddTransient<AuthenticationHeaderHandler>();
            builder.Services.AddAuthorizationCore(options => RegisterPermissionClaims(options));
            builder.Services.AddHttpClient("", x =>

                x.BaseAddress = new Uri(
                    BaseConfiguration.BaseAddress)
{ })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();



            return builder;

        }
        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {

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
