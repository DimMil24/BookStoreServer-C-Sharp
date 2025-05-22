using System.ComponentModel.DataAnnotations;

namespace BookStoreServerNet.Models.Requests
{
	public class RegisterRequest
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Required]
		[Length(3,15)]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
