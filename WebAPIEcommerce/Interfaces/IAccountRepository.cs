using StandardLibrary.Account;
using WebAPIEcommerce.Data.Dtos.Account;

namespace WebAPIEcommerce.Interfaces
{
	public interface IAccountRepository
	{
        Task<NewUserDto> RegisterAsync(RegisterDto registerDto);
        Task<NewUserDto> LoginAsync(LoginDto loginDto);
		Task LogoutAsync();
		Task<List<UserInfoDto>> GetAllUsersAsync();
	}
}
