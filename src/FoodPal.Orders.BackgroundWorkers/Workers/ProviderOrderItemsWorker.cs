using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.MessageBroker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundWorkers.Workers
{
	public class ProviderOrderItemsWorker : IHostedService
	{
		private readonly ILogger<ProviderOrderItemsWorker> _logger;
		private readonly IMessageBroker _messageBroker;
		private readonly IMessageHandlerFactory _messageHandlerFactory;
		private readonly IQueueNameProvider _queueNameProvider;

		public ProviderOrderItemsWorker(ILogger<ProviderOrderItemsWorker> logger, IMessageBroker messageBroker, IMessageHandlerFactory messageHandlerFactory, IQueueNameProvider queueNameProvider)
		{
			_logger = logger;
			_messageBroker = messageBroker;
			_messageHandlerFactory = messageHandlerFactory;
			_queueNameProvider = queueNameProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"{this.GetType().Name} starting; registering message handler.");
			_messageBroker.RegisterMessageReceiver<MessageBrokerEnvelope>(_queueNameProvider.GetNewOrderQueueName(), HandleNextMessageAsync);
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
				var payload = JsonConvert.DeserializeObject<MessageBrokerEnvelope>(messageEnvelopeAsString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
				var handler = _messageHandlerFactory.Get(MessageTypes.OrderItemsProcessedByProvider);
				await handler.ExecuteAsync(payload);
			}
			catch (Exception ex)
			{
				throw new Exception("Message processing failed.", ex);
			}
		}
	}
}
