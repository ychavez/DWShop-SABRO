using DWShop.Client.Infrastructure.Routes;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(BaseConfiguration.BaseAddress) });
builder.Services.AddMudServices();


await builder.Build().RunAsync();
