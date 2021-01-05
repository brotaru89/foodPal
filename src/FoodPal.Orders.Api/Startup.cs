using FoodPal.Orders.Api.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FoodPal.Orders.Api
{
	public class Startup
	{
		private readonly int _majorVersion;
		private readonly string _majorVersionString;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			_majorVersion = VersioningInfo.MajorVersion;
			_majorVersionString = $"v{_majorVersion}";
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			// Register API versioning
			services.AddApiVersioning(options =>
			{
				// reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
				options.ReportApiVersions = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
				options.DefaultApiVersion = new ApiVersion(_majorVersion, 0);
			});

			// Register Version API explorer
			services.AddVersionedApiExplorer(options =>
			{
				options.DefaultApiVersion = new ApiVersion(_majorVersion, 0);

				// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
				// note: the specified format code will format the version as "'v'major[.minor][-status]"
				options.GroupNameFormat = "'v'VVV";

				// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
				// can also be used to control the format of the API version in route templates
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(_majorVersionString, new OpenApiInfo
				{
					Version = _majorVersionString,
					Title = "FoodPal - Orders API",
					Description = "FoodPal - Orders API microservice"
				});

				// Set the comments path for the Swagger JSON and UI.
				c.IncludeXmlComments(GetXmlCommentsFilePath(), includeControllerXmlComments: true);
				c.IncludeXmlComments(GetDtosXmlCommentsFilePath());
			});
		}

		/// <summary>
		/// Docs path
		/// </summary>
		/// <returns></returns>
		protected static string GetXmlCommentsFilePath()
		{
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			return xmlPath;
		}

		/// <summary>
		/// DTOs doc path
		/// </summary>
		/// <returns></returns>
		protected static string GetDtosXmlCommentsFilePath()
		{
			// TODO: Investigate if this can be made generic and for multiple libraries
			var xmlFile = $"FoodPal.Orders.Dtos.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			return xmlPath;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			logger.LogInformation("Environment: {Environment}", env.EnvironmentName);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// HTTPS Redirection Middleware redirects HTTP requests to HTTPS.
			app.UseHttpsRedirection();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint($"/swagger/{_majorVersionString}/swagger.json", $"Orders API {_majorVersionString}");
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
