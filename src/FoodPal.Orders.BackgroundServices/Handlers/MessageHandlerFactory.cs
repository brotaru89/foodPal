using FoodPal.Orders.BackgroundServices.Handlers.Contracts;
using FoodPal.Orders.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FoodPal.Orders.BackgroundServices.Handlers
{
	public class MessageHandlerFactory : IMessageHandlerFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public MessageHandlerFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IMessageHandler Get(string messageType)
		{
			return messageType switch
			{
				MessageTypes.NewOrder => _serviceProvider.GetRequiredService<NewOrderMessageHandler>(),
				_ => throw new Exception(),
			};
		}
	}
}
