using DynamicsNAV365_Staff_WebPortal.ApprovalManagerWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.EmployeeAccountWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.FundsClaimWebserviceReference;
using DynamicsNAV365_Staff_WebPortal.FundsManagementWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.HumanResourceWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.InventoryManagementWebServiceReference;
using DynamicsNAV365_Staff_WebPortal.ProcurementManagementWebServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal
{
	public class DynamicsNAVSOAPServices
	{
		public EmployeeAccountWebService employeeAccountWS = new EmployeeAccountWebService();
        public HumanResourceManagementWS hrManagementWS = new HumanResourceManagementWS();
        public InventoryManagementWS inventoryManagementWS = new InventoryManagementWS();
        public ProcurementManagementWS procurementManagementWS = new ProcurementManagementWS();
        public FundsManagementWebService fundsManagementWS = new FundsManagementWebService();
        public FundsClaimManagementWebService fundsClaimManagementWS = new FundsClaimManagementWebService();
       // public MedicalClaimManagementWebService medicalClaimManagementWS = new MedicalClaimManagementWebService();
        public PortalApprovalManager ApprovalsMgmt = new PortalApprovalManager();

        public DynamicsNAVSOAPServices(string companyURLName)
		{
			//Employee Account WS
			employeeAccountWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("EmployeeAccountWebService", companyURLName);
			employeeAccountWS.Credentials = ServiceConnection.getConnectionCredentials(employeeAccountWS.Url);

            //HR Management WS
            hrManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("HumanResourceManagementWS", companyURLName);
            hrManagementWS.Credentials = ServiceConnection.getConnectionCredentials(hrManagementWS.Url);

            //Inventory Management WS
            inventoryManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("InventoryManagementWS", companyURLName);
            inventoryManagementWS.Credentials = ServiceConnection.getConnectionCredentials(inventoryManagementWS.Url);

            //Procurement Management WS
            procurementManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("ProcurementManagementWS", companyURLName);
            procurementManagementWS.Credentials = ServiceConnection.getConnectionCredentials(procurementManagementWS.Url);

            //Funds Management WS
            fundsManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("FundsManagementWebService", companyURLName);
            fundsManagementWS.Credentials = ServiceConnection.getConnectionCredentials(fundsManagementWS.Url);

            //Fundsclaim Management WS
            fundsClaimManagementWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("FundsClaimManagementWebService", companyURLName);
            fundsClaimManagementWS.Credentials = ServiceConnection.getConnectionCredentials(fundsClaimManagementWS.Url);

            //Approvals Management WS
            ApprovalsMgmt.Url = ServiceConnection.GetDynamicsNAVSOAPURL("PortalApprovalManager", companyURLName);
            ApprovalsMgmt.Credentials = ServiceConnection.getConnectionCredentials(ApprovalsMgmt.Url);


        }
	}
}