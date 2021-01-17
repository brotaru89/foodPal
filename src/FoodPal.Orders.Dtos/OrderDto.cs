using System;
using System.Collections.Generic;

namespace FoodPal.Orders.Dtos
{
	/// <summary>
	/// 
	/// </summary>
	public class OrderDto
	{
		/// <summary>
		/// Customer identifier
		/// </summary>
		public string CustomerId { get; set; }

		/// <summary>
		/// Customer name.
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// Customer email.
		/// </summary>
		public string CustomerEmail { get; set; }

		/// <summary>
		/// Collection of order items.
		/// </summary>
		public IEnumerable<OrderItemDto> Items { get; set; }

		/// <summary>
		/// Order delivery details.
		/// </summary>
		public DeliveryDetailsDto DeliveryDetails { get; set; }

		/// <summary>
		/// Order status.
		/// </summary>
		public OrderStatusDto OrderStatus { get; set; }

		/// <summary>
		/// Additional item comments.
		/// </summary>
		public string Comments { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime LastUpdatedAt { get; set; }
	}
}
