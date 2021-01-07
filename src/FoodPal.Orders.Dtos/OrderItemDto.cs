namespace FoodPal.Orders.Dtos
{
	/// <summary>
	/// Order item data transfer object.
	/// </summary>
	public class OrderItemDto
	{
		/// <summary>
		/// Item name.
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Item identifier.
		/// </summary>
		public string ItemId { get; set; }
		
		/// <summary>
		/// Item provider identifier.
		/// </summary>
		public string ProviderId { get; set; }
		
		/// <summary>
		/// Quantity, no. of same items.
		/// </summary>
		public short Quantity { get; set; }
		
		/// <summary>
		/// Price per item.
		/// </summary>
		public decimal Price { get; set; }
		
		/// <summary>
		/// Total price (price per item multiplied with quantity).
		/// </summary>
		public decimal TotalPrice => Price * Quantity;
	}
}
