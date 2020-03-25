using DynamicsNAV365_Staff_WebPortal.DynamicsNAVODataServiceReference;
using DynamicsNAV365_Staff_WebPortal.Models.Account;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.FinanceServices
{
	public class FinanceHomeController : Controller
    {
		static string companyName = ServiceConnection.CompanyName;
		static string companyURL = "";

		DynamicsNAVSOAPServices dynamicsNAVSOAPServices = new DynamicsNAVSOAPServices(companyURL);
		DynamicsNAVODATAServices dynamicsNAVODataServices = new DynamicsNAVODATAServices(companyURL);

		public FinanceHomeController()
		{

		}

		[Authorize]
		public ActionResult FinanceInfo()
		{
			return View();
		}

		#region Helper Views
		[Authorize]
		[ChildActionOnly]
		public ActionResult _FinanceSidebar()
		{
			EmployeeProfileModel employeeProfileModel = new EmployeeProfileModel();
			employeeProfileModel.PassportAttached = false;
			return PartialView(employeeProfileModel);
		}

		[Authorize]
		[ChildActionOnly]
		public ActionResult _EmployeeBalanceWidget()
		{
			decimal imprestBalance = 0;
			imprestBalance = dynamicsNAVSOAPServices.fundsManagementWS.GetEmployeeImprestBalance(AccountController.GetEmployeeNo());
			EmployeeBalancesModel employeeBalancesModel = new EmployeeBalancesModel();
			employeeBalancesModel.Amount = imprestBalance;
			employeeBalancesModel.AmountStr = imprestBalance.ToString("n");
			return PartialView(employeeBalancesModel);
		}
		#endregion Helper Views

		//#region Helper Functions
		//public JsonResult GetGlobalDimension1Codes()
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesQuery.Global_Dimension_No.Equals(1) && dimensionValuesQuery.Blocked.Equals(false)
		//						  select dimensionValuesQuery;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueObj = new DimensionValueModel();
		//		dimensionValueObj.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueObj.Code = dimensionValue.Code;
		//		dimensionValueObj.Name = dimensionValue.Name;
		//		dimensionValueObj.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueObj.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueObj.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		// dimensionValueObj.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueObj.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueObj);
		//	}
		//	return Json(dimensionValues.ToList(), JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetGlobalDimension2Codes(string GlobalDimension1Code)
		//{
		//	dynamicsNAVODataServices = new DynamicsNAVODATAServices(companyURL);
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesQuery.Global_Dimension_No.Equals(2) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//								dimensionValuesQuery.Blocked.Equals(false)
		//						  select dimensionValuesQuery;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueObj = new DimensionValueModel();
		//		dimensionValueObj.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueObj.Code = dimensionValue.Code;
		//		dimensionValueObj.Name = dimensionValue.Name;
		//		dimensionValueObj.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueObj.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueObj.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		//dimensionValueObj.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueObj.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueObj);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetShortcutDimension3Codes(string GlobalDimension1Code)
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesQuery.Global_Dimension_No.Equals(3) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//								dimensionValuesQuery.Blocked.Equals(false)
		//						  select dimensionValuesQuery;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueObj = new DimensionValueModel();
		//		dimensionValueObj.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueObj.Code = dimensionValue.Code;
		//		dimensionValueObj.Name = dimensionValue.Name;
		//		dimensionValueObj.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueObj.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueObj.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		// dimensionValueObj.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueObj.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueObj);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetShortcutDimension4Codes(string GlobalDimension1Code)
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesQuery.Global_Dimension_No.Equals(4) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//								dimensionValuesQuery.Blocked.Equals(false)
		//						  select dimensionValuesQuery;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueObj = new DimensionValueModel();
		//		dimensionValueObj.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueObj.Code = dimensionValue.Code;
		//		dimensionValueObj.Name = dimensionValue.Name;
		//		dimensionValueObj.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueObj.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueObj.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		// dimensionValueObj.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueObj.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueObj);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetShortcutDimension5Codes(string GlobalDimension1Code)
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesQuery.Global_Dimension_No.Equals(5) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//						  dimensionValuesQuery.Blocked.Equals(false)
		//						  select dimensionValuesQuery;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueObj = new DimensionValueModel();
		//		dimensionValueObj.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueObj.Code = dimensionValue.Code;
		//		dimensionValueObj.Name = dimensionValue.Name;
		//		dimensionValueObj.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueObj.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueObj.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		// dimensionValueObj.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueObj.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueObj);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetShortcutDimension6Codes(string GlobalDimension1Code)
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesList in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesList.Global_Dimension_No.Equals(6) && dimensionValuesList.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//						  dimensionValuesList.Blocked.Equals(false)
		//						  select dimensionValuesList;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueModel = new DimensionValueModel();
		//		dimensionValueModel.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueModel.Code = dimensionValue.Code;
		//		dimensionValueModel.Name = dimensionValue.Name;
		//		dimensionValueModel.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueModel.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueModel.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		//  dimensionValueModel.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueModel.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueModel);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetShortcutDimension7Codes(string GlobalDimension1Code)
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesList in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesList.Global_Dimension_No.Equals(7) && dimensionValuesList.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//						  dimensionValuesList.Blocked.Equals(false)
		//						  select dimensionValuesList;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueModel = new DimensionValueModel();
		//		dimensionValueModel.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueModel.Code = dimensionValue.Code;
		//		dimensionValueModel.Name = dimensionValue.Name;
		//		dimensionValueModel.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueModel.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueModel.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		//dimensionValueModel.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueModel.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueModel);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetShortcutDimension8Codes(string GlobalDimension1Code)
		//{
		//	List<DimensionValueModel> dimensionValueList = new List<DimensionValueModel>();
		//	var dimensionValues = from dimensionValuesList in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
		//						  where dimensionValuesList.Global_Dimension_No.Equals(8) && dimensionValuesList.Global_Dimension_1_Code.Equals(GlobalDimension1Code) &&
		//						  dimensionValuesList.Blocked.Equals(false)
		//						  select dimensionValuesList;
		//	foreach (DimensionValues dimensionValue in dimensionValues)
		//	{
		//		DimensionValueModel dimensionValueModel = new DimensionValueModel();
		//		dimensionValueModel.DimensionCode = dimensionValue.Dimension_Code;
		//		dimensionValueModel.Code = dimensionValue.Code;
		//		dimensionValueModel.Name = dimensionValue.Name;
		//		dimensionValueModel.DimensionValueType = dimensionValue.Dimension_Value_Type;
		//		dimensionValueModel.Blocked = dimensionValue.Blocked ?? false;
		//		dimensionValueModel.GlobalDimensionNo = dimensionValue.Global_Dimension_No ?? 0;
		//		//  dimensionValueModel.SequenceNo = dimensionValue.Sequence_No ?? 0;
		//		dimensionValueModel.GlobalDimension1Code = dimensionValue.Global_Dimension_1_Code;
		//		dimensionValueList.Add(dimensionValueModel);
		//	}
		//	return Json(dimensionValueList, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetCurrencies()
		//{
		//	List<Currency> currencyList = new List<Currency>();
		//	var currencies = from currenciesQuery in dynamicsNAVODataServices.dynamicsNAVOData.Currencies
		//					 select currenciesQuery;
		//	foreach (Currencies currency in currencies)
		//	{
		//		Currency currencyObj = new Currency();
		//		currencyObj.Code = currency.Code;
		//		currencyObj.Description = currency.Description;
		//		currencyList.Add(currencyObj);
		//	}
		//	return Json(currencyList, JsonRequestBehavior.AllowGet);
		//}
		//public string GetLocalCurrencyCode()
		//{
		//	return dynamicsNAVSOAPServices.fundsManagementWS.GetLocalCurrencyCode();
		//}
		//#endregion Helper Functions
	}
}