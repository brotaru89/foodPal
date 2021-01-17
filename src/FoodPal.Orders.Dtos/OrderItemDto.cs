namespace FoodPal.Orders.Dtos
{
	/// <summary>
	/// 
	/// </summary>
	public class OrderItemDto
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

		/// <summary>
		/// 
		/// </summary>
		public decimal TotalPrice => Quantity * Price;
	}
}
