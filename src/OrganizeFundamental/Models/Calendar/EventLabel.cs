using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.Calendar
{
	[Table("EventLabels", Schema = "Calendar")]
	public class EventLabel
	{
		[Key]
		public int ID { get; set; }

		[Required, StringLength(100)]
		public string Title { get; set; }

		public virtual List<UtahEmployee.PaidHolidayObservance> PaidHolidayObservances { get; set; }
	}
}
