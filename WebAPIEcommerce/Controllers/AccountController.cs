//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StandardLibrary.Account;
//using WebAPIEcommerce.Data.Dtos.Account;
//using WebAPIEcommerce.Interfaces;
//using WebAPIEcommerce.Models.Entities;

//namespace WebAPIEcommerce.Controllers
//{
//    [Route("api/account")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly ITokenService _tokenService;
//        private readonly SignInManager<User> _signInManager;
//        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
//        {
//            _userManager = userManager;
//            _tokenService = tokenService;
//            _signInManager = signInManager;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest(ModelState);
//                }

//                var user = new User
//                {
//                    UserName = registerDto.UserName,
//                    Email = registerDto.Email,
//                };
//                var createcUser = await _userManager.CreateAsync(user, registerDto.Password!);

//                if (createcUser.Succeeded)
//                {
//                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
//                    if (roleResult.Succeeded)
//                    {
//                        return Ok(
//                            new NewUserDto
//                            {
//                                UserName = user.UserName,
//                                Email = user.Email!,
//                                Token = _tokenService.CreateToken(user)
//                            });

//                    }
//                    else
//                    {
//                        return StatusCode(500, roleResult.Errors);
//                    }
//                }
//                else
//                {
//                    return StatusCode(500, createcUser.Errors);
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, ex);
//            }
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login(LoginDto loginDto)
//        {
//			if (!ModelState.IsValid)
//				return BadRequest(ModelState);

//			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

//			if (user == null) return Unauthorized("Invalid username!");

//			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

//			if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

//			return Ok(
//				new NewUserDto
//				{
//					UserName = user.UserName,
//					Email = user.Email,
//					Token = _tokenService.CreateToken(user)
//				}
//			);
//		}
//    }
//}

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
    }
}
