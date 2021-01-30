using AutoMapper;
using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.Data;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;
using FoodPal.Orders.MessageBroker;
using FoodPal.Orders.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundServices.Handlers
{
	public class NewOrderMessageHandler : BaseMessageHandler, IMessageHandler
	{
		private readonly ILogger<NewOrderMessageHandler> _logger;
		private readonly IMapper _mapper;
		private readonly IOrdersUnitOfWork _orderUoW;
		private readonly IQueueNameProvider _queueNameProvider;
		private readonly IMessageBroker _messageBroker;

		public NewOrderMessageHandler(ILogger<NewOrderMessageHandler> logger, IMapper mapper, IOrdersUnitOfWork unitOfWork, IQueueNameProvider queueNameProvider, IMessageBroker messageBroker)
		{
			_logger = logger;
			_mapper = mapper;
			_orderUoW = unitOfWork;
			_queueNameProvider = queueNameProvider;
			_messageBroker = messageBroker;
		}

		public async Task ExecuteAsync(MessageBrokerEnvelope messageEnvelope)
		{
			var payload = GetEnvelopePayload<NewOrderDto>(messageEnvelope);
			var persistedOrder = await SaveNewOrderAsync(payload);

			var grouppedOrderItems = persistedOrder.Items.GroupBy(x => x.ProviderId).ToDictionary(k => k.Key, v => v.ToList());

			foreach (var providerItems in grouppedOrderItems)
			{
				switch (providerItems.Key.ToLower())
				{
					case "xyz":
					case "kfc":
						await SendOrderRequestToProviderAsync(providerItems.Key.ToLower(), providerItems.Value);
						await UpdateOrderItemsStatus(providerItems.Value);
						break;

					default:
						throw new Exception("to do");
				}
			}
		}

		private async Task<Order> SaveNewOrderAsync(NewOrderDto newOrderDto)
		{
			var newOrderModel = _mapper.Map<Order>(newOrderDto);

			newOrderModel.CreatedAt = newOrderModel.LastUpdatedAt = DateTime.Now;
			newOrderModel.Status = OrderStatus.New;

			foreach (var orderItem in newOrderModel.Items)
			{
				orderItem.Status = OrderItemStatus.New;
			}

			return await _orderUoW.OrdersRepository.CreateAsync(newOrderModel);
		}

		private async Task UpdateOrderItemsStatus(List<OrderItem> orderItems)
		{
			foreach (var orderItem in orderItems)
			{
				await _orderUoW.OrderItemsRepository.UpdateStatusAsync(orderItem, OrderItemStatus.InProgress);
			}
		}

		private async Task SendOrderRequestToProviderAsync(string providerId, List<OrderItem> orderItems)
		{
			var messageContent = orderItems.Select(x => new ExternalOrderLine { Name = x.Name, Quantity = x.Quantity }).ToList();
			var payload = new ExternalMessageBrokerEnvelope(messageContent);

			await _messageBroker.SendMessageAsync(_queueNameProvider.GetProviderRequestQueueName(providerId), payload);
		}
	}

	class ExternalOrderLine
	{
		public string Name { get; set; }
		public short Quantity { get; set; }
	}
}
