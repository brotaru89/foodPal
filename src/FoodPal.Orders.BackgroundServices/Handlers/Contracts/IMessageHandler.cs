using FoodPal.Orders.MessageBroker;
using System.Threading.Tasks;

namespace FoodPal.Orders.BackgroundServices.Handlers.Contracts
{
	public interface IMessageHandler
	{
		Task ExecuteAsync(MessageBrokerEnvelope messageEnvelope);
	}
}
