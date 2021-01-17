using FoodPal.Orders.Dtos;
using FoodPal.Orders.Models;

namespace FoodPal.Orders.Mappers
{
	internal class OrderItemProfile : AbstractProfile
	{
		public OrderItemProfile()
		{
			CreateMap<NewOrderItemDto, OrderItem>();

			CreateMap<OrderItem, OrderItemDto>();
		}
	}
}
