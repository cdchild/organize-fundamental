using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.Scripture
{
	[Table("Chapter", Schema = "Scripture")]
	public class Chapter
	{
		[Key]
		public int ID { get; set; }

		[Required, ForeignKey("Book")]
		public int BookID { get; set; }
		[InverseProperty("Chapters")]
		public Book Book { get; set; }

		public virtual IList<Verse> Verses { get; set; }

		[Required]
		public int Number { get; set; }
	}
}