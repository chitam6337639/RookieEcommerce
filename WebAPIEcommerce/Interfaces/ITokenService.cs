using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}
