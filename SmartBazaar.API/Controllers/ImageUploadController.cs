using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SmartBazaar.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ImageUploadController : ControllerBase
	{
		private readonly Cloudinary _cloudinary;

		public ImageUploadController(IConfiguration configuration)
		{
			var cloudName = configuration["Cloudinary:CloudName"];
			var apiKey = configuration["Cloudinary:ApiKey"];
			var apiSecret = configuration["Cloudinary:ApiSecret"];

			var account = new Account(cloudName, apiKey, apiSecret);
			_cloudinary = new Cloudinary(account);
		}

		[HttpPost("upload")]
		public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
		{
			try
			{
				if (file == null || file.Length == 0)
				{
					return BadRequest(new { error = "No file uploaded" });
				}

				// Validate file type
				var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
				if (!allowedTypes.Contains(file.ContentType.ToLower()))
				{
					return BadRequest(new { error = "Invalid file type. Only JPEG, PNG, GIF, and WebP are allowed." });
				}

				// Validate file size (max 5MB)
				if (file.Length > 5 * 1024 * 1024)
				{
					return BadRequest(new { error = "File size exceeds 5MB limit." });
				}

				// Upload to Cloudinary
				using var stream = file.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(file.FileName, stream),
					Folder = "smart_bazaar",
					Transformation = new Transformation()
						.Width(800)
						.Height(800)
						.Crop("limit")
						.Quality("auto")
				};

				var uploadResult = await _cloudinary.UploadAsync(uploadParams);

				if (uploadResult.Error != null)
				{
					return BadRequest(new { error = uploadResult.Error.Message });
				}

				return Ok(new
				{
					url = uploadResult.SecureUrl.ToString(),
					publicId = uploadResult.PublicId,
					width = uploadResult.Width,
					height = uploadResult.Height,
					format = uploadResult.Format
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { error = $"Upload failed: {ex.Message}" });
			}
		}

		[HttpDelete("delete/{publicId}")]
		public async Task<IActionResult> DeleteImage(string publicId)
		{
			try
			{
				// Decode the public ID (replace URL encoding)
				publicId = Uri.UnescapeDataString(publicId);

				var deleteParams = new DeletionParams(publicId);
				var result = await _cloudinary.DestroyAsync(deleteParams);

				if (result.Result == "ok")
				{
					return Ok(new { message = "Image deleted successfully" });
				}

				return BadRequest(new { error = "Failed to delete image" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { error = $"Delete failed: {ex.Message}" });
			}
		}
	}
}
