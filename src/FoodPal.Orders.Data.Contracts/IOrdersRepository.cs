using FoodPal.Orders.Models;
using System.Threading.Tasks;

namespace FoodPal.Orders.Data.Contracts
{
	public interface IOrdersRepository
	{
		Task<Order> CreateAsync(Order newOrder);
	}
}
