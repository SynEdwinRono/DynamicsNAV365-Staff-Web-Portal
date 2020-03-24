using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal
{
    public class DynamicsNAVODATAServices
    {
		public NAV dynamicsNAVOData = null;
		public DynamicsNAVODATAServices(string companyURLName)
		{
			dynamicsNAVOData = new NAV(new Uri(ServiceConnection.GetDynamicsNAVODATAURL(companyURLName)));
			dynamicsNAVOData.Credentials = ServiceConnection.getConnectionCredentials(ServiceConnection.GetDynamicsNAVODATAURL(companyURLName));
		}
	}
}