using DynamicsNAV365_Staff_WebPortal.EmployeeAccountWebServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal
{
	public class DynamicsNAVSOAPServices
	{
		public EmployeeAccountWebService employeeAccountWS = new EmployeeAccountWebService();
		public DynamicsNAVSOAPServices(string companyURLName)
		{
			//Employee Account WS
			employeeAccountWS.Url = ServiceConnection.GetDynamicsNAVSOAPURL("EmployeeAccountWebService", companyURLName);
			employeeAccountWS.Credentials = ServiceConnection.getConnectionCredentials(employeeAccountWS.Url);
		}
	}
}