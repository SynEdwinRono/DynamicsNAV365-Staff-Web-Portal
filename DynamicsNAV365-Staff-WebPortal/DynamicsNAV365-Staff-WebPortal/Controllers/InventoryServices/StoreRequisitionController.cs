using DynamicsNAV365_Staff_WebPortal.Controllers.Responses;
using DynamicsNAV365_Staff_WebPortal.DynamicsNAVODataServiceReference;
using DynamicsNAV365_Staff_WebPortal.Models.InventoryModels.StoreRequisitionModel;
using DynamicsNAV365_Staff_WebPortal.Models.PortalDocumentsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.InventoryServices
{
    public class StoreRequisitionController : Controller
    {
        string companyName = ServiceConnection.CompanyName;
        static string companyURL = "";

        DynamicsNAVSOAPServices dynamicsNAVSOAPServices = new DynamicsNAVSOAPServices(companyURL);
        DynamicsNAVODATAServices dynamicsNAVODataServices = new DynamicsNAVODATAServices(companyURL);

        SuccessResponseController successResponse = new SuccessResponseController();
        InfoResponseController infoResponse = new InfoResponseController();
        ErrorResponseController errorResponse = new ErrorResponseController();

        private string responseHeader = "";
        private string responseMessage = "";
        private string detailedResponseMessage = "";
        private string returnControllerName = "";
        private string returnActionName = "";
        private string returnLinkName = "";
        private bool hasParameters = false;
        private string parameters = "";
        private string cancelControllerName = "";
        private string cancelActionName = "";
        private string cancelLinkName = "";

        IQueryable<Locations> locations = null;
        IQueryable<DimensionValues> globalDimension1Values = null;
        IQueryable<DimensionValues> globalDimension2Values = null;
        IQueryable<DimensionValues> shortcutDimension3Values = null;
        IQueryable<DimensionValues> shortcutDimension4Values = null;
        IQueryable<DimensionValues> shortcutDimension5Values = null;
        IQueryable<DimensionValues> shortcutDimension6Values = null;
        IQueryable<DimensionValues> shortcutDimension7Values = null;
        IQueryable<DimensionValues> shortcutDimension8Values = null;

        IQueryable<Items> items = null;
        IQueryable<ItemUOMs> itemUOMs = null;


        AccountController accountController = new AccountController();
        string employeeNo = "";

        public StoreRequisitionController()
        {
            employeeNo = AccountController.GetEmployeeNo();
        }

        #region New Store Requisition
        [Authorize]
        public ActionResult NewStoreRequisition()
        {
            string storeRequisitionNo = "";
            try
            {
                StoreRequisitionHeaderModel storeRequisitionObj = new StoreRequisitionHeaderModel();
                //Check open store requisition
                if (dynamicsNAVSOAPServices.inventoryManagementWS.CheckOpenStoreRequisitionExists(employeeNo))
                {
                    responseHeader = "Open Store Requisition";
                    responseMessage = "An open store requisition exists for employee no. " + employeeNo + ", finalize on this store requisition before creating a new one.";
                    detailedResponseMessage = "An open store requisition exists for employee no. " + employeeNo + ", finalize on this store requisition before creating a new one.";
                    returnControllerName = "StoreRequisition";
                    returnActionName = "StoreRequisitionHistory";
                    returnLinkName = "Ok";
                    hasParameters = false;
                    parameters = "";
                    return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                          returnControllerName, returnActionName, returnLinkName,
                                                          hasParameters, parameters);
                }
                //End check open store requisition

                //Create a new store requisition
                storeRequisitionNo = dynamicsNAVSOAPServices.inventoryManagementWS.CreateStoreRequisition(employeeNo);
                //End create store requisition

                storeRequisitionObj.No = storeRequisitionNo;
                storeRequisitionObj.EmployeeNo = employeeNo;
                storeRequisitionObj.GlobalDimension1Code = "";

                LoadLocationCodes();
                LoadStoreRequisitionDimensions(storeRequisitionObj.GlobalDimension1Code);

                storeRequisitionObj.LocationCodes = new SelectList(locations, "Code", "Code");
                storeRequisitionObj.GlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
                storeRequisitionObj.GlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
                storeRequisitionObj.ShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
                storeRequisitionObj.ShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
                storeRequisitionObj.ShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
                storeRequisitionObj.ShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
                storeRequisitionObj.ShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
                storeRequisitionObj.ShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");
                return View(storeRequisitionObj);
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        [Authorize]
        [HttpPost]
        //	[ValidateAntiForgeryToken]
        public ActionResult NewStoreRequisition(StoreRequisitionHeaderModel StoreRequisitionObj)
        {
            bool storeRequisitionModified = false;
            bool approvalWorkflowExist = false;

            LoadLocationCodes();
            LoadStoreRequisitionDimensions(StoreRequisitionObj.GlobalDimension1Code);
            StoreRequisitionObj.LocationCodes = new SelectList(locations, "Code", "Code");
            StoreRequisitionObj.GlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
            StoreRequisitionObj.GlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
            StoreRequisitionObj.ShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
            StoreRequisitionObj.ShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
            StoreRequisitionObj.ShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
            StoreRequisitionObj.ShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
            StoreRequisitionObj.ShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
            StoreRequisitionObj.ShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");
            if (ModelState.IsValid)
            {
                try
                {
                    if (dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionExists(StoreRequisitionObj.No, AccountController.GetEmployeeNo()))
                    {
                        //Check store requisition lines
                        if (!dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionLinesExist(StoreRequisitionObj.No))
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "Store requisition lines missing, the storeRequisition must contain a minimum of one storeRequisition line, add an storeRequisition line to continue.";
                            return View(StoreRequisitionObj);
                        }
                        //Validate store requisition lines
                        string storeRequisitionLineError = "";
                        storeRequisitionLineError = dynamicsNAVSOAPServices.inventoryManagementWS.ValidateStoreRequisitionLines(StoreRequisitionObj.No);
                        if (!storeRequisitionLineError.Equals(""))
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = storeRequisitionLineError;
                            return View(StoreRequisitionObj);
                        }
                        //Modify store requisition
                        StoreRequisitionObj.RequesterID = StoreRequisitionObj.RequesterID != null ? StoreRequisitionObj.RequesterID : "";

                        storeRequisitionModified = dynamicsNAVSOAPServices.inventoryManagementWS.ModifyStoreRequisition(StoreRequisitionObj.No, employeeNo, DateTime.Parse(StoreRequisitionObj.RequiredDate), StoreRequisitionObj.Description);

                        if (!storeRequisitionModified)
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "An error was experienced while trying to modify store requisition no." + StoreRequisitionObj.No + ", the server might be offline, try again after a while.";
                            return View(StoreRequisitionObj);
                        }
                        //Send store requisition for approval
                        approvalWorkflowExist = dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionApprovalWorkflowEnabled(StoreRequisitionObj.No);
                        if (!approvalWorkflowExist)
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "An error was experienced while trying to send an approval request for store requisition no." + StoreRequisitionObj.No + ", the approval workflow was not found. Contact the " + companyName + " ICT department for assistance.";
                            return View(StoreRequisitionObj);
                        }

                        if (dynamicsNAVSOAPServices.inventoryManagementWS.SendStoreRequisitionApprovalRequest(StoreRequisitionObj.No))
                        {
                            responseHeader = "Success";
                            responseMessage = "Store requisition no." + StoreRequisitionObj.No + " was successfully sent for approval. Check with the " + companyName + " inventory department for approval status.";
                            detailedResponseMessage = "Store requisition no." + StoreRequisitionObj.No + " was successfully sent for approval. Check with the " + companyName + " inventory department for approval status.";
                            returnControllerName = "StoreRequisition";
                            returnActionName = "StoreRequisitionHistory";
                            returnLinkName = "Ok";
                            hasParameters = false;
                            parameters = "";
                            return successResponse.ApplicationSuccess(responseHeader, responseMessage,
                                detailedResponseMessage, returnControllerName, returnActionName,
                                returnLinkName, hasParameters, parameters);
                        }
                        else
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "An error was experienced while trying to send an approval request for store requisition no." + StoreRequisitionObj.No + ". Contact the " + companyName + " ICT department for assistance.";
                            return View(StoreRequisitionObj);
                        }
                    }
                    else
                    {
                        responseHeader = "Store Requisition NotFound";
                        responseMessage = "The store requisition no." + StoreRequisitionObj.No + " was not found under employee no." + AccountController.GetEmployeeNo();
                        detailedResponseMessage = "The store requisition no." + StoreRequisitionObj.No + " was not found under employee no." + AccountController.GetEmployeeNo();
                        returnControllerName = "StoreRequisition";
                        returnActionName = "StoreRequisitionHistory";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                               returnControllerName, returnActionName, returnLinkName,
                                                               hasParameters, parameters);
                    }
                }
                catch (Exception ex)
                {
                    StoreRequisitionObj.ErrorStatus = true;
                    StoreRequisitionObj.ErrorMessage = ex.Message.ToString();
                    return View(StoreRequisitionObj);
                }
            }
            else
            {
                return View(StoreRequisitionObj);
            }
        }
        #endregion New Store Requisition

        #region Edit Store Requisition
        [Authorize]
        public ActionResult OnBeforeEdit(string StoreRequisitionNo)
        {
            try
            {
                if (StoreRequisitionNo.Equals(""))
                {
                    return RedirectToAction("StoreRequisitionHistory", "StoreRequisition");
                }
                if (dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionExists(StoreRequisitionNo, AccountController.GetEmployeeNo()))
                {
                    string storeRequisitionStatus = GetStoreRequisitionStatus(StoreRequisitionNo);
                    //if store requisition is open
                    if (storeRequisitionStatus.Equals("Open"))
                    {
                        return RedirectToAction("EditStoreRequisition", "StoreRequisition", new { StoreRequisitionNo = StoreRequisitionNo });
                    }
                    ////if store requisition is pending approval
                    //if (storeRequisitionStatus.Equals("Pending Approval"))
                    //{
                    //	responseHeader = "Store Requisition Pending Approval";
                    //	responseMessage = "The stores requisition no." + StoreRequisitionNo + " is pending approval. Editing will cancel the approval request and uncommit the document from the budget. Do you want to continue?";
                    //	detailedResponseMessage = "The stores requisition no." + StoreRequisitionNo + " is pending approval. Editing will cancel the approval request and uncommit the document from the budget. Do you want to continue?";
                    //	returnControllerName = "StoreRequisition";
                    //	returnActionName = "EditStoreRequisition";
                    //	returnLinkName = "Yes";
                    //	hasParameters = true;
                    //	parameters = "?StoreRequisitionNo=" + StoreRequisitionNo;
                    //	cancelControllerName = "StoreRequisition";
                    //	cancelActionName = "StoreRequisitionHistory";
                    //	cancelLinkName = "No";
                    //	return infoResponse.ApplicationConfirm(responseHeader, responseMessage, detailedResponseMessage,
                    //										   returnControllerName, returnActionName, returnLinkName,
                    //										   hasParameters, parameters, cancelControllerName,
                    //										   cancelActionName, cancelLinkName);
                    //}

                    //if store requisition is pending approval
                    if (storeRequisitionStatus.Equals("Pending Approval"))
                    {
                        responseHeader = "Store Requisition Pending Approval";
                        responseMessage = "The stores requisition no." + StoreRequisitionNo + " is already submitted for approval. Editing not allowed.";
                        detailedResponseMessage = "The stores requisition no." + StoreRequisitionNo + " is already submitted for approval. Editing not allowed.";
                        returnControllerName = "StoreRequisition";
                        returnActionName = "StoreRequisitionHistory";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                               returnControllerName, returnActionName, returnLinkName,
                                                               hasParameters, parameters);
                    }

                    //if store requisition is released
                    if (storeRequisitionStatus.Equals("Released"))
                    {
                        responseHeader = "Store Requisition Approved";
                        responseMessage = "The store requisition no." + StoreRequisitionNo + " is already approved. Editing not allowed.";
                        detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " is already approved. Editing not allowed.";
                        returnControllerName = "StoreRequisition";
                        returnActionName = "StoreRequisitionHistory";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                               returnControllerName, returnActionName, returnLinkName,
                                                               hasParameters, parameters);
                    }
                    //if store requisition is rejected
                    if (storeRequisitionStatus.Equals("Rejected"))
                    {
                        responseHeader = "Store Requisition Rejected";
                        responseMessage = "The store requisition no." + StoreRequisitionNo + " was rejected. Editing will reopen the document. Do you want to continue?";
                        detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " was rejected. Editing will reopen the document. Do you want to continue?";
                        returnControllerName = "StoreRequisition";
                        returnActionName = "EditStoreRequisition";
                        returnLinkName = "Yes";
                        hasParameters = true;
                        parameters = "?StoreRequisitionNo=" + StoreRequisitionNo;
                        cancelControllerName = "StoreRequisition";
                        cancelActionName = "StoreRequisitionHistory";
                        cancelLinkName = "No";
                        return infoResponse.ApplicationConfirm(responseHeader, responseMessage, detailedResponseMessage,
                                                               returnControllerName, returnActionName, returnLinkName,
                                                               hasParameters, parameters, cancelControllerName,
                                                               cancelActionName, cancelLinkName);
                    }
                    //if store requisition is posted/reversed
                    if (storeRequisitionStatus.Equals("Posted") || storeRequisitionStatus.Equals("Reversed"))
                    {
                        responseHeader = "Store Requisition Posted";
                        responseMessage = "The store requisition no." + StoreRequisitionNo + " is already posted. Editing not allowed.";
                        detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " is already posted. Editing not allowed.";
                        returnControllerName = "StoreRequisition";
                        returnActionName = "StoreRequisitionHistory";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                               returnControllerName, returnActionName, returnLinkName,
                                                               hasParameters, parameters);
                    }
                    return RedirectToAction("StoreRequisitionHistory", "StoreRequisition");
                }
                else
                {
                    responseHeader = "Store Requisition NotFound";
                    responseMessage = "The store requisition no." + StoreRequisitionNo + " was not found under employee no." + AccountController.GetEmployeeNo();
                    detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " was not found under employee no." + AccountController.GetEmployeeNo();
                    returnControllerName = "StoreRequisition";
                    returnActionName = "StoreRequisitionHistory";
                    returnLinkName = "Ok";
                    hasParameters = false;
                    parameters = "";
                    return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                           returnControllerName, returnActionName, returnLinkName,
                                                           hasParameters, parameters);
                }
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        [Authorize]
        public ActionResult EditStoreRequisition(string StoreRequisitionNo)
        {
            try
            {
                if (StoreRequisitionNo.Equals(""))
                {
                    return RedirectToAction("StoreRequisitionHistory", "StoreRequisition");
                }
                if (dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionExists(StoreRequisitionNo, AccountController.GetEmployeeNo()))
                {
                    string storeRequisitionStatus = dynamicsNAVSOAPServices.inventoryManagementWS.GetStoreRequisitionStatus(StoreRequisitionNo);

                    //if store requisition is pending approval, cancel approval request
                    if (storeRequisitionStatus.Equals("Pending Approval"))
                    {
                        //dynamicsNAVSOAPServices.inventoryManagementWS.CancelStoreRequisitionApprovalRequest(StoreRequisitionNo);
                        dynamicsNAVSOAPServices.inventoryManagementWS.CancelStoreRequisitionBudgetCommitment(StoreRequisitionNo);
                    }
                    //if store requisition is released, reopen and uncommit from budget
                    if (storeRequisitionStatus.Equals("Released"))
                    {
                        dynamicsNAVSOAPServices.inventoryManagementWS.ReopenStoreRequisition(StoreRequisitionNo);
                        dynamicsNAVSOAPServices.inventoryManagementWS.CancelStoreRequisitionBudgetCommitment(StoreRequisitionNo);
                    }
                    //if store requisition is rejected, reopen document
                    if (storeRequisitionStatus.Equals("Rejected"))
                    {
                        dynamicsNAVSOAPServices.inventoryManagementWS.ReopenStoreRequisition(StoreRequisitionNo);
                    }

                    StoreRequisitionHeaderModel storeRequisitionObj = new StoreRequisitionHeaderModel();
                    var storeRequisitions = from storeRequisitionsQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitions
                                            where storeRequisitionsQuery.No.Equals(StoreRequisitionNo)
                                            select storeRequisitionsQuery;
                    foreach (StoreRequisitions storeRequisition in storeRequisitions)
                    {
                        storeRequisitionObj.No = storeRequisition.No;
                        storeRequisitionObj.EmployeeNo = storeRequisition.Employee_No;
                        storeRequisitionObj.DocumentDate = storeRequisition.Document_Date != null ? storeRequisition.Document_Date.Value.ToShortDateString() : "n/a";
                        storeRequisitionObj.RequiredDate = storeRequisition.Required_Date != null ? storeRequisition.Required_Date.Value.ToShortDateString() : "n/a";
                        storeRequisitionObj.RequesterID = storeRequisition.Requester_ID;
                        storeRequisitionObj.Amount = (storeRequisition.Amount ?? 0).ToString("N");
                        storeRequisitionObj.Description = storeRequisition.Description;
                        storeRequisitionObj.Status = storeRequisition.Status;
                        storeRequisitionObj.LocationCode = storeRequisition.Location_Code;
                        storeRequisitionObj.GlobalDimension1Code = storeRequisition.Global_Dimension_1_Code;
                        storeRequisitionObj.GlobalDimension2Code = storeRequisition.Global_Dimension_2_Code;
                        storeRequisitionObj.ShortcutDimension3Code = storeRequisition.Shortcut_Dimension_3_Code;
                        storeRequisitionObj.ShortcutDimension4Code = storeRequisition.Shortcut_Dimension_4_Code;
                        storeRequisitionObj.ShortcutDimension5Code = storeRequisition.Shortcut_Dimension_5_Code;
                        storeRequisitionObj.ShortcutDimension6Code = storeRequisition.Shortcut_Dimension_6_Code;
                        storeRequisitionObj.ShortcutDimension7Code = storeRequisition.Shortcut_Dimension_7_Code;
                        storeRequisitionObj.ShortcutDimension8Code = storeRequisition.Shortcut_Dimension_8_Code;
                    }

                    LoadLocationCodes();
                    LoadStoreRequisitionDimensions(storeRequisitionObj.GlobalDimension1Code);
                    storeRequisitionObj.LocationCodes = new SelectList(locations, "Code", "Code");
                    storeRequisitionObj.GlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
                    storeRequisitionObj.GlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");

                    return View(storeRequisitionObj);
                }
                else
                {
                    responseHeader = "Store Requisition NotFound";
                    responseMessage = "The store requisition no." + StoreRequisitionNo + " was not found for employee no." + AccountController.GetEmployeeNo();
                    detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " was not found for employee no." + AccountController.GetEmployeeNo();
                    returnControllerName = "StoreRequisition";
                    returnActionName = "StoreRequisitionHistory";
                    returnLinkName = "Ok";
                    hasParameters = false;
                    parameters = "";
                    return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                           returnControllerName, returnActionName, returnLinkName,
                                                           hasParameters, parameters);
                }
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditStoreRequisition(StoreRequisitionHeaderModel StoreRequisitionObj)
        {
            bool storeRequisitionModified = false;
            bool approvalWorkflowExist = false;
            try
            {
                LoadLocationCodes();
                LoadStoreRequisitionDimensions(StoreRequisitionObj.GlobalDimension1Code);
                StoreRequisitionObj.LocationCodes = new SelectList(locations, "Code", "Code");
                StoreRequisitionObj.GlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
                StoreRequisitionObj.GlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
                StoreRequisitionObj.ShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
                StoreRequisitionObj.ShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
                StoreRequisitionObj.ShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
                StoreRequisitionObj.ShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
                StoreRequisitionObj.ShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
                StoreRequisitionObj.ShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");

                if (ModelState.IsValid)
                {
                    if (dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionExists(StoreRequisitionObj.No, AccountController.GetEmployeeNo()))
                    {
                        //Check store requisition lines
                        if (!dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionLinesExist(StoreRequisitionObj.No))
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "Store requisition lines missing, the store requisition must contain a minimum of one line, add an store requisition line to continue.";
                            return View(StoreRequisitionObj);
                        }
                        //Validate store requisition lines
                        string storeRequisitionLineError = "";
                        storeRequisitionLineError = dynamicsNAVSOAPServices.inventoryManagementWS.ValidateStoreRequisitionLines(StoreRequisitionObj.No);
                        if (!storeRequisitionLineError.Equals(""))
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = storeRequisitionLineError;
                            return View(StoreRequisitionObj);
                        }

                        //Modify store requisition
                        StoreRequisitionObj.RequesterID = StoreRequisitionObj.RequesterID != null ? StoreRequisitionObj.RequesterID : "";

                        storeRequisitionModified = dynamicsNAVSOAPServices.inventoryManagementWS.ModifyStoreRequisition(StoreRequisitionObj.No, employeeNo, DateTime.Parse(StoreRequisitionObj.RequiredDate), StoreRequisitionObj.Description);

                        if (!storeRequisitionModified)
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "An error was experienced while trying to modify store requisition no." + StoreRequisitionObj.No + ", the server might be offline, try again after a while.";
                            return View(StoreRequisitionObj);
                        }
                        //Send store requisition for approval
                        approvalWorkflowExist = dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionApprovalWorkflowEnabled(StoreRequisitionObj.No);
                        if (!approvalWorkflowExist)
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "An error was experienced while trying to send an approval request for store requisition no." + StoreRequisitionObj.No + ", the approval workflow was not found. Contact the " + companyName + " ICT department for assistance.";
                            return View(StoreRequisitionObj);
                        }

                        if (dynamicsNAVSOAPServices.inventoryManagementWS.SendStoreRequisitionApprovalRequest(StoreRequisitionObj.No))
                        {
                            responseHeader = "Success";
                            responseMessage = "Store requisition no." + StoreRequisitionObj.No + " was successfully sent for approval. Check with the " + companyName + " inventory department for approval status.";
                            detailedResponseMessage = "StoreRequisition no." + StoreRequisitionObj.No + " was successfully sent for approval. Check with the " + companyName + " inventory department for approval status.";
                            returnControllerName = "StoreRequisition";
                            returnActionName = "StoreRequisitionHistory";
                            returnLinkName = "Ok";
                            hasParameters = false;
                            parameters = "";
                            return successResponse.ApplicationSuccess(responseHeader, responseMessage,
                                detailedResponseMessage, returnControllerName, returnActionName,
                                returnLinkName, hasParameters, parameters);
                        }
                        else
                        {
                            StoreRequisitionObj.ErrorStatus = true;
                            StoreRequisitionObj.ErrorMessage = "An error was experienced while trying to send an approval request for store requisition no." + StoreRequisitionObj.No + ". Contact the " + companyName + " ICT department for assistance.";
                            return View(StoreRequisitionObj);
                        }
                    }
                    else
                    {
                        responseHeader = "Store Requisition NotFound";
                        responseMessage = "The store requisition no." + StoreRequisitionObj.No + " was not found for employee no." + AccountController.GetEmployeeNo();
                        detailedResponseMessage = "The store requisition no." + StoreRequisitionObj.No + " was not found for employee no." + AccountController.GetEmployeeNo();
                        returnControllerName = "StoreRequisition";
                        returnActionName = "StoreRequisitionHistory";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                                returnControllerName, returnActionName, returnLinkName,
                                                                hasParameters, parameters);
                    }
                }
                else
                {
                    return View(StoreRequisitionObj);
                }
            }
            catch (Exception ex)
            {
                StoreRequisitionObj.ErrorStatus = true;
                StoreRequisitionObj.ErrorMessage = ex.Message.ToString();
                return View(StoreRequisitionObj);
            }
        }
        #endregion Edit Store Requisition

        #region View Store Requisition
        [Authorize]
        public ActionResult ViewStoreRequisition(string StoreRequisitionNo)
        {
            try
            {
                if (StoreRequisitionNo.Equals(""))
                {
                    return RedirectToAction("StoreRequisitionHistory", "StoreRequisition");
                }
                if (dynamicsNAVSOAPServices.inventoryManagementWS.CheckStoreRequisitionExists(StoreRequisitionNo, AccountController.GetEmployeeNo()))
                {
                    StoreRequisitionHeaderModel storeRequisitionObj = new StoreRequisitionHeaderModel();
                    var storeRequisitions = from storeRequisitionsQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitions
                                            where storeRequisitionsQuery.No.Equals(StoreRequisitionNo)
                                            select storeRequisitionsQuery;
                    foreach (StoreRequisitions storeRequisition in storeRequisitions)
                    {
                        storeRequisitionObj.No = storeRequisition.No;
                        storeRequisitionObj.EmployeeNo = storeRequisition.Employee_No;
                        storeRequisitionObj.DocumentDate = storeRequisition.Document_Date != null ? storeRequisition.Document_Date.Value.ToShortDateString() : "n/a";
                        storeRequisitionObj.RequiredDate = storeRequisition.Required_Date != null ? storeRequisition.Required_Date.Value.ToShortDateString() : "n/a";
                        storeRequisitionObj.RequesterID = storeRequisition.Requester_ID;
                        storeRequisitionObj.Amount = (storeRequisition.Amount ?? 0).ToString("N");
                        storeRequisitionObj.Description = storeRequisition.Description;
                        storeRequisitionObj.Status = storeRequisition.Status;
                        storeRequisitionObj.LocationCode = storeRequisition.Location_Code;
                        storeRequisitionObj.GlobalDimension1Code = storeRequisition.Global_Dimension_1_Code;
                        storeRequisitionObj.GlobalDimension2Code = storeRequisition.Global_Dimension_2_Code;
                        storeRequisitionObj.ShortcutDimension3Code = storeRequisition.Shortcut_Dimension_3_Code;
                        storeRequisitionObj.ShortcutDimension4Code = storeRequisition.Shortcut_Dimension_4_Code;
                        storeRequisitionObj.ShortcutDimension5Code = storeRequisition.Shortcut_Dimension_5_Code;
                        storeRequisitionObj.ShortcutDimension6Code = storeRequisition.Shortcut_Dimension_6_Code;
                        storeRequisitionObj.ShortcutDimension7Code = storeRequisition.Shortcut_Dimension_7_Code;
                        storeRequisitionObj.ShortcutDimension8Code = storeRequisition.Shortcut_Dimension_8_Code;
                    }
                    LoadLocationCodes();
                    LoadStoreRequisitionDimensions(storeRequisitionObj.GlobalDimension1Code);
                    storeRequisitionObj.LocationCodes = new SelectList(locations, "Code", "Name");
                    storeRequisitionObj.GlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
                    storeRequisitionObj.GlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
                    storeRequisitionObj.ShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");

                    return View(storeRequisitionObj);
                }
                else
                {
                    responseHeader = "Store Requisition NotFound";
                    responseMessage = "The store requisition no." + StoreRequisitionNo + " was not found for employee no." + AccountController.GetEmployeeNo();
                    detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " was not found for employee no." + AccountController.GetEmployeeNo();
                    returnControllerName = "StoreRequisition";
                    returnActionName = "StoreRequisitionHistory";
                    returnLinkName = "Ok";
                    hasParameters = false;
                    parameters = "";
                    return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                           returnControllerName, returnActionName, returnLinkName,
                                                           hasParameters, parameters);
                }
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }
        #endregion View Store Requisition

        #region Store Requisition Line
        [ChildActionOnly]
        [Authorize]
        public ActionResult _StoreRequisitionLine(string StoreRequisitionNo)
        {
            StoreRequisitionLineModel storeRequisitionLineObj = new StoreRequisitionLineModel();

            LoadItems();
            LoadItemUOMs();
            LoadLocationCodes();

            var items = from itemsQuery in dynamicsNAVODataServices.dynamicsNAVOData.Items
                        where itemsQuery.Blocked.Equals(false)
                        select itemsQuery;

            string globalDimension1Code = dynamicsNAVSOAPServices.inventoryManagementWS.GetStoreRequisitionGlobalDimension1Code(StoreRequisitionNo);
            LoadStoreRequisitionDimensions(globalDimension1Code);

            storeRequisitionLineObj.Items = new SelectList(items, "No", "Description");
            storeRequisitionLineObj.LineLocationCodes = new SelectList(locations, "Code", "Code");
            storeRequisitionLineObj.UOMs = new SelectList(itemUOMs, "Code", "Code");
            storeRequisitionLineObj.LineGlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
            storeRequisitionLineObj.LineGlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");
            return PartialView(storeRequisitionLineObj);
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult _ViewStoreRequisitionLine(string StoreRequisitionNo)
        {
            StoreRequisitionLineModel storeRequisitionLineObj = new StoreRequisitionLineModel();

            LoadItems();
            LoadItemUOMs();
            LoadLocationCodes();
            string globalDimension1Code = dynamicsNAVSOAPServices.inventoryManagementWS.GetStoreRequisitionGlobalDimension1Code(StoreRequisitionNo);
            LoadStoreRequisitionDimensions(globalDimension1Code);

            storeRequisitionLineObj.Items = new SelectList(items, "No", "Description");
            storeRequisitionLineObj.LineLocationCodes = new SelectList(locations, "Code", "Code");
            storeRequisitionLineObj.UOMs = new SelectList(itemUOMs, "Code", "Code");
            storeRequisitionLineObj.LineGlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Code");
            storeRequisitionLineObj.LineGlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Code");
            storeRequisitionLineObj.LineShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Code");
            return PartialView(storeRequisitionLineObj);
        }

        [Authorize]
        public JsonResult GetStoreRequisitionLines(string DocumentNo)
        {
            List<StoreRequisitionLineModel> storeRequisitionLinesList = new List<StoreRequisitionLineModel>();

            var storeRequisitionLines = from storeRequisitionLinesQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitionLines
                                        where storeRequisitionLinesQuery.Document_No.Equals(DocumentNo)
                                        select storeRequisitionLinesQuery;
            foreach (StoreRequisitionLines storeRequisitionLine in storeRequisitionLines)
            {
                StoreRequisitionLineModel storeRequisitionLineObj = new StoreRequisitionLineModel();
                storeRequisitionLineObj.LineNo = storeRequisitionLine.Line_No;
                storeRequisitionLineObj.DocumentNo = storeRequisitionLine.Document_No;
                storeRequisitionLineObj.ItemNo = storeRequisitionLine.Item_No;
                storeRequisitionLineObj.ItemDescription = storeRequisitionLine.Item_Description;
                storeRequisitionLineObj.LineLocationCode = storeRequisitionLine.Location_Code;
                storeRequisitionLineObj.UOM = storeRequisitionLine.Unit_of_Measure_Code;
                storeRequisitionLineObj.Inventory = storeRequisitionLine.Inventory ?? 0;
                storeRequisitionLineObj.Quantity = storeRequisitionLine.Quantity ?? 0;
                storeRequisitionLineObj.LineDescription = storeRequisitionLine.Description;
                storeRequisitionLineObj.LineGlobalDimension1Code = storeRequisitionLine.Global_Dimension_1_Code;
                storeRequisitionLineObj.LineGlobalDimension2Code = storeRequisitionLine.Global_Dimension_2_Code;
                storeRequisitionLineObj.LineShortcutDimension3Code = storeRequisitionLine.Shortcut_Dimension_3_Code;
                storeRequisitionLineObj.LineShortcutDimension4Code = storeRequisitionLine.Shortcut_Dimension_4_Code;
                storeRequisitionLineObj.LineShortcutDimension5Code = storeRequisitionLine.Shortcut_Dimension_5_Code;
                storeRequisitionLineObj.LineShortcutDimension6Code = storeRequisitionLine.Shortcut_Dimension_6_Code;
                storeRequisitionLineObj.LineShortcutDimension7Code = storeRequisitionLine.Shortcut_Dimension_7_Code;
                storeRequisitionLineObj.LineShortcutDimension8Code = storeRequisitionLine.Shortcut_Dimension_8_Code;
                storeRequisitionLinesList.Add(storeRequisitionLineObj);
            }
            return Json(storeRequisitionLinesList, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetStoreRequisitionLine(int LineNo, string DocumentNo)
        {
            StoreRequisitionLineModel storeRequisitionLineObj = new StoreRequisitionLineModel();

            var storeRequisitionLines = from storeRequisitionLinesQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitionLines
                                        where storeRequisitionLinesQuery.Line_No.Equals(LineNo) && storeRequisitionLinesQuery.Document_No.Equals(DocumentNo)
                                        select storeRequisitionLinesQuery;
            foreach (StoreRequisitionLines storeRequisitionLine in storeRequisitionLines)
            {
                storeRequisitionLineObj.LineNo = storeRequisitionLine.Line_No;
                storeRequisitionLineObj.DocumentNo = storeRequisitionLine.Document_No;
                storeRequisitionLineObj.ItemNo = storeRequisitionLine.Item_No;
                storeRequisitionLineObj.ItemDescription = storeRequisitionLine.Item_Description;
                storeRequisitionLineObj.LineLocationCode = storeRequisitionLine.Location_Code;
                storeRequisitionLineObj.UOM = storeRequisitionLine.Unit_of_Measure_Code;
                storeRequisitionLineObj.Inventory = storeRequisitionLine.Inventory ?? 0;
                storeRequisitionLineObj.Quantity = storeRequisitionLine.Quantity ?? 0;
                storeRequisitionLineObj.LineDescription = storeRequisitionLine.Description;
                storeRequisitionLineObj.LineGlobalDimension1Code = storeRequisitionLine.Global_Dimension_1_Code;
                storeRequisitionLineObj.LineGlobalDimension2Code = storeRequisitionLine.Global_Dimension_2_Code;
                storeRequisitionLineObj.LineShortcutDimension3Code = storeRequisitionLine.Shortcut_Dimension_3_Code;
                storeRequisitionLineObj.LineShortcutDimension4Code = storeRequisitionLine.Shortcut_Dimension_4_Code;
                storeRequisitionLineObj.LineShortcutDimension5Code = storeRequisitionLine.Shortcut_Dimension_5_Code;
                storeRequisitionLineObj.LineShortcutDimension6Code = storeRequisitionLine.Shortcut_Dimension_6_Code;
                storeRequisitionLineObj.LineShortcutDimension7Code = storeRequisitionLine.Shortcut_Dimension_7_Code;
                storeRequisitionLineObj.LineShortcutDimension8Code = storeRequisitionLine.Shortcut_Dimension_8_Code;
            }

            return Json(storeRequisitionLineObj, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult CreateStoreRequisitionLine(StoreRequisitionLineModel StoreRequisitionLineObj)
        {
            bool storeRequisitionLineCreated = false;

            StoreRequisitionLineObj.ItemDescription = StoreRequisitionLineObj.ItemDescription != null ? StoreRequisitionLineObj.ItemDescription : "";
            StoreRequisitionLineObj.LineDescription = StoreRequisitionLineObj.LineDescription != null ? StoreRequisitionLineObj.LineDescription : "";
            StoreRequisitionLineObj.LineGlobalDimension1Code = StoreRequisitionLineObj.LineGlobalDimension1Code != null ? StoreRequisitionLineObj.LineGlobalDimension1Code : "";
            StoreRequisitionLineObj.LineGlobalDimension2Code = StoreRequisitionLineObj.LineGlobalDimension2Code != null ? StoreRequisitionLineObj.LineGlobalDimension2Code : "";
            StoreRequisitionLineObj.LineShortcutDimension3Code = StoreRequisitionLineObj.LineShortcutDimension3Code != null ? StoreRequisitionLineObj.LineShortcutDimension3Code : "";
            StoreRequisitionLineObj.LineShortcutDimension4Code = StoreRequisitionLineObj.LineShortcutDimension4Code != null ? StoreRequisitionLineObj.LineShortcutDimension4Code : "";
            StoreRequisitionLineObj.LineShortcutDimension5Code = StoreRequisitionLineObj.LineShortcutDimension5Code != null ? StoreRequisitionLineObj.LineShortcutDimension5Code : "";
            StoreRequisitionLineObj.LineShortcutDimension6Code = StoreRequisitionLineObj.LineShortcutDimension6Code != null ? StoreRequisitionLineObj.LineShortcutDimension6Code : "";
            StoreRequisitionLineObj.LineShortcutDimension7Code = StoreRequisitionLineObj.LineShortcutDimension7Code != null ? StoreRequisitionLineObj.LineShortcutDimension7Code : "";
            StoreRequisitionLineObj.LineShortcutDimension8Code = StoreRequisitionLineObj.LineShortcutDimension8Code != null ? StoreRequisitionLineObj.LineShortcutDimension8Code : "";

            storeRequisitionLineCreated = dynamicsNAVSOAPServices.inventoryManagementWS.CreateStoreRequisitionLine(StoreRequisitionLineObj.DocumentNo,
                                            StoreRequisitionLineObj.ItemNo, StoreRequisitionLineObj.LineLocationCode,
                                            StoreRequisitionLineObj.Quantity, StoreRequisitionLineObj.UOM, StoreRequisitionLineObj.LineDescription);

            return Json(new { StoreRequisitionLineCreated = storeRequisitionLineCreated }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ModifyStoreRequisitionLine(StoreRequisitionLineModel StoreRequisitionLineObj)
        {
            bool storeRequisitionLineModified = false;

            StoreRequisitionLineObj.ItemDescription = StoreRequisitionLineObj.ItemDescription != null ? StoreRequisitionLineObj.ItemDescription : "";
            StoreRequisitionLineObj.LineDescription = StoreRequisitionLineObj.LineDescription != null ? StoreRequisitionLineObj.LineDescription : "";
            StoreRequisitionLineObj.LineGlobalDimension1Code = StoreRequisitionLineObj.LineGlobalDimension1Code != null ? StoreRequisitionLineObj.LineGlobalDimension1Code : "";
            StoreRequisitionLineObj.LineGlobalDimension2Code = StoreRequisitionLineObj.LineGlobalDimension2Code != null ? StoreRequisitionLineObj.LineGlobalDimension2Code : "";
            StoreRequisitionLineObj.LineShortcutDimension3Code = StoreRequisitionLineObj.LineShortcutDimension3Code != null ? StoreRequisitionLineObj.LineShortcutDimension3Code : "";
            StoreRequisitionLineObj.LineShortcutDimension4Code = StoreRequisitionLineObj.LineShortcutDimension4Code != null ? StoreRequisitionLineObj.LineShortcutDimension4Code : "";
            StoreRequisitionLineObj.LineShortcutDimension5Code = StoreRequisitionLineObj.LineShortcutDimension5Code != null ? StoreRequisitionLineObj.LineShortcutDimension5Code : "";
            StoreRequisitionLineObj.LineShortcutDimension6Code = StoreRequisitionLineObj.LineShortcutDimension6Code != null ? StoreRequisitionLineObj.LineShortcutDimension6Code : "";
            StoreRequisitionLineObj.LineShortcutDimension7Code = StoreRequisitionLineObj.LineShortcutDimension7Code != null ? StoreRequisitionLineObj.LineShortcutDimension7Code : "";
            StoreRequisitionLineObj.LineShortcutDimension8Code = StoreRequisitionLineObj.LineShortcutDimension8Code != null ? StoreRequisitionLineObj.LineShortcutDimension8Code : "";

            storeRequisitionLineModified = dynamicsNAVSOAPServices.inventoryManagementWS.ModifyStoreRequisitionLine(StoreRequisitionLineObj.LineNo,
                                                        StoreRequisitionLineObj.DocumentNo, StoreRequisitionLineObj.ItemNo, StoreRequisitionLineObj.Quantity,
                                                        StoreRequisitionLineObj.UOM, StoreRequisitionLineObj.ItemDescription, StoreRequisitionLineObj.LineLocationCode);
            return Json(new { StoreRequisitionLineModified = storeRequisitionLineModified }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult DeleteStoreRequisitionLine(int LineNo, string DocumentNo)
        {
            bool storeRequisitionLineDeleted = false;

            storeRequisitionLineDeleted = dynamicsNAVSOAPServices.inventoryManagementWS.DeleteStoreRequisitionLine(LineNo, DocumentNo);

            return Json(new { StoreRequisitionLineDeleted = storeRequisitionLineDeleted }, JsonRequestBehavior.AllowGet);
        }
        #endregion Store Requisition Line

        #region Store requisitions history
        [Authorize]
        public ActionResult StoreRequisitionHistory()
        {
            try
            {
                List<StoreRequisitionHeaderModel> storeRequisitionsList = new List<StoreRequisitionHeaderModel>();
                //	FinanceHomeController financeHomeController = new FinanceHomeController();

                var storeRequisitions = from storeRequisitionsQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitions
                                        where storeRequisitionsQuery.Employee_No.Equals(employeeNo)
                                        select storeRequisitionsQuery;

                foreach (StoreRequisitions storeRequisition in storeRequisitions)
                {
                    StoreRequisitionHeaderModel storeRequisitionObj = new StoreRequisitionHeaderModel();
                    storeRequisitionObj.No = storeRequisition.No;
                    storeRequisitionObj.DocumentDate = storeRequisition.Document_Date != null ? storeRequisition.Document_Date.Value.ToShortDateString() : "n/a";
                    storeRequisitionObj.RequiredDate = storeRequisition.Required_Date != null ? storeRequisition.Required_Date.Value.ToShortDateString() : "n/a";
                    storeRequisitionObj.LocationCode = storeRequisition.Location_Code;
                    storeRequisitionObj.RequesterID = storeRequisition.Requester_ID;
                    storeRequisitionObj.Description = storeRequisition.Description;
                    storeRequisitionObj.GlobalDimension1Code = storeRequisition.Global_Dimension_1_Code;
                    storeRequisitionObj.GlobalDimension2Code = storeRequisition.Global_Dimension_2_Code;
                    storeRequisitionObj.ShortcutDimension3Code = storeRequisition.Shortcut_Dimension_3_Code;
                    storeRequisitionObj.ShortcutDimension4Code = storeRequisition.Shortcut_Dimension_4_Code;
                    storeRequisitionObj.ShortcutDimension5Code = storeRequisition.Shortcut_Dimension_5_Code;
                    storeRequisitionObj.ShortcutDimension6Code = storeRequisition.Shortcut_Dimension_6_Code;
                    storeRequisitionObj.ShortcutDimension7Code = storeRequisition.Shortcut_Dimension_7_Code;
                    storeRequisitionObj.ShortcutDimension8Code = storeRequisition.Shortcut_Dimension_8_Code;
                    storeRequisitionObj.Amount = (storeRequisition.Amount ?? 0).ToString("N"); ;
                    storeRequisitionObj.Status = storeRequisition.Status;
                    storeRequisitionObj.EmployeeNo = storeRequisition.Employee_No;
                    storeRequisitionsList.Add(storeRequisitionObj);
                }
                return View(storeRequisitionsList);
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        [Authorize]
        public JsonResult GetStoreRequisitions()
        {
            List<StoreRequisitionHeaderModel> storeRequisitionsList = new List<StoreRequisitionHeaderModel>();
            //	FinanceHomeController financeHomeController = new FinanceHomeController();

            var storeRequisitions = from storeRequisitionsQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitions
                                    where storeRequisitionsQuery.Employee_No.Equals(employeeNo)
                                    select storeRequisitionsQuery;

            foreach (StoreRequisitions storeRequisition in storeRequisitions)
            {
                StoreRequisitionHeaderModel storeRequisitionObj = new StoreRequisitionHeaderModel();
                storeRequisitionObj.No = storeRequisition.No;
                storeRequisitionObj.DocumentDate = storeRequisition.Document_Date != null ? storeRequisition.Document_Date.Value.ToShortDateString() : "n/a";
                storeRequisitionObj.RequiredDate = storeRequisition.Required_Date != null ? storeRequisition.Required_Date.Value.ToShortDateString() : "n/a";
                storeRequisitionObj.EmployeeNo = storeRequisition.Employee_No;
                storeRequisitionObj.RequesterID = storeRequisition.Requester_ID;
                storeRequisitionObj.Description = storeRequisition.Description;
                storeRequisitionObj.GlobalDimension1Code = storeRequisition.Global_Dimension_1_Code;
                storeRequisitionObj.GlobalDimension2Code = storeRequisition.Global_Dimension_2_Code;
                storeRequisitionObj.ShortcutDimension3Code = storeRequisition.Shortcut_Dimension_3_Code;
                storeRequisitionObj.ShortcutDimension4Code = storeRequisition.Shortcut_Dimension_4_Code;
                storeRequisitionObj.ShortcutDimension5Code = storeRequisition.Shortcut_Dimension_5_Code;
                storeRequisitionObj.ShortcutDimension6Code = storeRequisition.Shortcut_Dimension_6_Code;
                storeRequisitionObj.ShortcutDimension7Code = storeRequisition.Shortcut_Dimension_7_Code;
                storeRequisitionObj.ShortcutDimension8Code = storeRequisition.Shortcut_Dimension_8_Code;
                storeRequisitionObj.Amount = (storeRequisition.Amount ?? 0).ToString("N"); ;
                storeRequisitionObj.Status = storeRequisition.Status;
                storeRequisitionsList.Add(storeRequisitionObj);
            }
            return Json(storeRequisitionsList, JsonRequestBehavior.AllowGet);
        }

        #endregion Store requisitions history

        #region Store Requisition Approval
        [Authorize]
        public ActionResult StoreRequisitionApproval(string StoreRequisitionNo)
        {
            bool StoreRequisitionExists = true;
            try
            {
                if (StoreRequisitionNo.Equals(""))
                {
                    return RedirectToAction("RequestsToApprove", "Approval");
                }
                if (StoreRequisitionExists)
                {
                    StoreRequisitionHeaderModel StoreRequisitionObj = new StoreRequisitionHeaderModel();

                    var storeRequisitions = from storeRequisitionsQuery in dynamicsNAVODataServices.dynamicsNAVOData.StoreRequisitions
                                            where storeRequisitionsQuery.No.Equals(StoreRequisitionNo)
                                            select storeRequisitionsQuery;

                    foreach (StoreRequisitions storeRequisition in storeRequisitions)
                    {
                        StoreRequisitionObj.No = storeRequisition.No;
                        StoreRequisitionObj.EmployeeNo = storeRequisition.Employee_No;
                        StoreRequisitionObj.DocumentDate = storeRequisition.Document_Date != null ? storeRequisition.Document_Date.Value.ToShortDateString() : "n/a";
                        StoreRequisitionObj.Amount = (storeRequisition.Amount ?? 0).ToString("N");
                        StoreRequisitionObj.Description = storeRequisition.Description;
                        StoreRequisitionObj.Status = storeRequisition.Status;
                        StoreRequisitionObj.GlobalDimension1Code = storeRequisition.Global_Dimension_1_Code;
                        StoreRequisitionObj.GlobalDimension2Code = storeRequisition.Global_Dimension_2_Code;
                        StoreRequisitionObj.ShortcutDimension3Code = storeRequisition.Shortcut_Dimension_3_Code;
                        StoreRequisitionObj.ShortcutDimension4Code = storeRequisition.Shortcut_Dimension_4_Code;
                        StoreRequisitionObj.ShortcutDimension5Code = storeRequisition.Shortcut_Dimension_5_Code;
                        StoreRequisitionObj.ShortcutDimension6Code = storeRequisition.Shortcut_Dimension_6_Code;
                        StoreRequisitionObj.ShortcutDimension7Code = storeRequisition.Shortcut_Dimension_7_Code;
                        StoreRequisitionObj.ShortcutDimension8Code = storeRequisition.Shortcut_Dimension_8_Code;
                        StoreRequisitionObj.ResponsibilityCenter = storeRequisition.Responsibility_Center;

                    }

                    LoadLocationCodes();

                    //StoreRequisitionObj.GlobalDimension1Codes = new SelectList(globalDimension1Values, "Code", "Name");
                    //StoreRequisitionObj.GlobalDimension2Codes = new SelectList(globalDimension2Values, "Code", "Name");
                    //StoreRequisitionObj.ShortcutDimension3Codes = new SelectList(shortcutDimension3Values, "Code", "Name");
                    //StoreRequisitionObj.ShortcutDimension4Codes = new SelectList(shortcutDimension4Values, "Code", "Name");
                    //StoreRequisitionObj.ShortcutDimension5Codes = new SelectList(shortcutDimension5Values, "Code", "Name");
                    //StoreRequisitionObj.ShortcutDimension6Codes = new SelectList(shortcutDimension6Values, "Code", "Name");
                    //StoreRequisitionObj.ShortcutDimension7Codes = new SelectList(shortcutDimension7Values, "Code", "Name");
                    //StoreRequisitionObj.ShortcutDimension8Codes = new SelectList(shortcutDimension8Values, "Code", "Name");
                    StoreRequisitionObj.LocationCodes = new SelectList(locations, "Code", "Code");

                    return View(StoreRequisitionObj);
                }
                else
                {
                    responseHeader = "Store Requisition NotFound";
                    responseMessage = "The store requisition no." + StoreRequisitionNo + " was not found for employee no." + AccountController.GetEmployeeNo();
                    detailedResponseMessage = "The store requisition no." + StoreRequisitionNo + " was not found for employee no." + AccountController.GetEmployeeNo();
                    returnControllerName = "Approval";
                    returnActionName = "RequestsToApprove";
                    returnLinkName = "Ok";
                    hasParameters = false;
                    parameters = "";
                    return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
                                                           returnControllerName, returnActionName, returnLinkName,
                                                           hasParameters, parameters);
                }
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreRequisitionApproval(StoreRequisitionHeaderModel StoreRequisitionObj, string Command)
        {
            try
            {
                if (StoreRequisitionObj.No.Equals(""))
                {
                    return RedirectToAction("RequestsToApprove", "Approval");
                }
                if (Command == "Approve")
                {
                    if (dynamicsNAVSOAPServices.ApprovalsMgmt.ApproveStoreRequisition(employeeNo, StoreRequisitionObj.No))
                    {
                        responseHeader = "Success";
                        responseMessage = "Store Requisition no." + StoreRequisitionObj.No + " was successfully approved.";
                        detailedResponseMessage = "Store Requisition no." + StoreRequisitionObj.No + " was successfully approved.";
                        returnControllerName = "Approval";
                        returnActionName = "RequestsToApprove";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return successResponse.ApplicationSuccess(responseHeader, responseMessage,
                                    detailedResponseMessage, returnControllerName, returnActionName,
                                    returnLinkName, hasParameters, parameters);
                    }
                    else
                    {
                        StoreRequisitionObj.ErrorStatus = true;
                        StoreRequisitionObj.ErrorMessage = "Unable to process the Store Requisition approve action. Contact the " + companyName + " for assistance.";
                        return View(StoreRequisitionObj);
                    }
                }
                else if (Command == "Reject")
                {
                    if (dynamicsNAVSOAPServices.ApprovalsMgmt.RejectStoreRequisition(employeeNo, StoreRequisitionObj.No))
                    {
                        responseHeader = "Success";
                        responseMessage = "Store Requisition no." + StoreRequisitionObj.No + " was successfully rejected.";
                        detailedResponseMessage = "Store Requisition no." + StoreRequisitionObj.No + " was successfully rejected.";
                        returnControllerName = "Approval";
                        returnActionName = "RequestsToApprove";
                        returnLinkName = "Ok";
                        hasParameters = false;
                        parameters = "";
                        return successResponse.ApplicationSuccess(responseHeader, responseMessage,
                                    detailedResponseMessage, returnControllerName, returnActionName,
                                    returnLinkName, hasParameters, parameters);
                    }
                    else
                    {
                        StoreRequisitionObj.ErrorStatus = true;
                        StoreRequisitionObj.ErrorMessage = "Unable to process the Store Requisition reject action. Contact the " + companyName + " for assistance.";
                        return View(StoreRequisitionObj);
                    }
                }
                else
                {
                    StoreRequisitionObj.ErrorStatus = true;
                    StoreRequisitionObj.ErrorMessage = "Unable to process the approve/reject action. Contact the " + companyName + " for assistance.";

                    return View(StoreRequisitionObj);
                }
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        #endregion Store Requisition Approval

        #region Portal Documents

        [ChildActionOnly]
        [Authorize]
        public ActionResult _StoreRequisitionDocument(string DocumentNo)
        {
            PortalDocumentsModel portalDocumentObj = new PortalDocumentsModel();

            var portalDocuments = from portalDocumentQuery in dynamicsNAVODataServices.dynamicsNAVOData.PortalDocuments
                                  where portalDocumentQuery.DocumentNo.Equals(DocumentNo)
                                  select portalDocumentQuery;

            foreach (PortalDocuments portalDocument in portalDocuments)
            {
                portalDocumentObj.DocumentNo = portalDocument.DocumentNo;
                portalDocumentObj.DocumentCode = portalDocument.Document_Code;
                portalDocumentObj.DocumentDescription = portalDocument.Document_Description;
                portalDocumentObj.DocumentAttached = portalDocument.Document_Attached ?? false;
                portalDocumentObj.LocalURL = portalDocument.Local_File_URL;
                portalDocumentObj.SharePointURL = portalDocument.SharePoint_URL;
            }

            return PartialView(portalDocumentObj);
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult _ViewStoreRequisitionDocument(string DocumentNo)
        {
            PortalDocumentsModel portalDocumentObj = new PortalDocumentsModel();

            var portalDocuments = from portalDocumentQuery in dynamicsNAVODataServices.dynamicsNAVOData.PortalDocuments
                                  where portalDocumentQuery.DocumentNo.Equals(DocumentNo)
                                  select portalDocumentQuery;

            foreach (PortalDocuments portalDocument in portalDocuments)
            {
                portalDocumentObj.DocumentNo = portalDocument.DocumentNo;
                portalDocumentObj.DocumentCode = portalDocument.Document_Code;
                portalDocumentObj.DocumentDescription = portalDocument.Document_Description;
                portalDocumentObj.DocumentAttached = portalDocument.Document_Attached ?? false;
                portalDocumentObj.LocalURL = portalDocument.Local_File_URL;
                portalDocumentObj.SharePointURL = portalDocument.SharePoint_URL;
            }

            return PartialView(portalDocumentObj);
        }

        [Authorize]
        public JsonResult LoadStoreRequisitionDocuments(string DocumentNo)
        {
            List<PortalDocumentsModel> portalDocumentsList = new List<PortalDocumentsModel>();

            var portalDocuments = from portalDocumentQuery in dynamicsNAVODataServices.dynamicsNAVOData.PortalDocuments
                                  where portalDocumentQuery.DocumentNo.Equals(DocumentNo)
                                  select portalDocumentQuery;

            foreach (PortalDocuments portalDocument in portalDocuments)
            {
                PortalDocumentsModel portalDocumentObj = new PortalDocumentsModel();
                portalDocumentObj.DocumentNo = portalDocument.DocumentNo;
                portalDocumentObj.DocumentCode = portalDocument.Document_Code;
                portalDocumentObj.DocumentDescription = portalDocument.Document_Description;
                portalDocumentObj.DocumentAttached = portalDocument.Document_Attached ?? false;
                portalDocumentObj.LocalURL = portalDocument.Local_File_URL;
                portalDocumentObj.SharePointURL = portalDocument.SharePoint_URL;
                portalDocumentsList.Add(portalDocumentObj);
            }

            return Json(portalDocumentsList, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult GetStoreRequisitionDocument(string DocumentNo, string DocumentCode)
        {
            try
            {
                PortalDocumentsModel portalDocumentObj = new PortalDocumentsModel();

                var portalDocuments = from portalDocumentQuery in dynamicsNAVODataServices.dynamicsNAVOData.PortalDocuments
                                      where portalDocumentQuery.DocumentNo.Equals(DocumentNo)
                                      select portalDocumentQuery;

                foreach (PortalDocuments portalDocument in portalDocuments)
                {
                    portalDocumentObj.DocumentNo = portalDocument.DocumentNo;
                    portalDocumentObj.DocumentCode = portalDocument.Document_Code;
                    portalDocumentObj.DocumentDescription = portalDocument.Document_Description;
                    portalDocumentObj.DocumentAttached = portalDocument.Document_Attached ?? false;
                    portalDocumentObj.LocalURL = portalDocument.Local_File_URL;
                    portalDocumentObj.SharePointURL = portalDocument.SharePoint_URL;
                }

                return Json(portalDocumentObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return errorResponse.ApplicationExceptionError(ex);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult UploadStoreRequisitionAttachments(string DocumentNo, string DocumentCode, string DocumentDescription)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = "~/StaffData/" + employeeNo;
                    bool folderpath = System.IO.Directory.Exists(HttpContext.Server.MapPath(root));

                    if (!folderpath)
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Server.MapPath(root));
                    }

                    var file = Request.Files[0];
                    string fileExt = System.IO.Path.GetExtension(file.FileName).ToLower();
                    string fileName = DocumentNo + "_" + DocumentDescription + fileExt;
                    string path = System.IO.Path.Combine(HttpContext.Server.MapPath(root), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    file.SaveAs(path);

                    if (System.IO.File.Exists(path))
                    {
                        dynamicsNAVSOAPServices.inventoryManagementWS.ModifyPortalDocumentURL(DocumentNo, DocumentCode, path);

                        return Json(new { success = true, message = DocumentDescription + " uploaded successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = DocumentDescription + " was not uploaded. Try Again." }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = false, message = "Please select a pdf file!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Portal Documents

        #region Helper Functions
        public JsonResult GetStoreRequisitionAmount(string DocumentNo)
        {
            decimal storeRequisitionAmount = 0;
            storeRequisitionAmount = dynamicsNAVSOAPServices.inventoryManagementWS.GetStoreRequisitionAmount(DocumentNo);
            return Json(new { Amount = storeRequisitionAmount }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAvailableInventory(string ItemNo, string UOM, string LocationCode)
        {
            decimal availableInventory = 0;
            availableInventory = dynamicsNAVSOAPServices.inventoryManagementWS.GetAvailableInventory(ItemNo, UOM, LocationCode);
            return Json(new { AvailableInventory = availableInventory }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ValidateQuantityRequested(string ItemNo, string UOM, string LocationCode, decimal Quantity)
        {
            decimal availableInventory = 0;

            availableInventory = dynamicsNAVSOAPServices.inventoryManagementWS.GetAvailableInventory(ItemNo, UOM, LocationCode);

            if (Quantity < 1)
            {
                return Json(new { success = false, message = "The quantity requested cannot be more than the current inventory for item no. " + ItemNo + " in location. " + LocationCode + " " }, JsonRequestBehavior.AllowGet);
            }

            if (Quantity > availableInventory)
            {
                return Json(new { success = false, message = "The quantity requested cannot be more than the available inventory" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetItemUOMs(string ItemNo)
        {
            var itemUOMs = from itemUOMsQuery in dynamicsNAVODataServices.dynamicsNAVOData.ItemUOMs
                           where itemUOMsQuery.Item_No.Equals(ItemNo)
                           select itemUOMsQuery;

            return Json(itemUOMs.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string GetStoreRequisitionStatus(string DocumentNo)
        {
            return dynamicsNAVSOAPServices.inventoryManagementWS.GetStoreRequisitionStatus(DocumentNo);
        }

        private void LoadStoreRequisitionDimensions(string GlobalDimension1Code)
        {
            globalDimension1Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                     where dimensionValuesQuery.Global_Dimension_No.Equals(1) && dimensionValuesQuery.Blocked.Equals(false)
                                     select dimensionValuesQuery;
            globalDimension2Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                     where dimensionValuesQuery.Global_Dimension_No.Equals(2) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                     select dimensionValuesQuery;
            shortcutDimension3Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                       where dimensionValuesQuery.Global_Dimension_No.Equals(3) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                       select dimensionValuesQuery;
            shortcutDimension4Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                       where dimensionValuesQuery.Global_Dimension_No.Equals(4) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                       select dimensionValuesQuery;
            shortcutDimension5Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                       where dimensionValuesQuery.Global_Dimension_No.Equals(5) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                       select dimensionValuesQuery;
            shortcutDimension6Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                       where dimensionValuesQuery.Global_Dimension_No.Equals(6) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                       select dimensionValuesQuery;
            shortcutDimension7Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                       where dimensionValuesQuery.Global_Dimension_No.Equals(7) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                       select dimensionValuesQuery;
            shortcutDimension8Values = from dimensionValuesQuery in dynamicsNAVODataServices.dynamicsNAVOData.DimensionValues
                                       where dimensionValuesQuery.Global_Dimension_No.Equals(8) && dimensionValuesQuery.Global_Dimension_1_Code.Equals(GlobalDimension1Code) && dimensionValuesQuery.Blocked.Equals(false)
                                       select dimensionValuesQuery;
        }

        private void LoadLocationCodes()
        {
            locations = from locationsQuery in dynamicsNAVODataServices.dynamicsNAVOData.Locations
                        where locationsQuery.Use_As_In_Transit.Equals(false)
                        select locationsQuery;
        }

        private void LoadItems()
        {
            items = from itemsQuery in dynamicsNAVODataServices.dynamicsNAVOData.Items
                    where itemsQuery.Blocked.Equals(false)
                    select itemsQuery;
        }

        private void LoadItemUOMs()
        {
            itemUOMs = from itemUOMsQuery in dynamicsNAVODataServices.dynamicsNAVOData.ItemUOMs
                       select itemUOMsQuery;
        }

        private void LoadItemUOMs(string ItemNo)
        {
            itemUOMs = from itemUOMsQuery in dynamicsNAVODataServices.dynamicsNAVOData.ItemUOMs
                       where itemUOMsQuery.Item_No.Equals(ItemNo)
                       select itemUOMsQuery;
        }

        #endregion Helper Functions
    }
}