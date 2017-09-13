using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models
{
	public class Organization
	{
		[Key]
		public int ID { get; set; }

		[ForeignKey(nameof(Parent))]
		public int? ParentOrganizationID { get; set; }
		[InverseProperty(nameof(Children))]
		public virtual Organization Parent { get; set; }
		public virtual List<Organization> Children { get; set; }

		[Required, StringLength(50)]
		public string Label { get; set; }
	}
}