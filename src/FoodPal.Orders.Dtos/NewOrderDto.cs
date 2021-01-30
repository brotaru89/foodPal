using System.Collections.Generic;

namespace FoodPal.Orders.Dtos
{
	/// <summary>
	/// Order data transfer object.
	/// </summary>
	public class NewOrderDto
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
		public IEnumerable<NewOrderItemDto> Items { get; set; }

		/// <summary>
		/// Order delivery details.
		/// </summary>
		public DeliveryDetailsDto DeliveryDetails { get; set; }

		/// <summary>
		/// Additional item comments.
		/// </summary>
		public string Comments { get; set; }
	}
}
