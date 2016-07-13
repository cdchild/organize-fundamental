using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("PunchHours", Schema = "UtahEmployee")]
	public class PunchHour
	{
		[Key, Column(Order = 0)]
		public DateTime Date { get; set; }
		public bool IsConsideredWorking { get; set; }
		public bool IsLunch { get; set; }
		public bool? HasPotentialError { get; set; }
		public int? HoursTally { get; set; }
		public int? MinutesTally { get; set; }
		public float ActualHours { get; set; }
		public float RoundedHours { get; set; }
		[Key, Column(Order = 1)]
		public int PersonID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public int PayPeriodID { get; set; }
		public int YearRank { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime CheckDate { get; set; }
		public bool? IsCurrentPayPeriod { get; set; }
		public bool? IsFuturePayPeriod { get; set; }
		public bool? IsPastPayPeriod { get; set; }
	}
}