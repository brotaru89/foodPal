using FoodPal.Orders.Dtos;
using FoodPal.Orders.Models;

namespace FoodPal.Orders.Mappers
{
	internal class OrderProfile : AbstractProfile
	{
		public OrderProfile()
		{
			// NewOrderDto to Order data model
			CreateMap<NewOrderDto, Order>();

			// Order data model to OrderDto
			CreateMap<Order, OrderDto>();
		}
	}
}
