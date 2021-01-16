using FoodPal.Orders.Data.Contracts;
using FoodPal.Orders.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodPal.Orders.Data
{
	public class OrdersUnitOfWork : IOrdersUnitOfWork
	{
		private readonly Lazy<IOrdersRepository> _ordersRepository;

		public OrdersUnitOfWork(OrdersContext dbContext)
		{
			_ordersRepository = new Lazy<IOrdersRepository>(new OrdersRepository(dbContext));
		}

		//public void GetContext()
		//{
		//}

		public IOrdersRepository OrdersRepository => _ordersRepository.Value;
	}
}
