using FoodPal.Orders.Data.Contracts;
using FoodPal.Orders.Models;
using System;
using System.Threading.Tasks;

namespace FoodPal.Orders.Data.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private readonly OrdersContext _ordersContext;

		public OrdersRepository(OrdersContext ordersContext)
		{
			_ordersContext = ordersContext;
		}

		public async Task<Order> CreateAsync(Order newOrder)
		{
			if (newOrder is null) throw new ArgumentNullException(nameof(newOrder));

			try
			{
				await _ordersContext.AddAsync(newOrder);
				await _ordersContext.SaveChangesAsync();

				return newOrder;
			}
			catch (Exception ex)
			{
				throw new Exception($"{nameof(newOrder)} could not be saved: {ex.Message}");
			}
		}
	}
}
