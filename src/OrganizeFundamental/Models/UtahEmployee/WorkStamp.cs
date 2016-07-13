using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("WorkStamps", Schema = "UtahEmployee")]
	public class WorkStamp
	{
		[Key]
		public int ID { get; set; }

		[Required, ForeignKey("Person")]
		public int PersonID { get; set; }
		public Person Person { get; set; }

		[Required]
		public DateTime Stamp { get; set; }

		public bool IsConsideredWorking { get; set; }

		public bool IsLunch { get; set; }
	}
}