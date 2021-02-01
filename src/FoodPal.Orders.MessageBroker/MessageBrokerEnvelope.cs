﻿using System;

namespace FoodPal.Orders.MessageBroker
{
	public class MessageBrokerEnvelope<TPayload>
	{
		public string RequestId { get; set; }

		public string MessageType { get; set; }

		public TPayload Data { get; set; }

		public MessageBrokerEnvelope() { }

		public MessageBrokerEnvelope(string messageType, TPayload payload) : this(messageType, payload, Guid.NewGuid()) { }

		public MessageBrokerEnvelope(string messageType, TPayload payload, Guid requestId)
		{
			MessageType = messageType;
			Data = payload;
			RequestId = requestId.ToString();
		}
	}
}