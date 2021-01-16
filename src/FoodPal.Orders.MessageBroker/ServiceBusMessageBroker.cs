using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FoodPal.Orders.MessageBroker
{
	public class ServiceBusMessageBroker : IMessageBroker
	{
		private readonly string _messageBrokerEndpoint;

		public RetryPolicy DefaultRetryPolicy => new RetryExponential(new TimeSpan(0), new TimeSpan(0, 0, 5), 5);

		public ServiceBusMessageBroker(IOptions<MessageBrokerConnectionSettings> connectionSettings)
		{
			_messageBrokerEndpoint = connectionSettings.Value.Endpoint;
		}

		public async Task SendMessageAsync(string queueName, MessageBrokerEnvelope messageEnvelope)
		{
			var queueClient = new QueueClient(_messageBrokerEndpoint, queueName, ReceiveMode.ReceiveAndDelete, DefaultRetryPolicy);
			await queueClient.SendAsync(CreateMessage(messageEnvelope));
		}

		//public async Task StartListen()
		//{

		//}

		//public void StopListener()
		//{

		//}

		public async Task<MessageBrokerEnvelope> ReceiveMessageAsync(string queueName)
		{
			var messageReceiver = new MessageReceiver(_messageBrokerEndpoint, queueName, ReceiveMode.ReceiveAndDelete, DefaultRetryPolicy);
			var rawMessage = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(2));

			if (rawMessage != null)
			{
				try
				{
					var payload = DeserializeMessage<MessageBrokerEnvelope>(rawMessage);
					return payload;
				}
				catch (Exception)
				{
					await messageReceiver.AbandonAsync(rawMessage.SystemProperties.LockToken);
					throw;
				}
			}
			else
			{
				return default(MessageBrokerEnvelope);
			}
		}

		private static Message CreateMessage(MessageBrokerEnvelope messageEnvelope)
		{
			var payload = JsonConvert.SerializeObject(messageEnvelope, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
			return new Message(Encoding.UTF8.GetBytes(payload));
		}

		private static TPayload DeserializeMessage<TPayload>(Message message)
		{
			try
			{
				var strMsg = Encoding.UTF8.GetString(message.Body);
				var payload = JsonConvert.DeserializeObject<TPayload>(strMsg, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
				return payload;
			}
			catch (Exception ex)
			{
				throw new Exception("Deserialization failed on message broker envelope.", ex);
			}
		}
	}
}
