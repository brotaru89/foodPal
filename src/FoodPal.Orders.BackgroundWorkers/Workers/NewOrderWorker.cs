using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.Dtos;
using FoodPal.Orders.MessageBroker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundWorker.Workers
{
	public class NewOrderWorker : BackgroundService
	{
		private readonly ILogger<NewOrderWorker> _logger;
		private readonly IMessageBroker _messageBroker;
		private readonly IMessageHandlerFactory _messageHandlerFactory;

		public NewOrderWorker(ILogger<NewOrderWorker> logger, IMessageBroker messageBroker, IMessageHandlerFactory messageHandlerFactory)
		{
			_logger = logger;
			_messageBroker = messageBroker;
			_messageHandlerFactory = messageHandlerFactory;
		}

		//public Task StartAsync(CancellationToken cancellationToken)
		//{
		//	_logger.LogDebug($"{this.GetType().Name} starting; registering message handler.");
		//}

		//public async Task StopAsync(CancellationToken cancellationToken)
		//{
		//	_logger.LogDebug($"{this.GetType().Name} stopping.");
		//	await this.queueClient.CloseAsync();
		//}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogDebug($"{this.GetType().Name} is starting.");
			stoppingToken.Register(() => _logger.LogDebug($"Background task is stopping."));

			do
			{
				if (stoppingToken.IsCancellationRequested)
					break;

				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				
				await HandleNextMessageAsync();

				await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
			} while (true);

			//while (!stoppingToken.IsCancellationRequested)
			//{
			//	_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			//}

			_logger.LogDebug($"Background task is stopping.");
		}

		private async Task HandleNextMessageAsync()
		{
			var message = await _messageBroker.ReceiveMessageAsync(QueueNames.NewOrder);
			
			if (message is not null)
			{
				try
				{
					var handler = _messageHandlerFactory.Get(MessageTypes.NewOrder);
					await handler.ExecuteAsync(message);
				}
				catch(Exception ex)
				{
					throw new Exception("Message processing failed.", ex);
				}
			}
		}
	}
}
