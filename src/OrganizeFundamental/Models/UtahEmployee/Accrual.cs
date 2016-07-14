using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("Accruals", Schema = "UtahEmployee")]
	public class Accrual
	{
		[Key]
		public int ID { get; set; }

		[Required, ForeignKey(nameof(Person))]
		public int PersonID { get; set; }
		public virtual Person Person { get; set; }

		[Required, ForeignKey(nameof(AccrualType))]
		public int AccrualTypeID { get; set; }
		[InverseProperty(nameof(UtahEmployee.AccrualType.Accruals))]
		public virtual AccrualType AccrualType { get; set; }
	}
}
