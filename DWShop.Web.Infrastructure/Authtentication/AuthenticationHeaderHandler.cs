
using Blazored.LocalStorage;
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
            if (request.Headers.Authorization?.Scheme != "Bearer") 
            {

                var savedToken = await _localStorageService.GetItemAsync<string>("authToken");
                if (!string.IsNullOrWhiteSpace(savedToken)) {

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
