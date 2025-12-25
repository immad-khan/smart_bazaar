using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using SmartBazaar.API.Models;
using SmartBazaar.API.Services;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SmartBazaar.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly ProductService _productService;
		private readonly string _connectionString;

		public ProductsController(ProductService productService, IConfiguration configuration)
		{
			_productService = productService;
			_connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Database connection string not configured.");
		}

		private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

		// POST: api/products
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddProductRequest request)
		{
			try
			{
				// Check if the store exists using a raw SQL query with Dapper
				using (var connection = CreateConnection())
				{
					var storeExists = await connection.ExecuteScalarAsync<bool>(
						"SELECT EXISTS (SELECT 1 FROM stores WHERE storeid = @StoreID)",
						new { StoreID = request.StoreID });

					if (!storeExists)
					{
						return BadRequest("Store with the given ID does not exist.");
					}
				}

				var product = new Models.Product
				{
					StoreID = request.StoreID,
					ProductName = request.ProductName,
					Description = request.Description,
					Price = request.Price,
					StockQuantity = request.StockQuantity,
					Category = request.Category,
					ImageUrl = request.ImageUrl
				};

				// Check if a product with the same name already exists in the store
				if (await _productService.ExistsByNameAsync(product.ProductName, product.StoreID))
				{
					return BadRequest("Product name already exists in the store.");
				}

				// Create the product
				var productId = await _productService.CreateAsync(product);

				// Return success response with the created product ID
				return CreatedAtAction(nameof(Add), new { productId = productId });
			}
			catch (Exception ex)
			{
				// Return bad request with exception message in case of errors
				return BadRequest(ex.Message);
			}
		}

		// GET: api/products
		[HttpGet]
		public async Task<IActionResult> GetByStoreId([FromQuery] int storeId)
		{
			try
			{
				// Fetch products for the specified store ID
				var products = await _productService.GetByStoreIdAsync(storeId);

				// Return the list of products for the store
				return Ok(products);
			}
			catch (Exception ex)
			{
				// Return bad request in case of any error
				return BadRequest(ex.Message);
			}
		}

		// GET: api/products/search
		[HttpGet("search")]
		public async Task<IActionResult> SearchProducts([FromQuery] string q)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(q))
				{
					return BadRequest("Search query cannot be empty.");
				}

				// Search products in database by name
				using (var connection = CreateConnection())
				{
					var sql = @"
						SELECT p.*, s.storename, s.address 
						FROM products p
						INNER JOIN stores s ON p.storeid = s.storeid
						WHERE LOWER(p.productname) LIKE LOWER(@SearchTerm) 
						   OR LOWER(p.description) LIKE LOWER(@SearchTerm)
						   OR LOWER(p.category) LIKE LOWER(@SearchTerm)
						ORDER BY p.createdat DESC
						LIMIT 50";

					var products = await connection.QueryAsync<dynamic>(
						sql, 
						new { SearchTerm = $"%{q}%" }
					);

					return Ok(products);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT: api/products
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] Models.Product product)
		{
			try
			{
				// Update product in the database
				var updated = await _productService.UpdateAsync(product);
				if (!updated)
				{
					return NotFound("Product not found.");
				}

				// Return success response with updated product
				return Ok(product);
			}
			catch (Exception ex)
			{
				// Return bad request in case of any error
				return BadRequest(ex.Message);
			}
		}

		// DELETE: api/products/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				// Delete the product by ID
				var deleted = await _productService.DeleteAsync(id);
				if (!deleted)
				{
					return NotFound("Product not found.");
				}

				// Return no content (successful deletion)
				return NoContent();
			}
			catch (Exception ex)
			{
				// Return bad request in case of any error
				return BadRequest(ex.Message);
			}
		}
	}
}
