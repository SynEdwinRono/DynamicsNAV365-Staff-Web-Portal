using DynamicsNAV365_Staff_WebPortal.EmployeeAccountWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.FundsClaimManagementWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.FundsManagementWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.HumanResourceManagementWebServiceRef;
using DynamicsNAV365_Staff_WebPortal.InventoryManagementWebServiceRef;
using DynamicsNAV365_Staff_WebPortal.PortalApprovalManagerWebServiceRef;
using DynamicsNAV365_Staff_WebPortal.ProcurementManagementWebServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal
{
	public class DynamicsNAVSOAPServices
	{
		public EmployeeAccountWebService employeeAccountWS = new EmployeeAccountWebService();
		public HumanResourceManagementWS hrManagenentWS = new HumanResourceManagementWS();
		public FundsManagementWebService fundsManagementWS = new FundsManagementWebService();
		public FundsClaimManagementWebService fundsClaimManagementWS = new FundsClaimManagementWebService();
		public ProcurementManagementWS procurementManagementWS = new ProcurementManagementWS();
		public InventoryManagementWS inventoryManagementWS = new InventoryManagementWS();
		public PortalApprovalManager ApprovalsMgmt = new PortalApprovalManager();
		public DynamicsNAVSOAPServices(string companyURLName)
		{
			//Employee Account WS
			employeeAccountWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("EmployeeAccountWebService", companyURLName);
			employeeAccountWS.Credentials = ServiceConnection.getConnectionCredentials(employeeAccountWS.Url);

			//Human Resource Web Service
			hrManagenentWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("HumanResourceManagementWS", companyURLName);
			hrManagenentWS.Credentials = ServiceConnection.getConnectionCredentials(hrManagenentWS.Url);

			//Funds Management Web Service
			fundsManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("FundsManagementWebService", companyURLName);
			fundsManagementWS.Credentials = ServiceConnection.getConnectionCredentials(fundsManagementWS.Url);

			//Funds Claim Management Web Service
			fundsClaimManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("FundsClaimManagementWebService", companyURLName);
			fundsClaimManagementWS.Credentials = ServiceConnection.getConnectionCredentials(fundsClaimManagementWS.Url);

			//Procurement Management WS
			procurementManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("ProcurementManagementWS", companyURLName);
			procurementManagementWS.Credentials = ServiceConnection.getConnectionCredentials(procurementManagementWS.Url);

			//Inventory Management WS
			inventoryManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("InventoryManagementWS", companyURLName);
			inventoryManagementWS.Credentials = ServiceConnection.getConnectionCredentials(inventoryManagementWS.Url);

			//Approvals Management WS
			ApprovalsMgmt.Url = ServiceConnection.GetDynamicsNAVSOAPURL("PortalApprovalManager", companyURLName);
			ApprovalsMgmt.Credentials = ServiceConnection.getConnectionCredentials(ApprovalsMgmt.Url);
		}
	}
}