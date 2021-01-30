using System;

namespace FoodPal.Orders.MessageBroker
{
	public class QueueNameProvider : IQueueNameProvider
	{
		private readonly string _newOrderQueueName = "new-orders";
		private readonly string _providerRequestQueueNameTemplate = "provider";

		public QueueNameProvider(string prefix)
		{
			if (!string.IsNullOrEmpty(prefix))
			{
				_newOrderQueueName = $"{prefix}-{_newOrderQueueName}";
				_providerRequestQueueNameTemplate = $"{prefix}-{_providerRequestQueueNameTemplate}";
			}
		}

		public string GetNewOrderQueueName() => _newOrderQueueName;

		public string GetProviderRequestQueueName(string providerId)
		{
			if (string.IsNullOrEmpty(providerId)) throw new ArgumentException(nameof(providerId));

			return $"{_providerRequestQueueNameTemplate}-{providerId}-request";
		}

		public string GetProviderResponseQueueName(string providerId)
		{
			if (string.IsNullOrEmpty(providerId)) throw new ArgumentException(nameof(providerId));

			return $"{_providerRequestQueueNameTemplate}-{providerId}-response";
		}
	}
}
