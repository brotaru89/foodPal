using AutoMapper;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.Models;

namespace FoodPal.Orders.Services.Mappers
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<NewOrderDto, Order>();
		}
	}
}
