using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.FinanceModels.FundsClaimModels
{

    public class FundsClaimModel
    {
        public string No { get; set; }
        public string DocumentDate { get; set; }

        public string PostingDate { get; set; }

        public string BankAccountNo { get; set; }

        public string BankAccountName { get; set; }

        public string ReferenceNo { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyCode { get; set; }
        public SelectList CurrencyCodes { get; set; }

        [Display(Name = "Employee No.")]
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }

        [Display(Name = "Date From")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Imprest Date From. Required")]
        public string DateFrom { get; set; }

        [Display(Name = "To Date.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Imprest Date To. Required")]
        public string DateTo { get; set; }

        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        //   [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public string DepartureTime { get; set; }

        [Display(Name = "Return Time")]
        [DataType(DataType.Time)]
        //  [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public string ReturnTime { get; set; }

        [Display(Name = "Assigned Vehicle No.")]
        public string AssignedVehicleNo { get; set; }

        [Display(Name = "Vehicle Registration")]
        public string VehicleRegistration { get; set; }
        public SelectList Vehicles { get; set; }

        [Display(Name = "Assigned Driver")]
        public string AssignedDriverNo { get; set; }

        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        //   [Required(AllowEmptyStrings = false, ErrorMessage = "Destination Required")]
        //   [StringLength(50, ErrorMessage = "Destination cannot exceed 50 characters.")]
        public string Destination { get; set; }

        [Display(Name = "Description")]
        //     [Required(AllowEmptyStrings = false, ErrorMessage = "Description Required")]
        //    [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }
        public string Amount { get; set; }

        [Display(Name = Dimensions.GlobalDimension1Code)]
        //  [Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.GlobalDimension1Code + " Required")]
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
        // [Required(AllowEmptyStrings = false, ErrorMessage = Dimensions.ShortcutDimension6Code + " Required")]
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
        public string Status { get; set; }
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
        public virtual IList<FundsClaimLineModel> FundClaimsLines { get; set; }
    }
}