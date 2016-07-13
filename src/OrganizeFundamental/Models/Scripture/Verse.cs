using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.Scripture
{
	[Table("Verses", Schema = "Scripture")]
	public class Verse
	{
		[Key]
		public long ID { get; set; }

		[Required, ForeignKey("Chapter")]
		public int ChapterID { get; set; }
		[InverseProperty("Verses")]
		public Chapter Chapter { get; set; }

		[Required]
		public int Number { get; set; }
	}
}