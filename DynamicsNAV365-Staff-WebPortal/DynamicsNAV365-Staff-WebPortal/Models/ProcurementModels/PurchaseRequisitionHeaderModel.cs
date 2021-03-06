﻿using DynamicsNAV365_Staff_WebPortal.Controllers.FinanceServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Models.ProcurementModels
{
    public class PurchaseRequisitionHeaderModel
    {
        public string No { get; set; }

        [Display(Name = "Requisition Date.")]
        public string DocumentDate { get; set; }

        [Display(Name = "Expected Receipt Date")]
        public string RequestedReceiptDate { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyCode { get; set; }
        public SelectList CurrencyCodes { get; set; }

        [Display(Name = "Amount")]
        public string Amount { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description Required")]
        public string Description { get; set; }

        public string LocationCode { get; set; }
        public SelectList LocationCodes { get; set; }

        [Display(Name = Dimensions.GlobalDimension1Code)]
        // [Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension1Code + " Required")]
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

        [Display(Name = "Responsibility Center")]
        public string ResponsibilityCenter { get; set; }
        public SelectList ResponsibilityCenters { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        public string EmployeeNo { get; set; }
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }

        public virtual IList<PurchaseRequisitionLineModel> PurchaseRequisitionLines { get; set; }
    }
}