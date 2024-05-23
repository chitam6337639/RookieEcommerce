using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StandardLibrary.Account;
using System;
using System.Threading.Tasks;
using WebAPIEcommerce.Data.Dtos.Account;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<NewUserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };
            var createdUser = await _userManager.CreateAsync(user, registerDto.Password!);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    return new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email!,
                        Token = _tokenService.CreateToken(user)
                    };
                }
                else
                {
                    throw new InvalidOperationException("Failed to add user to role");
                }
            }
            else
            {
                throw new InvalidOperationException("Failed to create user");
            }
        }

        public async Task<NewUserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Username not found and/or password incorrect");
            }

            return new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
