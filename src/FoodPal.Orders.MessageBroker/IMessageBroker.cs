using System.Threading.Tasks;

namespace FoodPal.Orders.MessageBroker
{
	public interface IMessageBroker
	{
		Task SendMessageAsync(string queueName, MessageBrokerEnvelope messageEnvelope);

		Task<MessageBrokerEnvelope> ReceiveMessageAsync(string queueName);
	}
}
