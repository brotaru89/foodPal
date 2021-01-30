using FoodPal.Orders.Enums;
using FoodPal.Orders.Models;
using System.Threading.Tasks;

namespace FoodPal.Orders.Data.Contracts
{
	public interface IOrderItemsRepository
	{
		Task UpdateStatusAsync(OrderItem orderItemEntity, OrderItemStatus newStatus);
	}
}
