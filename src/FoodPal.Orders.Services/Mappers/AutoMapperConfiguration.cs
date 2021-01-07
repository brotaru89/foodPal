using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace FoodPal.Orders.Services.Mappers
{
	public class AutoMapperConfiguration
	{
		public static void ConfigureAutoMapperProfiles(IMapperConfigurationExpression configuration)
		{
			var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
			foreach (var profile in profiles)
			{
				configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
			}
		}
	}
}
