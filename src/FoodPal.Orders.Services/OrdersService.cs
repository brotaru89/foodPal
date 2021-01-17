using AutoMapper;
using FluentValidation;
using FoodPal.Orders.Contracts;
using FoodPal.Orders.Data;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.Enums;
using FoodPal.Orders.Exceptions;
using FoodPal.Orders.MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Orders.Services
{
	public class OrdersService : BaseService, IOrdersService
	{
		private readonly IMessageBroker _messageBroker;
		private readonly IValidator<NewOrderDto> _newOrderValidator;
		private readonly IOrdersUnitOfWork _orderUoW;
		private readonly IMapper _mapper;

		public OrdersService(IMessageBroker messageBroker, IValidator<NewOrderDto> newOrderValidator, IOrdersUnitOfWork unitOfWork, IMapper mapper)
		{
			_messageBroker = messageBroker;
			_newOrderValidator = newOrderValidator;
			_orderUoW = unitOfWork;
			_mapper = mapper;
		}

		public async Task<string> Create(NewOrderDto newOrder)
		{
			ValidateNewOrder(newOrder);

			var payload = new MessageBrokerEnvelope(MessageTypes.NewOrder, newOrder);

			await _messageBroker.SendMessageAsync(QueueNames.NewOrder, payload);

			return payload.RequestId;
		}

		public async Task<OrderDto> GetByIdAsync(int orderId)
		{
			ParameterChecks(new (Func<bool>, Exception)[]
			{
				( () => orderId > 0, new ArgumentOutOfRangeException(nameof(orderId), $"{nameof(orderId)} must be positive")),
			});

			var order = await _orderUoW.OrdersRepository.GetByIdAsync(orderId);

			if (order is null) throw new FoodPalNotFoundException(orderId.ToString());

			return _mapper.Map<OrderDto>(order);
		}

		public async Task<OrderStatusDto> GetStatusAsync(int orderId)
		{
			ParameterChecks(new (Func<bool>, Exception)[]
			{
				( () => orderId > 0, new ArgumentOutOfRangeException(nameof(orderId), $"{nameof(orderId)} must be positive")),
			});

			var orderStatus = await _orderUoW.OrdersRepository.GetStatusAsync(orderId);

			if (!orderStatus.HasValue) throw new FoodPalNotFoundException(orderId.ToString());

			return _mapper.Map<OrderStatusDto>(orderStatus);
		}

		public async Task<IEnumerable<OrderDto>> GetByFiltersAsync(string customerId, OrderStatus? status, int page, int pageSize)
		{
			ParameterChecks(new (Func<bool>, Exception)[]
			{
				( () => page > 0, new ArgumentOutOfRangeException(nameof(page), $"{nameof(page)} must be positive")),
				( () => pageSize > 0, new ArgumentOutOfRangeException(nameof(pageSize), $"{nameof(pageSize)} must be positive")),
			});

			var orders = await _orderUoW.OrdersRepository.GetByFiltersAsync(customerId, status, page - 1, pageSize);

			return orders.Any() ? _mapper.Map<List<OrderDto>>(orders) : new List<OrderDto>();
		}

		#region Private Methods

		private void ValidateNewOrder(NewOrderDto newOrder)
		{
			var failures = _newOrderValidator
					.Validate(newOrder).Errors
					.Where(error => error != null)
					.Select(error => error.ToString())
					.ToList();

			if (failures.Any())
				throw new FoodPalBadParamsException(failures);
		}

		#endregion
	}
}
