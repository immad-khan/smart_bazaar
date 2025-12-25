using Dapper;
using Npgsql;
using System.Data;

namespace SmartBazaar.API.Data
{

	public class DatabaseContext
	{
		private readonly string _connectionString;

		public DatabaseContext(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Database connection string not configured.");
		}

		public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
	}
}