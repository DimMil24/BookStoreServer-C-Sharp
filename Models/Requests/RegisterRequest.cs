using System.ComponentModel.DataAnnotations;

namespace BookStoreServerNet.Models.Requests
{
	public class RegisterRequest
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Required]
		[Range(3,14)]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
