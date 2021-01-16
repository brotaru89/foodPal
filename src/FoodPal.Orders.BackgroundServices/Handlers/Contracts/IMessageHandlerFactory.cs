namespace FoodPal.Orders.BackgroundServices.Handlers.Contracts
{
	public interface IMessageHandlerFactory
	{
		IMessageHandler Get(string messageType);
	}
}
