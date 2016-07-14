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

		[Required, DataType(DataType.Date)]
		public DateTime StartDate { get; set; }


		[Required, DataType(DataType.Date)]
		public DateTime EndDate { get; set; }


		[Required, DataType(DataType.Date)]
		public DateTime CheckDate { get; set; }
	}
}