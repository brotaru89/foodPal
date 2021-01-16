namespace FoodPal.Orders.Dtos
{
	/// <summary>
	/// Order item data transfer object.
	/// </summary>
	public class NewOrderItemDto
	{
		/// <summary>
		/// Item name.
		/// </summary>
		public string Name { get; set; }
		
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
	}
}
