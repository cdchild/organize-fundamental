using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("Hours", Schema = "UtahEmployee")]
	public class Hour
	{
		[Key]
		public int ID { get; set; }

		[Required, ForeignKey("Person")]
		public int PersonID { get; set; }
		public Person Person { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public double Hours { get; set; }

		//public bool IsHoliday { get; set; }

		public int? AccrualID { get; set; }

		public string Note { get; set; }
	}
}