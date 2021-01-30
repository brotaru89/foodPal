using System.Threading.Tasks;

namespace FoodPal.Orders.MessageBroker
{
	public delegate Task MessageReceivedEventHandler(string queueMessage);

	public interface IMessageBroker
	{
		Task SendMessageAsync<TMessage>(string queueName, TMessage messageEnvelope);

		void RegisterMessageReceiver<TMessageType>(string queueName, MessageReceivedEventHandler messageHandler);

		Task StartListenerAsync();

		Task StopListenerAsync();
	}
}
