using FoodPal.Orders.Dtos;
using System.Threading.Tasks;

namespace FoodPal.Orders.Contracts
{
	public interface IOrderItemService
	{
		Task PatchOrderItem(int orderId, int orderItemId, GenericPatchDto orderItemPatch);
	}
}
