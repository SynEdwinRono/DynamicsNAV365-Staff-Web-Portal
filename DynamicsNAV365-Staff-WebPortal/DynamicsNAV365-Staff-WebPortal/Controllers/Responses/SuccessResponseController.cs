using DynamicsNAV365_Staff_WebPortal.Models.Responses;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.Responses
{
	public class SuccessResponseController : Controller
    {
		#region Success Response
		public ActionResult ApplicationSuccess(string ResponseHeader, string ResponseMessage, string DetailedResponseMessage, string Button1ControllerName, string Button1ActionName, bool Button1HasParameters, string Button1Parameters, string Button1Name, string Button2ControllerName, string Button2ActionName, bool Button2HasParameters, string Button2Parameters, string Button2Name)
		{
			SuccessResponseModel successResponseObj = new SuccessResponseModel();
			successResponseObj.ResponseType = "SUCCESS";
			successResponseObj.ResponseHeader = ResponseHeader;
			successResponseObj.ResponseMessage = ResponseMessage;
			successResponseObj.DetailedResponseMessage = DetailedResponseMessage;

			successResponseObj.Button1ControllerName = Button1ControllerName;
			successResponseObj.Button1ActionName = Button1ActionName;
			successResponseObj.Button1HasParameters = Button1HasParameters;
			successResponseObj.Button1Parameters = Button1Parameters;
			successResponseObj.Button1Name = Button1Name;

			successResponseObj.Button2ControllerName = Button2ControllerName;
			successResponseObj.Button2ActionName = Button2ActionName;
			successResponseObj.Button2HasParameters = Button2HasParameters;
			successResponseObj.Button2Parameters = Button2Parameters;
			successResponseObj.Button2Name = Button2Name;
			return View("SuccessResponse", successResponseObj);
		}
		#endregion Success Response
	}
}