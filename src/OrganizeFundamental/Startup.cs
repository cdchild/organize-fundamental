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
				// For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets();

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

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

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

			// Add application services.
			services.AddSingleton(o => new ApplicationDbContextFactory(connectionString));
			services.AddTransient<IEmailSender, MessageServices>();
			services.AddTransient<ISmsSender, MessageServices>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContextFactory dbContextFactory)
		{
			bool isDatabaseConsideredCreated;

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Warning);

			app.UseApplicationInsightsRequestTelemetry();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
				app.UseDeveloperExceptionPage();

				try
				{
					using (var db = dbContextFactory.Create())
					{
						isDatabaseConsideredCreated = db.Database.EnsureCreated();
					}
				}
				catch (System.Exception ex)
				{
					throw new System.Exception("Database Context Error", ex);
				}
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
