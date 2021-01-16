using FoodPal.Orders.MessageBroker;
using System;

namespace FoodPal.Orders.BackgroundServices.Handlers
{
	public class BaseMessageHandler
	{
		protected TPayload GetEnvelopePayload<TPayload>(MessageBrokerEnvelope messageEnvelope)
		{
			if (messageEnvelope.Data is not TPayload payload)
			{
				throw new ArgumentException($"'{this.GetType().Name}' cannot hanle payloads of type {messageEnvelope.Data.GetType().Name}");
			}

			return payload;
		}
	}
}
