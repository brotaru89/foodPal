namespace FoodPal.Orders.MessageBroker
{
	public interface IQueueNameProvider
	{
		string GetNewOrderQueueName();

		string GetProviderRequestQueueName(string providerId);

		string GetProviderResponseQueueName(string providerId);
	}
}
