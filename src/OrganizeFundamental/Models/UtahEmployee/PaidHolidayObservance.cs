using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("PaidHolidayObservances", Schema = "UtahEmployee")]
	public class PaidHolidayObservance
	{
		[Key]
		public int ID { get; set; }

		[Required, DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required, ForeignKey(nameof(EventLabel))]
		public int EventLabelID { get; set; }
		[InverseProperty(nameof(Calendar.EventLabel.PaidHolidayObservances))]
		public virtual Calendar.EventLabel EventLabel { get; set; }
	}
}
