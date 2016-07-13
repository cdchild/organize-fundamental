using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OrganizeFundamental.Models
{
	public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
	{
		readonly DbContextOptions<ApplicationDbContext> _contextOptions;
		public ApplicationDbContextFactory(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(connectionString);
			_contextOptions = optionsBuilder.Options;
		}

		public ApplicationDbContext Create(DbContextFactoryOptions options = null) => new ApplicationDbContext(_contextOptions);
	}
}