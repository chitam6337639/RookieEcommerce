using StandardLibrary.Account;
using WebAPIEcommerce.Data.Dtos.Account;

namespace WebMVCEcommerce.Services.Authentication
{
    public class AuthenticationApiClient : IAuthenticatonApiClient
    {
        private readonly HttpClient _httpClient;

        public AuthenticationApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7245");
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/account/register", registerDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<NewUserDto>();
                return result?.Token;
            }
            return null;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/account/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<NewUserDto>();
                return result?.Token;
            }
            return null;
        }
		public async Task LogoutAsync()
		{
            await _httpClient.PostAsync("/api/account/logout",null);
		}
	}
}
