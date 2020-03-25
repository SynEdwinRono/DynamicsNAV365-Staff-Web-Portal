using DynamicsNAV365_Staff_WebPortal.Models.Responses;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.Responses
{
    public class SuccessResponseController : Controller
    {
        string companyName = ServiceConnection.CompanyName;

        #region Success Response
        public ActionResult ApplicationSuccess(string ResponseHeader, string ResponseMessage, string DetailedResponseMessage, string ReturnControllerName, string ReturnActionName, string ReturnLinkName, bool HasParameters, string Parameters)
        {
            SuccessResponseModel successResponseObj = new SuccessResponseModel();
            successResponseObj.ResponseType = "SUCCESS";
            successResponseObj.ResponseHeader = ResponseHeader;
            successResponseObj.ResponseMessage = ResponseMessage;
            successResponseObj.DetailedResponseMessage = DetailedResponseMessage;
            successResponseObj.ReturnControllerName = ReturnControllerName;
            successResponseObj.ReturnActionName = ReturnActionName;
            successResponseObj.ReturnLinkName = ReturnLinkName;
            successResponseObj.HasParameters = HasParameters;
            successResponseObj.Parameters = Parameters;

            return View("SuccessResponse", successResponseObj);
        }
        #endregion Success Response
    }
}