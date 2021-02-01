using System.Collections.Generic;

namespace FoodPal.Orders.Dtos
{
	public class HttpExternalOrderRequestDto
	{
		public int OrderId { get; set; }
		public List<HttpExternalOrderItemRequestDto> OrderItems { get; set; }
	}
}
