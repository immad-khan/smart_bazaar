using System.ComponentModel.DataAnnotations;

namespace SmartBazaar.API.Models
{
	public class LoginRequest
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}

	public class RegisterRequest
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }
	}

	public class AddStoreRequest
	{
		[Required]
		public int SellerID { get; set; }

		[Required]
		[StringLength(100)]
		public string StoreName { get; set; }

		[Required]
		[StringLength(255)]
		public string Address { get; set; }

		[Required]
		[StringLength(20)]
		public string ContactNumber { get; set; }

		public string Description { get; set; }
	}

	public class AddProductRequest
	{
		[Required]
		public int StoreID { get; set; }

		[Required]
		[StringLength(100)]
		public string ProductName { get; set; }

		public string Description { get; set; }

		[Required]
		[Range(0.01, 1000000)]
		public decimal Price { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int StockQuantity { get; set; }

		[Required]
		[StringLength(50)]
		public string Category { get; set; }

		public string ImageUrl { get; set; }
	}
}