using System.Web.Mvc;
using System.Web.Security;

namespace DynamicsNAV365_Staff_WebPortal.Controllers
{
	public class AccountController : Controller
    {
		#region Signout
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Home");
		}
		#endregion Signout
	}
}