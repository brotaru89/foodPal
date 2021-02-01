using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.MessageBroker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundWorkers.Workers
{
	public class NewOrderWorker : IHostedService
	{
		private readonly ILogger<NewOrderWorker> _logger;
		private readonly IMessageBroker _messageBroker;
		private readonly IMessageHandlerFactory _messageHandlerFactory;
		private readonly IQueueNameProvider _queueNameProvider;

		public NewOrderWorker(ILogger<NewOrderWorker> logger, IMessageBroker messageBroker, IMessageHandlerFactory messageHandlerFactory, IQueueNameProvider queueNameProvider)
		{
			_logger = logger;
			_messageBroker = messageBroker;
			_messageHandlerFactory = messageHandlerFactory;
			_queueNameProvider = queueNameProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"{this.GetType().Name} starting; registering message handler.");
			_messageBroker.RegisterMessageReceiver(_queueNameProvider.GetNewOrderQueueName(), HandleNextMessageAsync);
			await _messageBroker.StartListenerAsync();
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"{this.GetType().Name} stopping.");
			await _messageBroker.StopListenerAsync();
		}

		private async Task HandleNextMessageAsync(string messageEnvelopeAsString)
		{
			try
			{
				var payload = JsonConvert.DeserializeObject<MessageBrokerEnvelope<NewOrderDto>>(messageEnvelopeAsString);
				var handler = _messageHandlerFactory.Get(MessageTypes.NewOrder);
				await handler.ExecuteAsync(payload);
			}
			catch (Exception ex)
			{
				throw new Exception("Message processing failed.", ex);
			}
		}
	}
}
