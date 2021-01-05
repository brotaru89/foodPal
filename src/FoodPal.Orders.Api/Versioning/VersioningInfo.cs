using System.Reflection;

namespace FoodPal.Orders.Api.Versioning
{
	public class VersioningInfo
	{
		private const int DefaultVersionNumber = 0;

		public static int MajorVersion => Assembly.GetExecutingAssembly().GetName().Version?.Major ?? DefaultVersionNumber;
	}
}
