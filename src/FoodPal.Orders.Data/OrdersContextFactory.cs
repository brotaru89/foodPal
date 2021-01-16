using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FoodPal.Orders.Data
{
	public class OrdersContextFactory : IDesignTimeDbContextFactory<OrdersContext>, IOrdersContextFactory
	{
		public OrdersContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				//.Add
				.Build();

			var dbContextBuilder = new DbContextOptionsBuilder();
			var connectionString = configuration.GetConnectionString("OrdersConnectionString");

			dbContextBuilder.UseSqlServer(connectionString);

			return new OrdersContext(dbContextBuilder.Options);
		}

		public OrdersContext CreateDbContext(string connectionString)
		{
			var dbContextBuilder = new DbContextOptionsBuilder()
				.UseSqlServer(connectionString);

			return new OrdersContext(dbContextBuilder.Options);
		}
	}
}
