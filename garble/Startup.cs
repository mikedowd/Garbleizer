﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen;

namespace garble
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public IConfigurationRoot Configuration { get; set; }

		private string pathToDoc =
			"C:\\etc\\Garbleizer\\artifacts\\bin\\garble\\Debug\\dnx451\\garble.xml";
		// This method gets called by the runtime. Use this method to add services to the container
		public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

			services.AddSwaggerGen();

			services.ConfigureSwaggerDocument( options =>
			{
				options.SingleApiVersion( new Info
				{
					Version = "v1",
					Title = "garble API",
					Description = "A simple api to garble text",
					TermsOfService = "None"
				} );
				options.OperationFilter( new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlActionComments( pathToDoc ) );
			} );

			services.ConfigureSwaggerSchema( options =>
			{
				options.DescribeAllEnumsAsStrings = true;
				options.ModelFilter( new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlTypeComments( pathToDoc ) );
			} );
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc();

			app.UseSwaggerGen();
			app.UseSwaggerUi();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
