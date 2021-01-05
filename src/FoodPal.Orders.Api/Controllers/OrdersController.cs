using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodPal.Orders.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly ILogger<OrdersController> _logger;

		public OrdersController(ILogger<OrdersController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<string>> Get()
		{
			return Ok("I'm here!");
		}
	}
}
