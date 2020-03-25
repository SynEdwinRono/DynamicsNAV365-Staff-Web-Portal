using DynamicsNAV365_Staff_WebPortal.Models.Responses;
using System;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.Responses
{
    public class ErrorResponseController : Controller
    {
        string companyName = ServiceConnection.CompanyName;

        #region Application Error
        public ActionResult ApplicationError(string responseHeader, string responseMessage, string detailedResponseMessage, string returnControllerName, string returnActionName, string returnLinkName, bool hasParameters, string parameters)
        {
            ErrorResponseModel errorResponseObj = new ErrorResponseModel();

            errorResponseObj.ResponseType = "ERROR";
            errorResponseObj.ResponseHeader = responseHeader;
            errorResponseObj.ResponseMessage = responseMessage;
            errorResponseObj.DetailedResponseMessage = detailedResponseMessage;
            errorResponseObj.ReturnControllerName = returnControllerName;
            errorResponseObj.ReturnActionName = returnActionName;
            errorResponseObj.ReturnLinkName = returnLinkName;
            errorResponseObj.HasParameters = hasParameters;
            errorResponseObj.Parameters = parameters;

            return View("ErrorResponse", errorResponseObj);
        }

        public ActionResult ApplicationExceptionError(Exception ex)
        {
            ErrorResponseModel errorResponseObj = new ErrorResponseModel();

            errorResponseObj.ResponseType = "ERROR";
            errorResponseObj.ResponseHeader = "Application Exception Error";
            errorResponseObj.ResponseMessage = ex.Message;
            errorResponseObj.DetailedResponseMessage = ex.Message;
            errorResponseObj.ReturnControllerName = "Home";
            errorResponseObj.ReturnActionName = "Index";
            errorResponseObj.ReturnLinkName = "Close";
            errorResponseObj.HasParameters = false;
            errorResponseObj.Parameters = "";

            return View("ErrorResponse", errorResponseObj);
        }

        #endregion Application Error

        #region Server Errors
        public ActionResult InternalServerError()
        {
            ErrorResponseModel errorResponseModel = new ErrorResponseModel();
            errorResponseModel.ResponseType = "ERROR";
            errorResponseModel.ResponseHeader = "500 Internal Server Error";
            errorResponseModel.ResponseMessage = "The staff portal is unable to connect to the server.<br />" +
                               "The server could be temporarily unavailable or too busy.Try again in a few minutes.";
            errorResponseModel.DetailedResponseMessage = "";
            errorResponseModel.ReturnControllerName = "Account";
            errorResponseModel.ReturnActionName = "Logout";
            errorResponseModel.ReturnLinkName = "Ok";
            return View(errorResponseModel);
        }
        public ActionResult GatewayTimeout()
        {
            ErrorResponseModel errorResponseObj = new ErrorResponseModel();
            errorResponseObj.ResponseType = "ERROR";
            errorResponseObj.ResponseHeader = "504 Gateway Timeout Server Error";
            errorResponseObj.ResponseMessage = "The staff portal is unable to connect to the server.<br />" +
                                               "The server could be temporarily unavailable or too busy.Try again in a few minutes.";
            errorResponseObj.DetailedResponseMessage = "";
            errorResponseObj.ReturnControllerName = "Account";
            errorResponseObj.ReturnActionName = "Logout";
            errorResponseObj.ReturnLinkName = "Ok";
            errorResponseObj.HasParameters = false;
            errorResponseObj.Parameters = "";

            return View("ErrorResponse", errorResponseObj);
        }
        #endregion Server Errors

    }
}