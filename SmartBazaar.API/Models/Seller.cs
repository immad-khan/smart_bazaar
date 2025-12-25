using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.API.Models
{
	public class Seller
	{
		public int SellerID { get; set; }

		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Phone]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}