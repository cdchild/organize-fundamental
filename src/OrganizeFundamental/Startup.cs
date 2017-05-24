using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrganizeFundamental.Models;
using OrganizeFundamental.Services;

namespace OrganizeFundamental
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				// This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
				builder.AddApplicationInsightsSettings(developerMode: true);
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			// Add framework services.
			services.AddApplicationInsightsTelemetry(Configuration);

			// Add application services.
			services.AddSingleton(p => new ApplicationDbContextFactory(connectionString));
			services.AddSingleton(o => o.GetService<ApplicationDbContextFactory>().Create());
			services.AddTransient<IEmailSender, MessageServices>();
			services.AddTransient<ISmsSender, MessageServices>();

			services.AddIdentity<ApplicationUser, IdentityRole>(config =>
			{
				config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";

				config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-.";
				config.User.RequireUniqueEmail = false;

				config.Password.RequireDigit = true;
				config.Password.RequiredLength = 12;
				config.Password.RequireLowercase = true;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = true;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContextFactory dbContextFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Warning);

			app.UseApplicationInsightsRequestTelemetry();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
				app.UseDeveloperExceptionPage();

			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseStaticFiles();

			app.UseIdentity();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
