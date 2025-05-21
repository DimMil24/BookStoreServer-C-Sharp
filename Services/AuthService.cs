using BookStoreServerNet.Models.Requests;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreServerNet.Services
{
	public class AuthService
	{

		private readonly IConfiguration _config;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AuthService(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{

			_config = config;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IdentityResult> Register(RegisterRequest registerRequest)
		{
			var user = new IdentityUser { UserName = registerRequest.Username, Email = registerRequest.Email };
			

			return await _userManager.CreateAsync(user, registerRequest.Password);
		}

		public async Task<string?> Login(LoginRequest loginRequest)
		{
			var user = await _userManager.FindByNameAsync(loginRequest.Username);
			if (user == null)
				return null;

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
			if (!result.Succeeded)
				return null;

			return GenerateToken(user);
		}

		public string GenerateToken(IdentityUser user)
		{
			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id)
		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["JwtSettings:Issuer"],
				audience: _config["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(2),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
