using System;

namespace FoodPal.Orders.MessageBroker
{
	public class ExternalMessageBrokerEnvelope
	{
		public string RequestId { get; set; }

		public object Data { get; set; }

		public ExternalMessageBrokerEnvelope() { }

		public ExternalMessageBrokerEnvelope(object payload) : this(payload, Guid.NewGuid()) { }

		public ExternalMessageBrokerEnvelope(object payload, Guid requestId)
		{
			Data = payload;
			RequestId = requestId.ToString();
		}
	}
}