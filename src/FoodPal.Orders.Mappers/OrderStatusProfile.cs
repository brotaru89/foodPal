using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;

namespace FoodPal.Orders.Mappers
{
	public class OrderStatusProfile : AbstractProfile
	{
		public OrderStatusProfile()
		{
			CreateMap<OrderStatus, OrderStatusDto>().ConvertUsing(opt => new OrderStatusDto
			{
				StatusId = (int)opt,
				StatusName = opt.ToString()
			});
		}
	}
}
