using CaptchaMvc.HtmlHelpers;
using DynamicsNAV365_Staff_WebPortal.Controllers.Responses;
using DynamicsNAV365_Staff_WebPortal.DynamicsNAVODataServiceReference;
using DynamicsNAV365_Staff_WebPortal.Models.Account;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DynamicsNAV365_Staff_WebPortal.Controllers
{
	public class AccountController : Controller
    {
		static string companyURL = "";
		static string contactCustomerCareDesk = "Contact the " + ServiceConnection.CompanyName + " customer service help desk via:" + ServiceConnection.CompanySupportEmail + " for assistance.";

		DynamicsNAVSOAPServices dynamicsNAVSOAPServices = new DynamicsNAVSOAPServices(companyURL);
		DynamicsNAVODATAServices dynamicsNAVODataServices = new DynamicsNAVODATAServices(companyURL);

		SuccessResponseController successResponse = new SuccessResponseController();
		InfoResponseController infoResponse = new InfoResponseController();
		ErrorResponseController errorResponse = new ErrorResponseController();

		private string responseHeader = "";
		private string responseMessage = "";
		private string detailedResponseMessage = "";

		private string button1ControllerName = "";
		private string button1ActionName = "";
		private bool button1HasParameters = false;
		private string button1Parameters = "";
		private string button1Name = "";

		private string button2ControllerName = "";
		private string button2ActionName = "";
		private bool button2HasParameters = false;
		private string button2Parameters = "";
		private string button2Name = "";

		public AccountController()
		{

		}

		#region Login
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel loginModel, string returnURL)
		{
			if (ModelState.IsValid)
			{
				loginModel.ErrorStatus = false;

				try
				{
					string customerNo = loginModel.CustomerNo;
					string customerPassword = Cryptography.Hash(loginModel.Password);

					//If customer does not exist
					if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeExists(customerNo))
					{
						loginModel.ErrorStatus = true;
						loginModel.ErrorMessage = "The customer account no. " + customerNo + " was not found." + contactCustomerCareDesk;
						return View(loginModel);
					}
					//If customer account is not active
					if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeAccountIsActive(customerNo))
					{
						loginModel.ErrorStatus = true;
						loginModel.ErrorMessage = "The customer account no. " + customerNo + " is inactive." + contactCustomerCareDesk;
						return View(loginModel);
					}
					if (dynamicsNAVSOAPServices.employeeAccountWS.LoginEmployee(customerNo, customerPassword))  //Login Customer
					{
						int timeout = 30;
						var ticket = new FormsAuthenticationTicket(customerNo, false, timeout);
						string encryptedTicket = FormsAuthentication.Encrypt(ticket);
						var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
						cookie.Expires = DateTime.Now.AddMinutes(timeout);
						cookie.HttpOnly = true;
						Response.Cookies.Add(cookie);

						if (Url.IsLocalUrl(returnURL))
						{
							return Redirect(returnURL);
						}
						else
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else //Customer login unsuccessful
					{
						loginModel.ErrorStatus = true;
						loginModel.ErrorMessage = "Invalid password provided.";
					}
				}
				catch (Exception ex)
				{
					return errorResponse.ApplicationExceptionError(ex);
				}
			}
			return View(loginModel);
		}
		#endregion Login

		#region Password Reset
		public ActionResult SendPasswordResetLink()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SendPasswordResetLink(SendPasswordResetLinkModel passwordResetLinkObj)
		{
			//Math or Char capcha
			if (!this.IsCaptchaValid("Invalid Answer"))
			{
				ViewBag.CaptchaErrorMessage = "Invalid Answer. Please Verify that you are not a robot.";
				return View(passwordResetLinkObj);
			}
			//End Math or Char capcha

			if (ModelState.IsValid)
			{
				try
				{
					string customerNo = passwordResetLinkObj.CustomerNo;
					string customerEmailAddress = dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeEmailAddress(customerNo);

					//If customer does not exist
					if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeExists(customerNo))
					{
						passwordResetLinkObj.ErrorStatus = true;
						passwordResetLinkObj.ErrorMessage = "The customer account no. " + customerNo + " was not found. " + contactCustomerCareDesk;
						return View(passwordResetLinkObj);
					}
					//If customer account is not active
					if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeAccountIsActive(customerNo))
					{
						passwordResetLinkObj.ErrorStatus = true;
						passwordResetLinkObj.ErrorMessage = "The customer account no. " + customerNo + " is inactive. " + contactCustomerCareDesk;
						return View(passwordResetLinkObj);
					}
					//If the email address is empty
					if (customerEmailAddress.Equals(""))
					{
						passwordResetLinkObj.ErrorStatus = true;
						passwordResetLinkObj.ErrorMessage = "The email address for the customer account no. " + customerNo + " is empty. " + contactCustomerCareDesk;
						return View(passwordResetLinkObj);
					}
					//Generate Password Reset Token
					Random rnd = new Random();
					int prefix = rnd.Next(10000, 1000000);
					int surfix = rnd.Next(10000, 1000000);
					string passwordResetToken = Cryptography.Hash(prefix.ToString() + customerNo + surfix.ToString());

					//Save the password reset token
					dynamicsNAVSOAPServices.employeeAccountWS.SetPasswordResetToken(customerNo, passwordResetToken);

					//Create Email Body
					var callbackUrl = Url.Action("ResetEmployeePassword", "Account", new { CustomerNo = customerNo, PasswordResetToken = passwordResetToken }, "http");
					var linkHref = "<a href='" + callbackUrl + "' class='btn btn-primary'><strong>Generate New Password</strong></a>";

					string emailBody = "<p>You recently requested to reset your password for your " + ServiceConnection.CompanyName + " staff account no. " + customerNo + ". Click the link below to reset it.</p>";
					emailBody += "<p>" + linkHref + "</p>";
					emailBody += "<p><b><i>Note that this link will expire after 24hrs</i></b></p>";
					//End Create Email Body

					if (dynamicsNAVSOAPServices.employeeAccountWS.SendPasswordResetLink(customerNo, emailBody))
					{
						responseHeader = "Password Reset Link Sent";
						responseMessage = "A password reset link has been sent to your registered customer email address (" + customerEmailAddress + "). Note that the link will expire after 24hrs." +
										  "If you did not get the email, " + contactCustomerCareDesk;
						detailedResponseMessage = "A password reset link has been sent to your registered customer email address (" + customerEmailAddress + "). Note that the link will expire after 24hrs." +
												  "If you did not get the email, " + contactCustomerCareDesk;
						button1ControllerName = "Account";
						button1ActionName = "Logout";
						button1HasParameters = false;
						button1Parameters = "";
						button1Name = "Ok";

						button2ControllerName = "";
						button2ActionName = "";
						button2HasParameters = false;
						button2Parameters = "";
						button2Name = "";
						return successResponse.ApplicationSuccess(responseHeader, responseMessage, detailedResponseMessage,
																  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
																  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
					}
					else
					{
						passwordResetLinkObj.ErrorStatus = true;
						passwordResetLinkObj.ErrorMessage = "Unable to send the password reset link to email address(" + customerEmailAddress + "). " + contactCustomerCareDesk;
						return View(passwordResetLinkObj);
					}
				}
				catch (Exception ex)
				{
					return errorResponse.ApplicationExceptionError(ex);
				}
			}
			return View(passwordResetLinkObj);
		}

		public ActionResult ResetEmployeePassword(string CustomerNo, string PasswordResetToken)
		{
			try
			{
				//If customer no. is empty
				if (CustomerNo.Equals(""))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "Customer no. was not provided.";
					detailedResponseMessage = "Customer no. was not provided.";

					button1ControllerName = "Account";
					button1ActionName = "SendPasswordResetLink";
					button1HasParameters = false;
					button1Parameters = "";
					button1Name = "Send New Password Reset Link";

					button2ControllerName = "Account";
					button2ActionName = "Logout";
					button2HasParameters = false;
					button2Parameters = "";
					button2Name = "Cancel";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
														  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
				}
				//If password reset token is empty
				if (PasswordResetToken.Equals(""))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The password reset security token was not provided.";
					detailedResponseMessage = "The password reset security token was not provided.";

					button1ControllerName = "Account";
					button1ActionName = "SendPasswordResetLink";
					button1HasParameters = true;
					button1Parameters = "?CustomerNo=" + CustomerNo;
					button1Name = "Send New Password Reset Link";

					button2ControllerName = "Account";
					button2ActionName = "Logout";
					button2HasParameters = false;
					button2Parameters = "";
					button2Name = "Cancel";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
														  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
				}
				//If customer does not exist
				if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeExists(CustomerNo))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The customer account no. " + CustomerNo + " was not found.";
					detailedResponseMessage = "The customer account no. " + CustomerNo + " was not found.";

					button1ControllerName = "Account";
					button1ActionName = "SendPasswordResetLink";
					button1HasParameters = true;
					button1Parameters = "?CustomerNo=" + CustomerNo;
					button1Name = "Send New Password Reset Link";

					button2ControllerName = "Account";
					button2ActionName = "Logout";
					button2HasParameters = false;
					button2Parameters = "";
					button2Name = "Cancel";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
														  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
				}
				//If customer account is not active
				if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeAccountIsActive(CustomerNo))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The customer account no. " + CustomerNo + " is inactive. " + contactCustomerCareDesk;
					detailedResponseMessage = "The customer account no. " + CustomerNo + " is inactive. " + contactCustomerCareDesk;

					button1ControllerName = "Account";
					button1ActionName = "Logout";
					button1HasParameters = false;
					button1Parameters = "";
					button1Name = "Ok";

					button2ControllerName = "";
					button2ActionName = "";
					button2HasParameters = false;
					button2Parameters = "";
					button2Name = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
														  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
				}
				//If password reset security token is invalid
				if (!dynamicsNAVSOAPServices.employeeAccountWS.GetPasswordResetToken(CustomerNo).Equals(PasswordResetToken))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The provided password reset security token is invalid.";
					detailedResponseMessage = "The provided password reset security token is invalid.";

					button1ControllerName = "Account";
					button1ActionName = "SendPasswordResetLink";
					button1HasParameters = true;
					button1Parameters = "?CustomerNo=" + CustomerNo;
					button1Name = "Send New Password Reset Link";

					button2ControllerName = "Account";
					button2ActionName = "Logout";
					button2HasParameters = false;
					button2Parameters = "";
					button2Name = "Cancel";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
														  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
				}
				//If password reset security token is expired
				if (dynamicsNAVSOAPServices.employeeAccountWS.IsPasswordResetTokenExpired(CustomerNo, PasswordResetToken))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The provided password reset security token is expired.";
					detailedResponseMessage = "The provided password reset security token is expired.";

					button1ControllerName = "Account";
					button1ActionName = "SendPasswordResetLink";
					button1HasParameters = true;
					button1Parameters = "?CustomerNo=" + CustomerNo;
					button1Name = "Send New Password Reset Link";

					button2ControllerName = "Account";
					button2ActionName = "Logout";
					button2HasParameters = false;
					button2Parameters = "";
					button2Name = "Cancel";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
														  button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
				}
				PasswordResetModel passwordResetModel = new PasswordResetModel();
				passwordResetModel.CustomerNo = CustomerNo;
				passwordResetModel.PasswordResetToken = PasswordResetToken;
				return View(passwordResetModel);
			}
			catch (Exception ex)
			{
				return errorResponse.ApplicationExceptionError(ex);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ResetEmployeePassword(PasswordResetModel PasswordResetObj)
		{
			//Math or Char captha
			if (!this.IsCaptchaValid("Invalid Answer"))
			{
				ViewBag.CaptchaErrorMessage = "Invalid Answer. Please Verify that you are not a robot";
				return View(PasswordResetObj);
			}
			//End Math or Char captha

			if (ModelState.IsValid)
			{
				try
				{
					//If password reset token is empty
					if (PasswordResetObj.PasswordResetToken.Equals(""))
					{
						PasswordResetObj.ErrorStatus = true;
						PasswordResetObj.ErrorMessage = "The password reset security token was not provided.";
						return View(PasswordResetObj);
					}

					//If customer does not exist
					if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeExists(PasswordResetObj.CustomerNo))
					{
						PasswordResetObj.ErrorStatus = true;
						PasswordResetObj.ErrorMessage = "The customer account no. " + PasswordResetObj.CustomerNo + " was not found. " + contactCustomerCareDesk;
						return View(PasswordResetObj);
					}

					//If customer account is not active
					if (!dynamicsNAVSOAPServices.employeeAccountWS.EmployeeAccountIsActive(PasswordResetObj.CustomerNo))
					{
						PasswordResetObj.ErrorStatus = true;
						PasswordResetObj.ErrorMessage = "The customer account no. " + PasswordResetObj.CustomerNo + " is inactive. " + contactCustomerCareDesk;
						return View(PasswordResetObj);
					}

					//If password reset security token is invalid
					if (!dynamicsNAVSOAPServices.employeeAccountWS.GetPasswordResetToken(PasswordResetObj.CustomerNo).Equals(PasswordResetObj.PasswordResetToken))
					{
						PasswordResetObj.ErrorStatus = true;
						PasswordResetObj.ErrorMessage = "The provided password reset security token is invalid.";
						return View(PasswordResetObj);
					}
					//If password reset security token is expired
					if (dynamicsNAVSOAPServices.employeeAccountWS.IsPasswordResetTokenExpired(PasswordResetObj.CustomerNo, PasswordResetObj.PasswordResetToken))
					{
						PasswordResetObj.ErrorStatus = true;
						PasswordResetObj.ErrorMessage = "The provided password reset security token is expired.";
						return View(PasswordResetObj);
					}
					//Update the customer password
					if (dynamicsNAVSOAPServices.employeeAccountWS.ResetEmployeePortalPassword(PasswordResetObj.CustomerNo, Cryptography.Hash(PasswordResetObj.Password)))
					{
						responseHeader = "Password reset successful";
						responseMessage = "The password for your customer account no. " + PasswordResetObj.CustomerNo + " at " + ServiceConnection.CompanyName + " was successfully reset. Click ok to proceed to Login.";
						detailedResponseMessage = "The password for your customer account no. " + PasswordResetObj.CustomerNo + " at " + ServiceConnection.CompanyName + " was successfully reset. Click ok to proceed to Login.";

						button1ControllerName = "Account";
						button1ActionName = "Logout";
						button1HasParameters = false;
						button1Parameters = "";
						button1Name = "Ok";

						button2ControllerName = "";
						button2ActionName = "";
						button2HasParameters = false;
						button2Parameters = "";
						button2Name = "";
						return successResponse.ApplicationSuccess(responseHeader, responseMessage, detailedResponseMessage,
													button1ControllerName, button1ActionName, button1HasParameters, button1Parameters, button1Name,
													button2ControllerName, button2ActionName, button2HasParameters, button2Parameters, button2Name);
					}
				}
				catch (Exception ex)
				{
					return errorResponse.ApplicationExceptionError(ex);
				}
			}
			return View(PasswordResetObj);
		}

		#endregion Password Reset

		#region Employee Profile
		[Authorize]
		public ActionResult EmployeeProfile()
		{
			try
			{
				EmployeeProfileModel employeeProfileModel = new EmployeeProfileModel();
				var employees = from employeeQuery in dynamicsNAVODataServices.dynamicsNAVOData.Employees
								where employeeQuery.No.Equals(GetEmployeeNo())
								select employeeQuery;

				foreach (Employees employee in employees)
				{
					employeeProfileModel.No = employee.No;
					employeeProfileModel.EmployeeName = employee.Full_Name;
					employeeProfileModel.DateOfBirth = employee.Birth_Date.Value.ToString("dd-MM-yyyy");
					employeeProfileModel.Gender = employee.Gender;
					employeeProfileModel.MartialStatus = employee.Marital_Status;
					employeeProfileModel.Citizenship = employee.Citizenship;
					employeeProfileModel.Religion = employee.Religion;
					employeeProfileModel.PhoneNumber = employee.Phone_No;
					employeeProfileModel.MobilePhoneNumber = employee.Mobile_Phone_No;
					employeeProfileModel.Address = employee.Address;
					employeeProfileModel.Address2 = employee.Address_2;
					employeeProfileModel.City = employee.City;
					employeeProfileModel.EmailAddress = employee.E_Mail;
					employeeProfileModel.WorkEmailAddress = employee.Company_E_Mail;
					employeeProfileModel.JobNumber = employee.Job_No;
					employeeProfileModel.JobTitle = employee.Job_Title;
					employeeProfileModel.JobGrade = employee.Job_Grade;
					employeeProfileModel.EmployementDate = employee.Employment_Date.Value.ToString("dd-MM-yyyy");
					employeeProfileModel.NationalIDNumber = employee.National_ID_No;
					employeeProfileModel.PINNumber = employee.PIN_No;
					employeeProfileModel.NSSFNumber = employee.NSSF_No;
					employeeProfileModel.NHIFNumber = employee.NHIF_No;
					//  employeeProfileModel.ProfessionalNumber = employee.Professional_License_No;
					// employeeProfileModel.ProfessionalLicenceExpiryDate = employee.Professional_License_Expiry.Value.ToString("dd-MM-yyyy");
					employeeProfileModel.BankName = employee.Bank_Name;
					employeeProfileModel.BankBranchName = employee.Bank_Branch_Name;
					employeeProfileModel.BankAccountNumber = employee.Bank_Account_No;
					employeeProfileModel.CountyName = employee.County_Name;
					employeeProfileModel.SubcountyName = employee.SubCounty_Name;
				}
				return View(employeeProfileModel);
			}
			catch (Exception ex)
			{
				return errorResponse.ApplicationExceptionError(ex);
			}

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EmployeeProfile(EmployeeProfileModel employeeProfileModel)
		{
			return View(employeeProfileModel);
		}

		#endregion Employee Profile

		#region Logout
		[Authorize]
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
		#endregion Logout

		#region Helper Views
		[ChildActionOnly]
		public ActionResult _EmployeeProfileSidebar()
		{
			EmployeeProfileModel employeeProfileModel = new EmployeeProfileModel();
			return PartialView(employeeProfileModel);
		}

		[ChildActionOnly]
		public ActionResult _AccountAttachments()
		{
			return PartialView();
		}
		#endregion Helper Views

		#region Helper Functions
		public static string GetEmployeeNo()
		{
			return System.Web.HttpContext.Current.User.Identity.Name;
		}
		public static string GetCleanCustomerNo()
		{
			return "";
		}
		[Authorize]
		public string GetDynamicsNAVEmployeeDirectoryPath(string CustomerNo)
		{
			string parentDirectoryName = "Org_Data";
			string childDirectoryName = "StaffData";
			CustomerNo = dynamicsNAVSOAPServices.employeeAccountWS.GetCleanedEmployeeNo(CustomerNo);
			return ServiceConnection.protocol + ServiceConnection.DynamicsNAVServer + "/" + parentDirectoryName + "/" + childDirectoryName + "/" + CustomerNo + "/" + CustomerNo;
		}

		#endregion Helper Functions
	}
}