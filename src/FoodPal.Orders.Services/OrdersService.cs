using FluentValidation;
using FoodPal.Orders.Contracts;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.Exceptions;
using FoodPal.Orders.MessageBroker;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Orders.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly IMessageBroker _messageBroker;
		private readonly IValidator<NewOrderDto> _newOrderValidator;

		public OrdersService(IMessageBroker messageBroker, IValidator<NewOrderDto> newOrderValidator)
		{
			_messageBroker = messageBroker;
			_newOrderValidator = newOrderValidator;
		}

		public async Task<string> Create(NewOrderDto newOrder)
		{
			ValidateNewOrder(newOrder);

			var payload = new MessageBrokerEnvelope(MessageTypes.NewOrder, newOrder);

			await _messageBroker.SendMessageAsync(QueueNames.NewOrder, payload);

			return payload.RequestId;
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
