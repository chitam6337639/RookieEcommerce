using StandardLibrary.Account;
using WebAPIEcommerce.Data.Dtos.Account;

namespace WebMVCEcommerce.Services.Authentication
{
    public interface IAuthenticatonApiClient
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
