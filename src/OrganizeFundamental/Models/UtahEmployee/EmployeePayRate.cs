using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.UtahEmployee
{
	[Table("EmployeePayRate", Schema = "UtahEmployee")]
	public class EmployeePayRate
	{
		[Key]
		public int ID { get; set; }

		[Required, ForeignKey(nameof(Person))]
		public int PersonID { get; set; }
		[InverseProperty(nameof(Models.Person.EmployeePayRates))]
		public virtual Person Person { get; set; }

		[Required, DataType(DataType.DateTime)]
		public DateTime Effective { get; set; }

		[Required, Column(TypeName = "DECIMAL(9,4)")]
		public decimal Rate { get; set; }
	}
}
