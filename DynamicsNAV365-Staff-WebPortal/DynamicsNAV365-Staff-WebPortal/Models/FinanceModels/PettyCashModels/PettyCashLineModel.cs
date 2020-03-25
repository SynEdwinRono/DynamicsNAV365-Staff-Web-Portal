using DynamicsNAV365_Staff_WebPortal.Controllers.FinanceServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Models.FinanceModels.PettyCashModels
{
	public class PettyCashLineModel
	{
		public int LineNo { get; set; }

		public string DocumentNo { get; set; }

		[Display(Name = "Imprest Code")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Imprest Code Required")]
		public string ImprestCode { get; set; }
		public SelectList ImprestCodes { get; set; }

		[Display(Name = "Traveling From")]
		public string FromCity { get; set; }

		[Display(Name = "To")]
		public string ToCity { get; set; }
		public SelectList Cities { get; set; }
		public string AccountType { get; set; }
		public string AccountNo { get; set; }
		public string AccountName { get; set; }

		[Display(Name = "Description")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Description Required")]
		public string LineDescription { get; set; }

		[Display(Name = Dimensions.GlobalDimension1Code)]
		// [Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension1Code+" Required")]
		public string LineGlobalDimension1Code { get; set; }
		public SelectList LineGlobalDimension1Codes { get; set; }

		[Display(Name = Dimensions.GlobalDimension2Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension2Code+" Required")]
		public string LineGlobalDimension2Code { get; set; }
		public SelectList LineGlobalDimension2Codes { get; set; }

		[Display(Name = Dimensions.ShortcutDimension3Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension3Code+" Required")]
		public string LineShortcutDimension3Code { get; set; }
		public SelectList LineShortcutDimension3Codes { get; set; }

		[Display(Name = Dimensions.ShortcutDimension4Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension4Code+" Required")]
		public string LineShortcutDimension4Code { get; set; }
		public SelectList LineShortcutDimension4Codes { get; set; }

		[Display(Name = Dimensions.ShortcutDimension5Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension5Code+" Required")]
		public string LineShortcutDimension5Code { get; set; }
		public SelectList LineShortcutDimension5Codes { get; set; }

		[Display(Name = Dimensions.ShortcutDimension6Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension6Code+" Required")]
		public string LineShortcutDimension6Code { get; set; }
		public SelectList LineShortcutDimension6Codes { get; set; }

		[Display(Name = Dimensions.ShortcutDimension7Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension7Code+" Required")]
		public string LineShortcutDimension7Code { get; set; }
		public SelectList LineShortcutDimension7Codes { get; set; }

		[Display(Name = Dimensions.ShortcutDimension8Code)]
		//[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension8Code+" Required")]
		public string LineShortcutDimension8Code { get; set; }
		public SelectList LineShortcutDimension8Codes { get; set; }

		[Display(Name = "Amount")]
		[Required(ErrorMessage = "Amount Required")]
		public decimal LineAmount { get; set; }

		public bool LineErrorStatus { get; set; }
		public string LineErrorMessage { get; set; }
	}
}