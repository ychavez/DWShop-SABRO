using DWShop.Web.Client.Client.Pages;
using DWShop.Web.Client.Components;
using DWShop.Web.Client.Extensions;
using MudBlazor.Services;

namespace DWShop.Web.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.AddClientServices();

            builder.Services.AddMudServices();
            builder.Services.AddManagers();

            var app = builder.Build();

           // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Products).Assembly);

            app.Run();
        }
    }
}
