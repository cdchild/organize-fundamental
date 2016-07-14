using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("AccrualTypes", Schema = "UtahEmployee")]
	public class AccrualType
	{
		[Key]
		public byte ID { get; set; }

		[Required, StringLength(4)]
		public string Code { get; set; }

		[Required, StringLength(100)]
		public string Label { get; set; }

		public virtual List<Accrual> Accruals { get; set; }
	}
}