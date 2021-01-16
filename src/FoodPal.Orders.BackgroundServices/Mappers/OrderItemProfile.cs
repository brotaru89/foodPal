using FoodPal.Orders.Dtos;
using FoodPal.Orders.Models;

namespace FoodPal.Orders.BackgroundServices.Mappers
{
	internal class OrderItemProfile : AbstractProfile
	{
		public OrderItemProfile()
		{
			CreateMap<NewOrderItemDto, OrderItem>();
		}
	}
}
