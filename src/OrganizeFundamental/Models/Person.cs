using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizeFundamental.Models
{
	public class Person
	{
		[Key]
		public int ID { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[InverseProperty("Person")]
		public virtual List<UtahEmployee.Hour> Hours { get; set; }

		public virtual List<UtahEmployee.EmployeePayRate> EmployeePayRates { get; set; }

		[InverseProperty("Person")]
		public virtual List<UtahEmployee.WorkStamp> WorkStamps { get; set; }

		public virtual List<CoupleScriptureLog.SharedScriptures> ScripturesReceived { get; set; }

		public virtual List<CoupleScriptureLog.SharedScriptures> ScripturesShared { get; set; }
	}
}