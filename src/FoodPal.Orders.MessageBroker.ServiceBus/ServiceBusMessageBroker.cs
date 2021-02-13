using Azure.Messaging.ServiceBus;
using FoodPal.Orders.MessageBroker.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FoodPal.Orders.MessageBroker.ServiceBus
{
	public class ServiceBusMessageBroker : IMessageBroker
	{
		private readonly string _messageBrokerEndpoint;
		private ServiceBusClient _sbMessageReceiverClient;
		private ServiceBusProcessor _sbProcessor;
		private MessageReceivedEventHandler _messageHandler;

		public ServiceBusMessageBroker(IOptions<MessageBrokerConnectionSettings> connectionSettings)
		{
			_messageBrokerEndpoint = connectionSettings.Value.Endpoint;
		}

		public async Task SendMessageAsync<TMessage>(string queueName, TMessage messageEnvelope)
		{
			await using (var sbClient = new ServiceBusClient(_messageBrokerEndpoint))
			{
				var sender = sbClient.CreateSender(queueName);
				var message = CreateMessage(messageEnvelope);

				await sender.SendMessageAsync(message);
			}
		}

		public void RegisterMessageReceiver(string queueName, MessageReceivedEventHandler messageHandler)
		{
			_messageHandler = messageHandler;
			_sbMessageReceiverClient = new ServiceBusClient(_messageBrokerEndpoint);
			_sbProcessor = _sbMessageReceiverClient.CreateProcessor(queueName, new ServiceBusProcessorOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

			_sbProcessor.ProcessMessageAsync += MessageHandlerAsync;
			_sbProcessor.ProcessErrorAsync += ErrorHandlerAsync;
		}

		public async Task StartListenerAsync()
		{
			await _sbProcessor.StartProcessingAsync();
		}

		public async Task StopListenerAsync()
		{
			await _sbProcessor.StopProcessingAsync();
			await _sbMessageReceiverClient.DisposeAsync();
		}

		#region Private methods

		private static ServiceBusMessage CreateMessage<T>(T messageEnvelope)
		{
			var payload = JsonConvert.SerializeObject(messageEnvelope/*, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }*/);
			return new ServiceBusMessage(payload);
		}

		private async Task MessageHandlerAsync(ProcessMessageEventArgs args)
		{
			var strMessage = args.Message.Body.ToString();

			await _messageHandler(strMessage);

			await args.CompleteMessageAsync(args.Message);
		}

		private async Task ErrorHandlerAsync(ProcessErrorEventArgs args)
		{
			throw new Exception("Error occurred in ServiceBus message handler.", args.Exception);
		}

		#endregion
	}
}
