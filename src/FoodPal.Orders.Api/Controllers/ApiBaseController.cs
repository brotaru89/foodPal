using Microsoft.AspNetCore.Mvc;

namespace FoodPal.Orders.Api.Controllers
{
	/// <summary>
	/// Base controller class for TDS
	/// </summary>
	[ApiController]
	[Produces("application/json")]
	[Route("v{version:apiVersion}/[controller]")]
	public class ApiBaseController : ControllerBase
	{
	}
}
