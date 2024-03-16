using Blazored.LocalStorage;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Client.Infrastructure.Managers.Identity.Login;
using DWShop.Client.Infrastructure.Routes;
using DWShop.Shared.Wrapper;
using DWShop.Web.Infrastructure.Authtentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;


namespace DWShop.Web.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly HttpClient httpClient;

        public LoginService(IAuthenticationManager authenticationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider, 
            HttpClient httpClient)
        {
            this.authenticationManager = authenticationManager;
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
            this.httpClient = httpClient;
        }
        public async Task<IResult> Login(LoginCommand loginCommand)
        {
            var result = await authenticationManager.Login(loginCommand);

            if (result.Succeded)
            {
                await localStorageService.SetItemAsStringAsync(BaseConfiguration.AuthToken, 
                    result.Data.Token);
                await ((DWStateProvider)authenticationStateProvider).StateChangedAsync();

                httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue(BaseConfiguration.Scheme, result.Data.Token);

                return await Result.SuccessAsync();

            }
            return await Result.FailAsync(result.Messages);
        }
    }
}
