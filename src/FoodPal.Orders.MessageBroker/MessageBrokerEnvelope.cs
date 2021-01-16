using System;

namespace FoodPal.Orders.MessageBroker
{
	public class MessageBrokerEnvelope
	{
		public string RequestId { get; set; }

		public string MessageType { get; set; }

		public object Data { get; set; }

		public MessageBrokerEnvelope() { }

		public MessageBrokerEnvelope(string messageType, object payload) : this(messageType, payload, Guid.NewGuid()) { }

		public MessageBrokerEnvelope(string messageType, object payload, Guid requestId)
		{
			MessageType = messageType;
			Data = payload;
			RequestId = requestId.ToString();
		}
	}
}