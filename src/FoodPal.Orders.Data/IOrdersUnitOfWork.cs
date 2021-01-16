using FoodPal.Orders.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodPal.Orders.Data
{
	public interface IOrdersUnitOfWork
	{
		IOrdersRepository OrdersRepository { get; }
	}
}
