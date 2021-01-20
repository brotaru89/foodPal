using FoodPal.Orders.Enums;
using FoodPal.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Orders.Data.Contracts
{
	public interface IOrdersRepository
	{
		Task<Order> CreateAsync(Order newOrder);
		
		Task<Order> GetByIdAsync(int orderId);

		Task<(IEnumerable<Order> Orders, int AllOrdersCount)> GetByFiltersAsync(string customerId, OrderStatus? status, int page, int pageSize);

		Task<OrderStatus?> GetStatusAsync(int orderId);

		Task UpdateStatusAsync(Order orderEntity, OrderStatus newStatus);
	}
}
