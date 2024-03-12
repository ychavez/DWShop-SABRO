
using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Routes;
using System.Net.Http.Headers;

namespace DWShop.Web.Infrastructure.Authtentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationHeaderHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != BaseConfiguration.Scheme) 
            {

                var savedToken = await _localStorageService.GetItemAsync<string>(BaseConfiguration.AuthToken);
                if (!string.IsNullOrWhiteSpace(savedToken)) {

                    request.Headers.Authorization = new AuthenticationHeaderValue(BaseConfiguration.Scheme, 
                        savedToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
