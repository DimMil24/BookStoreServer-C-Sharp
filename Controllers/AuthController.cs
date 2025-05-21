using BookStoreServerNet.Models.Requests;
using BookStoreServerNet.Services;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServerNet.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;

		public AuthController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterRequest registerRequest)
		{
			var result = await _authService.Register(registerRequest);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok("User registered successfully.");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			var token = await _authService.Login(loginRequest);
			if (token == null)
				return Unauthorized("Invalid credentials.");

			return Ok(new { token });
		}
	}
}
