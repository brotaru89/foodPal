using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;

namespace FoodPal.Orders.Mappers
{
	public class OrderStatusProfile : AbstractProfile
	{
		public OrderStatusProfile()
		{
			CreateMap<OrderStatus, StatusDto>().ConvertUsing(opt => new StatusDto
			{
				StatusId = (int)opt,
				StatusName = opt.ToString()
			});

			CreateMap<OrderItemStatus, StatusDto>().ConvertUsing(opt => new StatusDto
			{
				StatusId = (int)opt,
				StatusName = opt.ToString()
			});
		}
	}
}
