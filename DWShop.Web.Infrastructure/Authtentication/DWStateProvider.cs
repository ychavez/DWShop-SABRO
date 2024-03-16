
using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Routes;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace DWShop.Web.Infrastructure.Authtentication
{
    public class DWStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public DWStateProvider(HttpClient httpClient,
            ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }
        public void MaskAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState =
                Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            byte[] ParseBase64(string payload)
            {
                payload = payload
                    .Trim()
                    .Replace('-', '+')
                    .Replace('_', '/');

                var base64 = payload

               .PadRight(payload.Length + (4 - payload.Length) % 4, '=');
                
                return Convert.FromBase64String(base64);
            }

            var claims = new List<Claim>();
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64(payload);
            var keyValuesPairs =
                JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuesPairs is not null)
            {
                keyValuesPairs.TryGetValue(ClaimTypes.Role, out var roles);
                if (roles is not null)
                {
                    if (roles.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedRoles =
                            JsonSerializer
                            .Deserialize<string[]>(roles.ToString()!);
                        claims.AddRange(parsedRoles!.Select(
                            x => new Claim(ClaimTypes.Role, x)));
                    }
                    else
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));

                    keyValuesPairs.Remove(ClaimTypes.Role);
                }
                claims
                    .AddRange(keyValuesPairs
                    .Select(x => new Claim(x.Key, x.Value.ToString()!)));
            }
            return claims;
        }

        public override async Task<AuthenticationState>  GetAuthenticationStateAsync()
        {



            string savedToken = ""; /* await localStorageService.GetItemAsync<string>(
                BaseConfiguration.AuthToken);*/
   


            if (string.IsNullOrWhiteSpace(savedToken)) 
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var state = new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(GetClaimsFromJwt(savedToken), "jwt")));

            var user = state.User;
            string? exp = user.FindFirst(x => x.Type == "exp")?.Value;
            var expTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(exp));
            var diff = expTime - DateTimeOffset.Now;
            if (diff.TotalMinutes >= 1)
            {
                return state;
            }

            await localStorageService.RemoveItemAsync(BaseConfiguration.AuthToken);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public  async Task StateChangedAsync()
        {
            var authState = Task.FromResult(await GetAuthenticationStateAsync());

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
