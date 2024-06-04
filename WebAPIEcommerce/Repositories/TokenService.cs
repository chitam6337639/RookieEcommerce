﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Repositories
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;
		public TokenService(IConfiguration config) 
		{
			_config = config;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));

		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.UserName),
				//new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			};


			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _config["JWT:Issuer"],
				Audience = _config["JWT:Audience"],
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
