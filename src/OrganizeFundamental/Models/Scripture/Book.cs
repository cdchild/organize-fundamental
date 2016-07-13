using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.Scripture
{
	[Table("Books", Schema = "Scripture")]
	public class Book
	{
		[Key]
		public int ID { get; set; }

		[ForeignKey("Parent")]
		public int? ParentID { get; set; }
		[InverseProperty("Children")]
		public Book Parent { get; set; }

		[Required, StringLength(200)]
		public string Title { get; set; }

		[MaxLength]
		public string FullTitle { get; set; }

		[MaxLength]
		public string Summary { get; set; }

		[Required, StringLength(20)]
		public string Label { get; set; }

		[StringLength(8)]
		public string ShortLabel { get; set; }

		public virtual IList<Chapter> Chapters { get; set; }

		public virtual IList<Book> Children { get; set; }
	}
}