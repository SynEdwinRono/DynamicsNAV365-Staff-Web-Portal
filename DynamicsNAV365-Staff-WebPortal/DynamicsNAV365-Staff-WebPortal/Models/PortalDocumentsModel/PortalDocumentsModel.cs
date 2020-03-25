using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.PortalDocumentsModel
{
    public class PortalDocumentsModel
    {
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentDescription { get; set; }
        public bool DocumentAttached { get; set; }
        public string LocalURL { get; set; }
        public string SharePointURL { get; set; }
        public bool ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
