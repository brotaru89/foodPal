using FoodPal.Orders.Enums;
using FoodPal.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Orders.Data.Contracts
{
	public interface IOrderItemsRepository
	{
		Task<OrderItem> GetOrderItemAsync(int orderId, int orderItemId);

		Task<List<OrderItem>> GetItemsAsync(int orderId);

		Task UpdateStatusAsync(OrderItem orderItemEntity, OrderItemStatus newStatus);
	}
}
