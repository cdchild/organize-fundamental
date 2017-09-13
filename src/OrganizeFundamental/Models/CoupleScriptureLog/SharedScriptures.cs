using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models.CoupleScriptureLog
{
	[Table("SharedScriptures", Schema = "CoupleScriptureLog")]
	public class SharedScriptures
	{
		[Key]
		public int ID { get; set; }

		[Required]
		public DateTime Created { get; set; }

		[Required]
		public DateTime Shared { get; set; }

		[Required, Display(Name = "Person"), ForeignKey(nameof(Person))]
		public int PersonID { get; set; }
		[InverseProperty(nameof(Models.Person.ScripturesReceived))]
		public virtual Person Person { get; set; }

		[Required, Display(Name = "Sharing Person"), ForeignKey(nameof(SharingPerson))]
		public int SharingPersonID { get; set; }
		[InverseProperty(nameof(Models.Person.ScripturesShared))]
		public virtual Person SharingPerson { get; set; }

		[Required, StringLength(200)]
		public string ScriptureReference { get; set; }
	}
}
