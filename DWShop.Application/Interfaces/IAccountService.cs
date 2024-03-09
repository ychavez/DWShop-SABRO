
using Microsoft.AspNetCore.Identity;

namespace DWShop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);

        Task<string> GetToken(IdentityUser user);
    }
}
