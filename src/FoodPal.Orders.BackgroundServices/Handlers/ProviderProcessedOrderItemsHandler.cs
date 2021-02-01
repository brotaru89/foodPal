using AutoMapper;
using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.Data;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;
using FoodPal.Orders.MessageBroker;
using FoodPal.Orders.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundServices.Handlers
{
	public class ProviderProcessedOrderItemsHandler : BaseMessageHandler, IMessageHandler
	{
		private readonly ILogger<ProviderProcessedOrderItemsHandler> _logger;
		private readonly IMapper _mapper;
		private readonly IOrdersUnitOfWork _orderUoW;

		public ProviderProcessedOrderItemsHandler(ILogger<ProviderProcessedOrderItemsHandler> logger, IMapper mapper, IOrdersUnitOfWork unitOfWork)
		{
			_logger = logger;
			_mapper = mapper;
			_orderUoW = unitOfWork;
		}

		public async Task ExecuteAsync<TPayload>(MessageBrokerEnvelope<TPayload> messageEnvelope)
		{
			var payload = GetEnvelopePayload<TPayload, MessageBrokerExternalOrderResponseDto>(messageEnvelope);

			var order = await _orderUoW.OrdersRepository.GetByIdAsync(payload.OrderId);
			var orderItem = await _orderUoW.OrderItemsRepository.GetItemsAsync(payload.OrderId);
			
			await UpdateOrderItemsStatus(order.Items);

			var updatedOrder = await _orderUoW.OrdersRepository.GetByIdAsync(payload.OrderId);

			if (updatedOrder.Items.All(x => x.Status == OrderItemStatus.Ready))
				await _orderUoW.OrdersRepository.UpdateStatusAsync(updatedOrder, OrderStatus.ReadyForPickup);
		}

		private async Task UpdateOrderItemsStatus(List<OrderItem> orderItems)
		{
			foreach (var orderItem in orderItems)
			{
				await _orderUoW.OrderItemsRepository.UpdateStatusAsync(orderItem, OrderItemStatus.Ready);
			}
		}
	}
}
