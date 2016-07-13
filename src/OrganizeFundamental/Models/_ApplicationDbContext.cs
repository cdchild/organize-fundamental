using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OrganizeFundamental.Models
{

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options: options) {}

		public DbSet<Scripture.Book> Books { get; set; }

		public DbSet<Scripture.Chapter> Chapters { get; set; }

		public DbSet<UtahEmployee.EmployeePayPeriod> EmployeePayPeriods { get; set; }

		public DbSet<UtahEmployee.Hour> Hours { get; set; }

		public DbSet<Person> Persons { get; set; }

		public DbSet<UtahEmployee.PunchHour> PunchHours { get; set; }

		public DbSet<UtahEmployee.PunchPair> PunchPairs { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		public DbSet<Scripture.Verse> Verses { get;  set; }

		public DbSet<UtahEmployee.ViewPayPeriod> ViewPayPeriods { get; set; }

		public DbSet<UtahEmployee.WorkStamp> WorkStamps { get; set; }

		public DbSet<UtahEmployee.WorkTimeCardEntry> WorkTimeCardEntries { get; set; }
	}
}