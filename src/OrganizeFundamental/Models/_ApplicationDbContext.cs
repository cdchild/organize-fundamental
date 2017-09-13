using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OrganizeFundamental.Models
{

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options: options) {}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<UtahEmployee.PunchHour>()
				.HasKey(h => new { h.PersonID, h.PayPeriodID, h.Date, h.IsConsideredWorking, h.IsLunch, h.ActualHours });
		}

		public DbSet<UtahEmployee.Accrual> Accruals { get; set; }

		public DbSet<UtahEmployee.AccrualType> AccrualTypes { get; set; }

		public DbSet<Scripture.Book> Books { get; set; }

		public DbSet<Scripture.Chapter> Chapters { get; set; }

		public DbSet<UtahEmployee.EmployeePayPeriod> EmployeePayPeriods { get; set; }

		public DbSet<UtahEmployee.EmployeePayRate> EmployeePayRates { get; set; }

		public DbSet<Calendar.EventLabel> EventLabels { get; set; }

		public DbSet<UtahEmployee.Hour> Hours { get; set; }

		public DbSet<UtahEmployee.PaidHolidayObservance> PaidHolidayObservances { get; set; }

		public DbSet<Person> Persons { get; set; }

		public DbSet<UtahEmployee.PunchHour> PunchHours { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		public DbSet<Scripture.Verse> Verses { get;  set; }

		public DbSet<UtahEmployee.ViewPayPeriod> ViewPayPeriods { get; set; }

		public DbSet<UtahEmployee.WorkStamp> WorkStamps { get; set; }

		public DbSet<UtahEmployee.WorkTimeCardEntry> WorkTimeCardEntries { get; set; }
	}
}