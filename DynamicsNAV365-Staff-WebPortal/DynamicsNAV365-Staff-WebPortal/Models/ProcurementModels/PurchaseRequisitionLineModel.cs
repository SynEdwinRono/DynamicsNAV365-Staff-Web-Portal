using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.ProcurementModels
{
    public class PurchaseRequisitionLineModel
    {
        public int LineNo { get; set; }

        public string DocumentNo { get; set; }

        [Display(Name = "Requisition Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requisition Type Required")]
        public string RequisitionType { get; set; }
        public SelectList RequisitionTypes { get; set; }

        [Display(Name = "Requisition Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requisition Code Required")]
        public string RequisitionCode { get; set; }
        public SelectList RequisitionCodes { get; set; }

        public string Type { get; set; }

        public string No { get; set; }

        public string Name { get; set; }

        [Display(Name = "Location Code")]
        public string LineLocationCode { get; set; }
        public SelectList LineLocationCodes { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Cost (Kenya Shillings)")]
        public decimal UnitCost { get; set; }

        public decimal TotalLineCost { get; set; }

        [Display(Name = "Unit of Measure")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unit of Measure Required")]
        public string UOM { get; set; }
        public SelectList UOMs { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description Required")]
        public string LineDescription { get; set; }

        [Display(Name = Dimensions.GlobalDimension1Code)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension1Code + " Required")]
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

        public bool LineErrorStatus { get; set; }
        public string LineErrorMessage { get; set; }
    }
}