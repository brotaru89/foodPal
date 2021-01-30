using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;
using System.Threading.Tasks;

namespace FoodPal.Orders.Contracts
{
	public interface IOrdersService
	{
		Task<string> Create(NewOrderDto newOrder);

		Task<OrderDto> GetByIdAsync(int orderId);

		Task<StatusDto> GetStatusAsync(int orderId);

		Task<PagedResultSetDto<OrderDto>> GetByFiltersAsync(string customerId, OrderStatus? status, int page, int pageSize);

		Task PatchOrder(int orderId, OrderPatchDto orderPatch);
	}
}
