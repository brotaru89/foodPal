namespace FoodPal.Orders.Dtos
{
	public class HttpExternalOrderItemRequestDto
	{
		public int OrderItemId { get; set; }
		public string Name { get; set; }
		public short Quantity { get; set; }
		public string CallbackEndpoint { get; set; }
	}
}
