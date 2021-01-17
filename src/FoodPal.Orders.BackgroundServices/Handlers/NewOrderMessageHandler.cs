using AutoMapper;
using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.Data;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.MessageBroker;
using FoodPal.Orders.Models;
using System;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundServices.Handlers
{
	public class NewOrderMessageHandler : BaseMessageHandler, IMessageHandler
	{
		private readonly IMapper _mapper;
		private readonly IOrdersUnitOfWork _orderUoW;

		public NewOrderMessageHandler(IMapper mapper, IOrdersUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_orderUoW = unitOfWork;
		}

		public async Task ExecuteAsync(MessageBrokerEnvelope messageEnvelope)
		{
			var payload = GetEnvelopePayload<NewOrderDto>(messageEnvelope);
			var newOrder = _mapper.Map<Order>(payload);

			newOrder.CreatedAt = newOrder.LastUpdatedAt = DateTime.Now;

			var createdOrder = await _orderUoW.OrdersRepository.CreateAsync(newOrder);
		}
	}
}
