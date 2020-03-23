﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.FinanceModels.ImprestSurrenderModels
{
    public class ImprestSurrenderHeaderModel
    {
        public string No { get; set; }

        public string DocumentDate { get; set; }

        public string PostingDate { get; set; }

        [Display(Name = "Employee No.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee No. Required")]
        public string EmployeeNo { get; set; }

        public string EmployeeName { get; set; }

        [Display(Name = "Imprest No.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "imprest no. cannot be empty")]
        public string ImprestNumber { get; set; }
        public SelectList ImprestNos { get; set; }

        [Display(Name = "Imprest Date.")]
        public string ImprestDate { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyCode { get; set; }
        public SelectList CurrencyCodes { get; set; }

        [Display(Name = "Brief Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a brief description before submitting.")]
        public string Description { get; set; }

        [Display(Name = "Imprest Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Actual Spent")]
        public string ActualSpent { get; set; }

        [Display(Name = "Difference (Amount - Actual Spent)")]
        public string Difference { get; set; }

        [Display(Name = Dimensions.GlobalDimension1Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension1Code + " Required")]
        public string GlobalDimension1Code { get; set; }
        public SelectList GlobalDimension1Codes { get; set; }

        [Display(Name = Dimensions.GlobalDimension2Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension2Code + " Required")]
        public string GlobalDimension2Code { get; set; }
        public SelectList GlobalDimension2Codes { get; set; }

        [Display(Name = Dimensions.ShortcutDimension3Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension3Code + " Required")]
        public string ShortcutDimension3Code { get; set; }
        public SelectList ShortcutDimension3Codes { get; set; }

        [Display(Name = Dimensions.ShortcutDimension4Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension4Code + " Required")]
        public string ShortcutDimension4Code { get; set; }
        public SelectList ShortcutDimension4Codes { get; set; }

        [Display(Name = Dimensions.ShortcutDimension5Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension5Code + " Required")]
        public string ShortcutDimension5Code { get; set; }
        public SelectList ShortcutDimension5Codes { get; set; }

        [Display(Name = Dimensions.ShortcutDimension6Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension6Code + " Required")]
        public string ShortcutDimension6Code { get; set; }
        public SelectList ShortcutDimension6Codes { get; set; }

        [Display(Name = Dimensions.ShortcutDimension7Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension7Code + " Required")]
        public string ShortcutDimension7Code { get; set; }
        public SelectList ShortcutDimension7Codes { get; set; }

        [Display(Name = Dimensions.ShortcutDimension8Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension8Code + " Required")]
        public string ShortcutDimension8Code { get; set; }
        public SelectList ShortcutDimension8Codes { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }

        public virtual IList<ImprestSurrenderLineModel> ImprestSurrenderLines { get; set; }
    }
}