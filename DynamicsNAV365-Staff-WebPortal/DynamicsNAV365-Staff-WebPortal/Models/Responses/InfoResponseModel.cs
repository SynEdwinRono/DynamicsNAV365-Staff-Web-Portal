using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsNAV365_Staff_WebPortal.Models.Responses
{
    public class InfoResponseModel
    {
        public string ResponseType { get; set; }
        public string ResponseHeader { get; set; }
        public string ResponseMessage { get; set; }
        public string DetailedResponseMessage { get; set; }
        public string ReturnControllerName { get; set; }
        public string ReturnActionName { get; set; }
        public string ReturnLinkName { get; set; }
        public bool HasParameters { get; set; }
        public string Parameters { get; set; }
        public string CancelControllerName { get; set; }
        public string CancelActionName { get; set; }
        public string CancelLinkName { get; set; }
    }
}