using FoodPal.Orders.Dtos;
using FoodPal.Orders.Models;

namespace FoodPal.Orders.BackgroundServices.Mappers
{
	internal class OrderProfile : AbstractProfile
	{
		public OrderProfile()
		{
			CreateMap<NewOrderDto, Order>();
		}
	}
}
