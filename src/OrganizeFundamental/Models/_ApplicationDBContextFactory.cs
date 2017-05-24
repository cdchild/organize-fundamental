using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OrganizeFundamental.Models
{
	public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
	{
		internal DbContextOptions<ApplicationDbContext> _contextOptions;

		/// <summary>
		/// Constructor ONLY for Entity Framework Tools and Other External Programs that want to create a factory using the settings from the appsettings.json file.
		/// Everything should use the built-in dependancy injection friendly constructor.
		/// </summary>
		public ApplicationDbContextFactory()
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
				.AddJsonFile("appsettings.json")
				.Build();

			Initialize(config.GetConnectionString("DefaultConnection"));
		}

		public ApplicationDbContextFactory(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(connectionString);
			_contextOptions = optionsBuilder.Options;
		}

		internal void Initialize(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(connectionString);
			_contextOptions = optionsBuilder.Options;
		}

		/// <summary>
		/// For external use only, hopefully only Entity Framework Tools.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public ApplicationDbContext Create(DbContextFactoryOptions options = null) => Create();

		public ApplicationDbContext Create() => new ApplicationDbContext(_contextOptions);
	}
}