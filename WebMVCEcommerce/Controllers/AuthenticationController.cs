using Microsoft.AspNetCore.Mvc;
using StandardLibrary.Account;
using System;
using System.Threading.Tasks;
using WebAPIEcommerce.Data.Dtos.Account;
using WebMVCEcommerce.Services.Authentication;

namespace WebMVCEcommerce.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticatonApiClient _authenticationApiClient;

        public AuthenticationController(IAuthenticatonApiClient authenticationApiClient)
        {
            _authenticationApiClient = authenticationApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var token = await _authenticationApiClient.RegisterAsync(registerDto);
            if (token != null)
            {
                SetTokenCookie(token);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Registration failed");
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var token = await _authenticationApiClient.LoginAsync(loginDto);
            if (token != null)
            {
                SetTokenCookie(token);
                return RedirectToAction("Index","Home");
            }

            ModelState.AddModelError("", "Login failed");
            return RedirectToAction("Index","Home");
        }

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _authenticationApiClient.LogoutAsync();
			Response.Cookies.Delete("jwt"); 
			return RedirectToAction("Index", "Home");
		}

		private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true, 
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("jwt", token, cookieOptions);
        }
    }
}
