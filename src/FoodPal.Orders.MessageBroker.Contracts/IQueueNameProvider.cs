namespace FoodPal.Orders.MessageBroker.Contracts
{
	public interface IQueueNameProvider
	{
		string GetNewOrderQueueName();

		string GetProviderRequestQueueName(string providerId);

		string GetProviderResponseQueueName(string providerId);
	}
}
