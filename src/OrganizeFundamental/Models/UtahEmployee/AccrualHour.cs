using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("AccrualHours", Schema = "UtahEmployee")]
	public class AccrualHour
	{
		public int PersonID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public int PayPeriodID { get; set; }
		public int YearRank { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime CheckDate { get; set; }
		public bool? IsCurrent { get; set; }
		public bool? IsFuture { get; set; }
		public bool? IsPast { get; set; }

		public byte AccrualTypeID { get; set; }
		public string Code { get; set; }
		public string Label { get; set; }
		public int AccrualID { get; set; }
		public string Note { get; set; }
		[Key]
		public int HourID { get; set; }
		public DateTime Date { get; set; }
		public float Hours { get; set; }
		public float? Balance { get; set; }

	}
}
