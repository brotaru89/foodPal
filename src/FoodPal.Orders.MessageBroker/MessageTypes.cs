namespace FoodPal.Orders.MessageBroker
{
	public static class MessageTypes
	{
		public const string NewOrder = "new-order";
		public const string OrderItemsProcessedByProvider = "order-items-processed";

		public const string ProviderNewOrder = "provider-new-order";
	}
}
