
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StandardLibrary.Account;
using System;
using System.Threading.Tasks;
using WebAPIEcommerce.Data.Dtos.Account;
using WebAPIEcommerce.Interfaces;

namespace WebAPIEcommerce.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newUser = await _accountRepository.RegisterAsync(registerDto);
                return Ok(newUser);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loggedInUser = await _accountRepository.LoginAsync(loginDto);
                return Ok(loggedInUser);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await _accountRepository.LogoutAsync();
				return Ok("Logout successful");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("allusers")]
		public async Task<IActionResult> GetAllUsers()
		{
			try
			{
				var allUsers = await _accountRepository.GetAllUsersAsync();
				return Ok(allUsers);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
