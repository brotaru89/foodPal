using FoodPal.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodPal.Orders.Data
{
	public class OrdersContext : DbContext
	{
		public OrdersContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public DbSet<Order> Orders { get; set; }
	}
}
