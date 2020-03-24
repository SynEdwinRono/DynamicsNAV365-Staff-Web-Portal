using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.Account
{
	public class SendPasswordResetLinkModel
	{
		[Display(Name = "Employee No.")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Employee No. Required")]
		public string EmployeeNo { get; set; }
		public bool ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }
	}
}