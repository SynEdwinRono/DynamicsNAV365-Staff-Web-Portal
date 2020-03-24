using System;
using System.Net;

namespace DynamicsNAV365_Staff_WebPortal
{
	public class ServiceConnection
	{
		public static string CompanyName = "Regent Management Ltd";
		public static string CompanySupportEmail = "info@regent-mgt.com";
		public static string protocol = "http://";
		public static string DynamicsNAVServer = "41.78.24.34"; //192.168.88.23 //MEDIQUIP@2019
		public static string contactCustomerCareDesk = "Contact the " + CompanyName + " ICT Department for assistance.";

		static string SOAPPortNumber = "9047";
		static string ODATAPortNumber = "9048";
		static string DynamicsNAVServiceName = "CPHD";
		static string DefaultCompanyURLName = "CPHD";

		static string ConnUsername = "WSAUTH";
		static string ConnPassword = "Enterpr1$e";

		//========================-Network Connection Credentials-------------------------------------------
		static NetworkCredential netCred = new NetworkCredential(ConnUsername, ConnPassword);
		public static string googleReCaptchaKey = "6LdBZFkUAAAAAGhY2hZVzi2B9LB-SdHX83whjp1i";
		//========================-End Connection Credentials-----------------------------------------------

		//------------------------- share point configuration ----------------------------------------------
		public static string SharePointServer = "192.168.4.52";
		public static string SharePointParentFolder = "InvestmentPortal";
		public static string SharePointUser = "NAVADMIN";
		public static string SharePointUserPassword = "$ecurity123";
		//------------------------- share point configuration ----------------------------------------------

		//http://41.78.24.34:9048/CPHD/OData/Company('CPHD')/PurchaseRequisitionLines


		public static string GetDynamicsNAVSOAPURL(string ServiceName, string CompanyURLName)
		{
			if (CompanyURLName.Equals(""))
			{
				return protocol + DynamicsNAVServer + ":" + SOAPPortNumber + "/" + DynamicsNAVServiceName + "/WS/" + DefaultCompanyURLName + "/Codeunit/" + ServiceName;
			}
			else
			{
				return protocol + DynamicsNAVServer + ":" + SOAPPortNumber + "/" + DynamicsNAVServiceName + "/WS/" + CompanyURLName + "/Codeunit/" + ServiceName;
			}
		}
		public static string GetDynamicsNAVODATAURL(string CompanyURLName)
		{
			if (CompanyURLName.Equals(""))
			{
				return protocol + DynamicsNAVServer + ":" + ODATAPortNumber + "/" + DynamicsNAVServiceName + "/OData/Company('" + DefaultCompanyURLName + "')";
			}
			else
			{
				return protocol + DynamicsNAVServer + ":" + ODATAPortNumber + "/" + DynamicsNAVServiceName + "/OData/Company('" + CompanyURLName + "')";
			}
		}
		public static CredentialCache getConnectionCredentials(string Url)
		{
			CredentialCache myCredentials = new CredentialCache();
			if (myCredentials.GetCredential(new Uri(Url), "Basic") == null)
			{
				myCredentials.Add(new Uri(Url), "Basic", netCred);
			}
			return myCredentials;
		}
	}
}