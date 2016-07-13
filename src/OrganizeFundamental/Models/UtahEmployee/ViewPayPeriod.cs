using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("ViewPayPeriods", Schema = "UtahEmployee")]
	public class ViewPayPeriod
	{
		public int ID { get; set; }
		public int YearRank { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime CheckDate { get; set; }

		public bool IsPast { get; set; }
		public bool IsCurrent { get; set; }
		public bool IsFuture { get; set; }
	}
}