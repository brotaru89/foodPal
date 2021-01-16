using FoodPal.Orders.Dtos;
using System.Threading.Tasks;

namespace FoodPal.Orders.Contracts
{
	public interface IOrdersService
	{
		Task<string> Create(NewOrderDto newOrder);
	}
}
