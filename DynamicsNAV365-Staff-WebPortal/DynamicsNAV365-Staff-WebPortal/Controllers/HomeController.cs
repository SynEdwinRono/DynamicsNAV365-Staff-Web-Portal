using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers
{
	public class HomeController : Controller
	{
		#region Home Page
		[Authorize]
		public ActionResult Index()
		{
			return View();
		}
		#endregion Home Page
	}
}