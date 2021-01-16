using FoodPal.Orders.Dtos;
using FoodPal.Orders.Models;

namespace FoodPal.Orders.BackgroundServices.Mappers
{
	internal class DeliveryDetailsProfile : AbstractProfile
	{
		public DeliveryDetailsProfile()
		{
			CreateMap<DeliveryDetailsDto, DeliveryDetails>();
		}
	}
}
