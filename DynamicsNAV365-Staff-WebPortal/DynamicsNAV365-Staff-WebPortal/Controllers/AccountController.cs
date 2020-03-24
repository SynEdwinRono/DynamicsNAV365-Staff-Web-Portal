using DynamicsNAV365_Staff_WebPortal.Controllers.Responses;
using DynamicsNAV365_Staff_WebPortal.Models.Account;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace DynamicsNAV365_Staff_WebPortal.Controllers
{
	public class AccountController : Controller
    {
		static string companyName = ServiceConnection.CompanyName;
		static string companyURL = "";

		DynamicsNAVSOAPServices dynamicsNAVSOAPServices = new DynamicsNAVSOAPServices(companyURL);
		DynamicsNAVODATAServices dynamicsNAVODataServices = new DynamicsNAVODATAServices(companyURL);
		SuccessResponseController successResponse = new SuccessResponseController();
		ErrorResponseController errorResponse = new ErrorResponseController();

		private string responseHeader = "";
		private string responseMessage = "";
		private string detailedResponseMessage = "";
		private string returnControllerName = "";
		private string returnActionName = "";
		private string returnLinkName = "";
		private bool hasParameters = false;
		private string parameters = "";

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
					string employeeNo = loginModel.EmployeeNo;
					string employeePassword = Cryptography.Hash(loginModel.Password);

					//If employee does not exist
					if (!CheckEmployeeExists(employeeNo))
					{
						loginModel.ErrorStatus = true;
						loginModel.ErrorMessage = "The employee no. " + employeeNo + " was not found. Contact the " + companyName + " human resource division for assistance.";
						return View(loginModel);
					}
					//If employee account is not active
					if (!CheckEmployeeAccountIsActive(employeeNo))
					{
						loginModel.ErrorStatus = true;
						loginModel.ErrorMessage = "The employee no. " + employeeNo + " is inactive. Contact the " + companyName + " human resource division for account activation.";
						return View(loginModel);
					}
					if (LoginEmployee(employeeNo, employeePassword))  //Employee login successful
					{
						int timeout = 30;
						var ticket = new FormsAuthenticationTicket(employeeNo, false, timeout);
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
					else //Employee login unsuccessful
					{
						loginModel.ErrorStatus = true;
						loginModel.ErrorMessage = "Invalid password provided";
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
		public ActionResult SendPasswordResetLink(SendPasswordResetLinkModel passwordResetLinkModel)
		{
			/*Google reCaptcha
            var response = Request["g-recaptcha-response"];
            string secretKey = ServiceConnection.googleReCaptchaKey;

            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            bool status = (bool)obj.SelectToken("success");
            if (!status)
            {
                ViewBag.CaptchaErrorMessage = "Please Verify that you are not a robot";
                return View(passwordResetLinkModel);
            }
            //End Google reCaptcha*/
			//Math or Char captha
			if (!this.IsCaptchaValid("Invalid Answer"))
			{
				ViewBag.CaptchaErrorMessage = "Invalid Answer. Please Verify that you are not a robot";
				return View(passwordResetLinkModel);
			}
			//End Math or Char captha
			if (ModelState.IsValid)
			{
				try
				{
					string employeeNo = passwordResetLinkModel.EmployeeNo;
					string employeeEmailAddress = GetEmployeeEmailAddress(employeeNo);
					//If employee does not exist
					if (!CheckEmployeeExists(employeeNo))
					{
						passwordResetLinkModel.ErrorStatus = true;
						passwordResetLinkModel.ErrorMessage = "The employee no. " + employeeNo + " was not found.";
						return View(passwordResetLinkModel);
					}
					//If employee account is inactive
					if (!CheckEmployeeAccountIsActive(employeeNo))
					{
						passwordResetLinkModel.ErrorStatus = true;
						passwordResetLinkModel.ErrorMessage = "The employee no. " + employeeNo + " is inactive. Contact the human resource division for account activation.";
						return View(passwordResetLinkModel);
					}
					//If the email address is empty
					if (employeeEmailAddress.Equals(""))
					{
						passwordResetLinkModel.ErrorStatus = true;
						passwordResetLinkModel.ErrorMessage = "The email address for the employee no. " + employeeNo + " is not setup. Contact the human resource division for email address update.";
						return View(passwordResetLinkModel);
					}
					//Generate Password Reset Token
					Random rnd = new Random();
					int prefix = rnd.Next(10000, 1000000);
					int surfix = rnd.Next(10000, 1000000);
					string passwordResetToken = Cryptography.Hash(prefix.ToString() + employeeNo + surfix.ToString());

					//Save the password reset token
					SetEmployeePasswordResetToken(employeeNo, passwordResetToken);

					//Create Email Body
					var callbackUrl = Url.Action("ResetEmployeePassword", "Account", new { employeeNo = employeeNo, passwordResetToken = passwordResetToken }, "http");
					var linkHref = "<a href='" + callbackUrl + "' class='btn btn-primary'>Set your password</a>";

					string emailBody = "<p>You recently requested to reset your password for your " + companyName + " employee account no. " + employeeNo + ". Click the link below to reset it.</p>";
					emailBody += "<p>" + linkHref + "</p>";
					emailBody += "<p><b>Note that this link will expire after 24hrs</b></p>";
					//End Create Email Body

					if (SendPasswordResetLink(employeeNo, emailBody))
					{
						responseHeader = "Password Reset Link Sent";
						responseMessage = "A password reset link has been sent to your registered employee email address (" + employeeEmailAddress + "). Note that the link will expire after 24hrs." +
										  "If you did not get the email, contact the " + companyName + " ICT division for assistance.";
						detailedResponseMessage = "";
						returnControllerName = "Account";
						returnActionName = "Logout";
						returnLinkName = "Ok";
						hasParameters = false;
						parameters = "";
						return successResponse.ApplicationSuccess(responseHeader, responseMessage, detailedResponseMessage,
													returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
					}
					else
					{
						passwordResetLinkModel.ErrorStatus = true;
						passwordResetLinkModel.ErrorMessage = "Unable to send the password reset link to email address(" + employeeEmailAddress + "). Contact the " + companyName + " ICT division for assistance.";
						return View(passwordResetLinkModel);
					}
				}
				catch (Exception ex)
				{
					return errorResponse.ApplicationExceptionError(ex);
				}
			}
			return View(passwordResetLinkModel);
		}

		public ActionResult ResetEmployeePassword(string employeeNo, string passwordResetToken)
		{
			try
			{
				//If employee number is empty
				if (employeeNo.Equals(""))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "Employee account no. was not provided";
					detailedResponseMessage = "";
					returnControllerName = "Account";
					returnActionName = "Logout";
					returnLinkName = "Ok";
					hasParameters = false;
					parameters = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
				}
				//If password reset token is empty
				if (passwordResetToken.Equals(""))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The password reset security token was not provided";
					detailedResponseMessage = "";
					returnControllerName = "Account";
					returnActionName = "Logout";
					returnLinkName = "Ok";
					hasParameters = false;
					parameters = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
				}
				//If employee no. does not exist
				if (!CheckEmployeeExists(employeeNo))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The employee no. " + employeeNo + " was not found. Contact the " + companyName + " human resource division for assistance.";
					detailedResponseMessage = "";
					returnControllerName = "Account";
					returnActionName = "Logout";
					returnLinkName = "Ok";
					hasParameters = false;
					parameters = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
				}
				//If employee account is inactive
				if (!CheckEmployeeAccountIsActive(employeeNo))
				{
					responseHeader = "Password Reset Error";
					responseMessage = "The employee no. " + employeeNo + " is inactive. Contact the " + companyName + " human resource division for account activation.";
					detailedResponseMessage = "";
					returnControllerName = "Account";
					returnActionName = "Logout";
					returnLinkName = "Ok";
					hasParameters = false;
					parameters = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
				}
				//If password reset security token is invalid
				if (!GetEmployeePasswordResetToken(employeeNo).Equals(passwordResetToken))
				{
					responseHeader = "Password Reset Failed";
					responseMessage = "The provided password reset security token is invalid. Click ok to generate a new password reset link.";
					detailedResponseMessage = "";
					returnControllerName = "Account";
					returnActionName = "SendPasswordResetLink";
					returnLinkName = "Ok";
					hasParameters = false;
					parameters = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
				}
				//If password reset security token is expired
				if (CheckPasswordResetTokenExpired(employeeNo, passwordResetToken))
				{
					responseHeader = "Password Reset Failed";
					responseMessage = "The provided password reset security token is expired. Click ok to generate a new password reset link.";
					detailedResponseMessage = "";
					returnControllerName = "Account";
					returnActionName = "SendPasswordResetLink";
					returnLinkName = "Ok";
					hasParameters = false;
					parameters = "";
					return errorResponse.ApplicationError(responseHeader, responseMessage, detailedResponseMessage,
														  returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
				}

				ResetPasswordModel passwordResetModel = new ResetPasswordModel();
				passwordResetModel.EmployeeNo = employeeNo;
				passwordResetModel.PasswordResetToken = passwordResetToken;
				return View(passwordResetModel);
			}
			catch (Exception ex)
			{
				return errorResponse.ApplicationExceptionError(ex);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ResetEmployeePassword(ResetPasswordModel passwordResetModel)
		{
			bool errorStatus = false;
			string errorMessage = "";

			/*Google reCaptcha
            var response = Request["g-recaptcha-response"];
            string secretKey = ServiceConnection.googleReCaptchaKey;
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            bool status = (bool)obj.SelectToken("success");
            if (!status)
            {
                ViewBag.CaptchaErrorMessage = "Please Verify that you are not a robot";
                return View(passwordResetModel);
            }
            //End Google reCaptcha*/
			//Math or Char captha
			if (!this.IsCaptchaValid("Invalid Answer"))
			{
				ViewBag.CaptchaErrorMessage = "Invalid Answer. Please Verify that you are not a robot";
				return View(passwordResetModel);
			}
			//End Math or Char captha

			if (ModelState.IsValid)
			{
				try
				{
					//If password reset token is empty
					if (passwordResetModel.PasswordResetToken.Equals(""))
					{
						errorStatus = true;
						errorMessage = "The password reset security token was not provided";
						passwordResetModel.ErrorStatus = errorStatus;
						passwordResetModel.ErrorMessage = errorMessage;
						return View(passwordResetModel);
					}
					//If employee does not exist
					if (!CheckEmployeeExists(passwordResetModel.EmployeeNo))
					{
						errorStatus = true;
						errorMessage = "The employee no. " + passwordResetModel.EmployeeNo + " was not found";
						passwordResetModel.ErrorStatus = errorStatus;
						passwordResetModel.ErrorMessage = errorMessage;
						return View(passwordResetModel);
					}
					//If employee account is inactive
					if (!CheckEmployeeAccountIsActive(passwordResetModel.EmployeeNo))
					{
						errorStatus = true;
						errorMessage = "The employee no. " + passwordResetModel.EmployeeNo + " is inactive. Contact the " + companyName + " human resource division for account activation.";
						passwordResetModel.ErrorStatus = errorStatus;
						passwordResetModel.ErrorMessage = errorMessage;
						return View(passwordResetModel);
					}
					//If password reset security token is not valid
					if (!GetEmployeePasswordResetToken(passwordResetModel.EmployeeNo).Equals(passwordResetModel.PasswordResetToken))
					{
						errorStatus = true;
						errorMessage = "The provided password reset security token is invalid.";
						passwordResetModel.ErrorStatus = errorStatus;
						passwordResetModel.ErrorMessage = errorMessage;
						return View(passwordResetModel);
					}
					//If password reset security token is expired
					if (CheckPasswordResetTokenExpired(passwordResetModel.EmployeeNo, passwordResetModel.PasswordResetToken))
					{
						errorStatus = true;
						errorMessage = "The provided password reset security token is expired.";
						passwordResetModel.ErrorStatus = errorStatus;
						passwordResetModel.ErrorMessage = errorMessage;
						return View(passwordResetModel);
					}
					//Update the employee password
					if (ResetEmployeePortalPassword(passwordResetModel.EmployeeNo, Cryptography.Hash(passwordResetModel.Password)))
					{
						responseHeader = "Password reset successful";
						responseMessage = "The password for your employee no. " + passwordResetModel.EmployeeNo + " at " + companyName + " was successfully reset. Click ok to proceed to Login.";
						detailedResponseMessage = "";
						returnControllerName = "Account";
						returnActionName = "Logout";
						returnLinkName = "Ok";
						hasParameters = false;
						parameters = "";
						return successResponse.ApplicationSuccess(responseHeader, responseMessage, detailedResponseMessage,
													returnControllerName, returnActionName, returnLinkName, hasParameters, parameters);
					}
				}
				catch (Exception ex)
				{
					return errorResponse.ApplicationExceptionError(ex);
				}
			}
			return View(passwordResetModel);
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
			employeeProfileModel.PassportAttached = false;
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
		public string GetEmployeeName(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeName(EmployeeNo);
		}
		public string GetCleanedEmployeeNo(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetCleanedEmployeeNo(EmployeeNo);
		}
		public string GetEmployeeGender(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeGender(EmployeeNo);
		}
		public string GetEmployeeBirthDay(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeDateOfBirth(EmployeeNo);
		}
		public string GetEmployeeRetirementDate(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeRetirementDate(EmployeeNo);
		}
		public string GetEmployeeAge(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeAge(EmployeeNo);
		}
		private string GetEmployeeEmailAddress(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetEmployeeEmailAddress(EmployeeNo);
		}
		private bool CheckEmployeeExists(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.EmployeeExists(EmployeeNo);
		}
		private bool LoginEmployee(string EmployeeNo, string EmployeePassword)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.LoginEmployee(EmployeeNo, EmployeePassword);
		}
		private bool CheckEmployeeAccountIsActive(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.EmployeeAccountIsActive(EmployeeNo);
		}
		private bool SetEmployeePasswordResetToken(string EmployeeNo, string PasswordResetToken)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.SetPasswordResetToken(EmployeeNo, PasswordResetToken);
		}
		private string GetEmployeePasswordResetToken(string EmployeeNo)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.GetPasswordResetToken(EmployeeNo);
		}
		private bool SendPasswordResetLink(string EmployeeNo, string EmailBody)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.SendPasswordResetLink(EmployeeNo, EmailBody);
		}
		private bool CheckPasswordResetTokenExpired(string EmployeeNo, string PasswordResetToken)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.IsPasswordResetTokenExpired(EmployeeNo, PasswordResetToken);
		}
		private bool ResetEmployeePortalPassword(string EmployeeNo, string NewPassword)
		{
			return dynamicsNAVSOAPServices.employeeAccountWS.ResetEmployeePortalPassword(EmployeeNo, NewPassword);
		}
		public string GetDynamicsNAVEmployeeDataDirectoryPath(string EmployeeNo)
		{
			string parentDirectoryName = "Org_Data";
			string childDirectoryName = "StaffData";
			return ServiceConnection.protocol + ServiceConnection.DynamicsNAVServer + "/" + parentDirectoryName + "/" + childDirectoryName + "/" + EmployeeNo + "/";
		}
		#endregion Helper Functions
	}
}