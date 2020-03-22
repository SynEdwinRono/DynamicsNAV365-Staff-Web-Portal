using System;
using System.Net;

namespace DynamicsNAV365_Staff_WebPortal
{
	public class ServiceConnection
	{
		public static string CompanyName = "ICDC";
		public static string CompanySupportEmail = "customerservice@icdc.co.ke";

		public static string protocol = "http://";
		public static string DynamicsNAVServer = "192.168.4.55";
		static string SOAPPortNumber = "4042";
		static string ODATAPortNumber = "4043";
		static string DynamicsNAVServiceName = "ICDC_WS";
		static string DefaultCompanyURLName = "ICDC%20Test";
		//static string DefaultCompanyURLName = "ICDC";
		static string ConnUsername = "WSAUTH";
		static string ConnPassword = "Wsauth@2019";

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