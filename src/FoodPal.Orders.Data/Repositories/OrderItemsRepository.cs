using FoodPal.Orders.Data.Contracts;
using FoodPal.Orders.Enums;
using FoodPal.Orders.Models;
using System.Threading.Tasks;

namespace FoodPal.Orders.Data.Repositories
{
	public class OrderItemsRepository : IOrderItemsRepository
	{
		private readonly OrdersContext _ordersContext;

		public OrderItemsRepository(OrdersContext ordersContext)
		{
			_ordersContext = ordersContext;
		}

		public async Task UpdateStatusAsync(OrderItem orderItemEntity, OrderItemStatus newStatus)
		{
			orderItemEntity.Status = newStatus;
			await _ordersContext.SaveChangesAsync();
		}
	}
}
