using DynamicsNAV365_Staff_WebPortal.DynamicsNAVODataServiceReference;
using DynamicsNAV365_Staff_WebPortal.Models.Account;
using DynamicsNAV365_Staff_WebPortal.Models.HumanResourceModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicsNAV365_Staff_WebPortal.Controllers.HumanResourceServices
{
	public class HumanResourceHomeController : Controller
    {
		public HumanResourceHomeController()
		{
		}
		//Human Resource Service Summary
		[Authorize]
		public ActionResult HumanResourceInfo()
		{
			return View();
		}

		#region Helper Views
		[ChildActionOnly]
		public ActionResult _HumanResourceSidebar()
		{
			EmployeeProfileModel employeeProfileModel = new EmployeeProfileModel();
			employeeProfileModel.PassportAttached = false;
			return PartialView(employeeProfileModel);
		}

		[ChildActionOnly]
		public ActionResult _EmployeeLeaveBalanceWidget()
		{
			var employeeLeaveTypes = from employeeLeaveTypeList in dynamicsNAVODataServices.dynamicsNAVOData.EmployeeLeaveTypes
									 where employeeLeaveTypeList.Employee_No.Equals(AccountController.GetEmployeeNo())
									 select employeeLeaveTypeList;

			List<EmployeeLeaveBalanceModel> employeeLeaveBalances = new List<EmployeeLeaveBalanceModel>();
			foreach (EmployeeLeaveTypes employeeLeaveType in employeeLeaveTypes)
			{
				EmployeeLeaveBalanceModel employeeLeaveBalance = new EmployeeLeaveBalanceModel();
				employeeLeaveBalance.LeaveType = employeeLeaveType.Leave_Type;
				employeeLeaveBalance.LeaveBalance = employeeLeaveType.Leave_Balance;
				employeeLeaveBalance.LeaveBalanceStr = employeeLeaveType.Leave_Balance.Value.ToString("n");
				employeeLeaveBalances.Add(employeeLeaveBalance);
			}
			return PartialView(employeeLeaveBalances);
		}
		#endregion Helper Views
	}
}