using DynamicsNAV365_Staff_WebPortal.Models.Responses;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.Responses
{
    public class InfoResponseController : Controller
    {
        string companyName = ServiceConnection.CompanyName;

        #region Application Error
        public ActionResult ApplicationInfo(string responseHeader, string responseMessage, string detailedResponseMessage, string returnControllerName, string returnActionName, string returnLinkName, bool hasParameters, string parameters)
        {
            InfoResponseModel infoResponseModel = new InfoResponseModel();

            infoResponseModel.ResponseType = "INFO";
            infoResponseModel.ResponseHeader = responseHeader;
            infoResponseModel.ResponseMessage = responseMessage;
            infoResponseModel.DetailedResponseMessage = detailedResponseMessage;
            infoResponseModel.ReturnControllerName = returnControllerName;
            infoResponseModel.ReturnActionName = returnActionName;
            infoResponseModel.ReturnLinkName = returnLinkName;
            infoResponseModel.HasParameters = hasParameters;
            infoResponseModel.Parameters = parameters;
            infoResponseModel.CancelControllerName = "";
            infoResponseModel.CancelActionName = "";
            infoResponseModel.CancelLinkName = "";
            return View("InfoResponse", infoResponseModel);
        }

        public ActionResult ApplicationConfirm(string responseHeader, string responseMessage, string detailedResponseMessage, string returnControllerName, string returnActionName, string returnLinkName, bool hasParameters, string parameters, string cancelControllerName, string cancelActionName, string cancelLinkName)
        {
            InfoResponseModel infoResponseModel = new InfoResponseModel();

            infoResponseModel.ResponseType = "CONFIRM";
            infoResponseModel.ResponseHeader = responseHeader;
            infoResponseModel.ResponseMessage = responseMessage;
            infoResponseModel.DetailedResponseMessage = detailedResponseMessage;
            infoResponseModel.ReturnControllerName = returnControllerName;
            infoResponseModel.ReturnActionName = returnActionName;
            infoResponseModel.ReturnLinkName = returnLinkName;
            infoResponseModel.HasParameters = hasParameters;
            infoResponseModel.Parameters = parameters;
            infoResponseModel.CancelControllerName = cancelControllerName;
            infoResponseModel.CancelActionName = cancelActionName;
            infoResponseModel.CancelLinkName = cancelLinkName;
            return View("ConfirmResponse", infoResponseModel);
        }

        #endregion Application Error
    }
}