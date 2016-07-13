using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("EmployeePayPeriods", Schema = "UtahEmployee")]
	public class EmployeePayPeriod
	{
		[Key]
		public int ID { get; set; }

		[Required]
		public int YearRank { get; set; }

		[Required]
		public DateTime StartDate { get; set; }


		[Required]
		public DateTime EndDate { get; set; }


		[Required]
		public DateTime CheckDate { get; set; }
	}
}