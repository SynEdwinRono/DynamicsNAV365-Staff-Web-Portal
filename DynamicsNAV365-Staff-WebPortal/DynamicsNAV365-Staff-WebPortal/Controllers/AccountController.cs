using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers
{
    public class AccountController : Controller
    {
		#region Signout
		public ActionResult Logout()
		{
			return View();
		}
		#endregion Signout
	}
}