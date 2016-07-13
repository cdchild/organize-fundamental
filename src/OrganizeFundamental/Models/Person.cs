using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models
{
	public class Person
	{
		[Key]
		public int ID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		[InverseProperty("Person")]
		public IList<UtahEmployee.Hour> Hours { get; set; }
		[InverseProperty("Person")]
		public IList<UtahEmployee.WorkStamp> WorkStamps { get; set; }
	}
}