using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Orders.Contracts
{
	public interface IOrdersService
	{
		Task<string> Create(NewOrderDto newOrder);

		Task<OrderDto> GetByIdAsync(int orderId);

		Task<OrderStatusDto> GetStatusAsync(int orderId);

		Task<IEnumerable<OrderDto>> GetByFiltersAsync(string customerId, OrderStatus? status, int page, int pageSize);
	}
}
