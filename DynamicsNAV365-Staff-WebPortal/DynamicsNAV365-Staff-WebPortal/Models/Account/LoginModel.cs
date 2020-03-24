using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.Account
{
	public class LoginModel
	{
		[Display(Name = "Employee No.")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Employee No. Required")]
		public string EmployeeNo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
		public bool ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }
	}
}