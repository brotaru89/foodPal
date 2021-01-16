using FoodPal.Orders.Contracts;
using FoodPal.Orders.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodPal.Orders.Api.Controllers
{
	/// <summary>
	/// Providers API methods for handling order requests.
	/// </summary>
	public class OrdersController : ApiBaseController
	{
		private readonly IOrdersService _ordersService;

		/// <summary>
		/// Constructor for Orders controller.
		/// </summary>
		public OrdersController(IOrdersService ordersService)
		{
			_ordersService = ordersService;
		}

		/// <summary>
		/// Returns a paged list of orders.
		/// </summary>
		/// <param name="page">Current result page no.</param>
		/// <param name="pageSize">No. of returned records per page.</param>
		/// <returns>A paginated collection of orders, sorted by last modified date - the most recently updated will be first in the result set.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(PagedResultSetDto<NewOrderDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesErrorResponseType(typeof(ErrorInfoDto))]
		public async Task<ActionResult<string>> GetOrders(int page = 1, int pageSize = 20)
		{
			return Ok("I'm here!");
		}

		/// <summary>
		/// Returns the specified order, if exists.
		/// </summary>
		/// <param name="orderId">The order identifier.</param>
		/// <returns>An object containing the order details.</returns>
		[HttpGet]
		[Route("{orderId}")]
		[ProducesResponseType(typeof(NewOrderDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesErrorResponseType(typeof(ErrorInfoDto))]
		public async Task<ActionResult<string>> GetOrderById(int orderId)
		{
			return Ok("I'm here!");
		}

		/// <summary>
		/// Creates an order.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesErrorResponseType(typeof(ErrorInfoDto))]
		public async Task<ActionResult<string>> CreateOrder(NewOrderDto newOrder)
		{
			var requestId = await _ordersService.Create(newOrder);
			return Accepted(requestId);
		}
	}
}
