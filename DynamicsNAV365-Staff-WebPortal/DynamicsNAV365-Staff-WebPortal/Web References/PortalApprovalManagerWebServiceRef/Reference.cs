﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace DynamicsNAV365_Staff_WebPortal.PortalApprovalManagerWebServiceRef {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PortalApprovalManager_Binding", Namespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager")]
    public partial class PortalApprovalManager : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ApproveLeaveApplicationOperationCompleted;
        
        private System.Threading.SendOrPostCallback RejectLeaveApplicationOperationCompleted;
        
        private System.Threading.SendOrPostCallback ApproveImprestApplicationOperationCompleted;
        
        private System.Threading.SendOrPostCallback RejectImprestApplicationOperationCompleted;
        
        private System.Threading.SendOrPostCallback ApproveImprestAccountingOperationCompleted;
        
        private System.Threading.SendOrPostCallback RejectImprestAccountingOperationCompleted;
        
        private System.Threading.SendOrPostCallback ApproveStoreRequisitionOperationCompleted;
        
        private System.Threading.SendOrPostCallback RejectStoreRequisitionOperationCompleted;
        
        private System.Threading.SendOrPostCallback ApprovePurchaseRequisitionOperationCompleted;
        
        private System.Threading.SendOrPostCallback RejectPurchaseRequisitionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PortalApprovalManager() {
            this.Url = global::DynamicsNAV365_Staff_WebPortal.Properties.Settings.Default.DynamicsNAV365_Staff_WebPortal_PortalApprovalManagerWebServiceRef_PortalApprovalManager;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ApproveLeaveApplicationCompletedEventHandler ApproveLeaveApplicationCompleted;
        
        /// <remarks/>
        public event RejectLeaveApplicationCompletedEventHandler RejectLeaveApplicationCompleted;
        
        /// <remarks/>
        public event ApproveImprestApplicationCompletedEventHandler ApproveImprestApplicationCompleted;
        
        /// <remarks/>
        public event RejectImprestApplicationCompletedEventHandler RejectImprestApplicationCompleted;
        
        /// <remarks/>
        public event ApproveImprestAccountingCompletedEventHandler ApproveImprestAccountingCompleted;
        
        /// <remarks/>
        public event RejectImprestAccountingCompletedEventHandler RejectImprestAccountingCompleted;
        
        /// <remarks/>
        public event ApproveStoreRequisitionCompletedEventHandler ApproveStoreRequisitionCompleted;
        
        /// <remarks/>
        public event RejectStoreRequisitionCompletedEventHandler RejectStoreRequisitionCompleted;
        
        /// <remarks/>
        public event ApprovePurchaseRequisitionCompletedEventHandler ApprovePurchaseRequisitionCompleted;
        
        /// <remarks/>
        public event RejectPurchaseRequisitionCompletedEventHandler RejectPurchaseRequisitionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:ApproveLeaveApplica" +
            "tion", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="ApproveLeaveApplication_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool ApproveLeaveApplication(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("ApproveLeaveApplication", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ApproveLeaveApplicationAsync(string employeeNoa46, string documentNoa46) {
            this.ApproveLeaveApplicationAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void ApproveLeaveApplicationAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.ApproveLeaveApplicationOperationCompleted == null)) {
                this.ApproveLeaveApplicationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnApproveLeaveApplicationOperationCompleted);
            }
            this.InvokeAsync("ApproveLeaveApplication", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.ApproveLeaveApplicationOperationCompleted, userState);
        }
        
        private void OnApproveLeaveApplicationOperationCompleted(object arg) {
            if ((this.ApproveLeaveApplicationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ApproveLeaveApplicationCompleted(this, new ApproveLeaveApplicationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:RejectLeaveApplicat" +
            "ion", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="RejectLeaveApplication_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool RejectLeaveApplication(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("RejectLeaveApplication", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RejectLeaveApplicationAsync(string employeeNoa46, string documentNoa46) {
            this.RejectLeaveApplicationAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void RejectLeaveApplicationAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.RejectLeaveApplicationOperationCompleted == null)) {
                this.RejectLeaveApplicationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRejectLeaveApplicationOperationCompleted);
            }
            this.InvokeAsync("RejectLeaveApplication", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.RejectLeaveApplicationOperationCompleted, userState);
        }
        
        private void OnRejectLeaveApplicationOperationCompleted(object arg) {
            if ((this.RejectLeaveApplicationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RejectLeaveApplicationCompleted(this, new RejectLeaveApplicationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:ApproveImprestAppli" +
            "cation", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="ApproveImprestApplication_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool ApproveImprestApplication(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("ApproveImprestApplication", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ApproveImprestApplicationAsync(string employeeNoa46, string documentNoa46) {
            this.ApproveImprestApplicationAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void ApproveImprestApplicationAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.ApproveImprestApplicationOperationCompleted == null)) {
                this.ApproveImprestApplicationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnApproveImprestApplicationOperationCompleted);
            }
            this.InvokeAsync("ApproveImprestApplication", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.ApproveImprestApplicationOperationCompleted, userState);
        }
        
        private void OnApproveImprestApplicationOperationCompleted(object arg) {
            if ((this.ApproveImprestApplicationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ApproveImprestApplicationCompleted(this, new ApproveImprestApplicationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:RejectImprestApplic" +
            "ation", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="RejectImprestApplication_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool RejectImprestApplication(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("RejectImprestApplication", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RejectImprestApplicationAsync(string employeeNoa46, string documentNoa46) {
            this.RejectImprestApplicationAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void RejectImprestApplicationAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.RejectImprestApplicationOperationCompleted == null)) {
                this.RejectImprestApplicationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRejectImprestApplicationOperationCompleted);
            }
            this.InvokeAsync("RejectImprestApplication", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.RejectImprestApplicationOperationCompleted, userState);
        }
        
        private void OnRejectImprestApplicationOperationCompleted(object arg) {
            if ((this.RejectImprestApplicationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RejectImprestApplicationCompleted(this, new RejectImprestApplicationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:ApproveImprestAccou" +
            "nting", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="ApproveImprestAccounting_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool ApproveImprestAccounting(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("ApproveImprestAccounting", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ApproveImprestAccountingAsync(string employeeNoa46, string documentNoa46) {
            this.ApproveImprestAccountingAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void ApproveImprestAccountingAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.ApproveImprestAccountingOperationCompleted == null)) {
                this.ApproveImprestAccountingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnApproveImprestAccountingOperationCompleted);
            }
            this.InvokeAsync("ApproveImprestAccounting", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.ApproveImprestAccountingOperationCompleted, userState);
        }
        
        private void OnApproveImprestAccountingOperationCompleted(object arg) {
            if ((this.ApproveImprestAccountingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ApproveImprestAccountingCompleted(this, new ApproveImprestAccountingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:RejectImprestAccoun" +
            "ting", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="RejectImprestAccounting_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool RejectImprestAccounting(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("RejectImprestAccounting", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RejectImprestAccountingAsync(string employeeNoa46, string documentNoa46) {
            this.RejectImprestAccountingAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void RejectImprestAccountingAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.RejectImprestAccountingOperationCompleted == null)) {
                this.RejectImprestAccountingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRejectImprestAccountingOperationCompleted);
            }
            this.InvokeAsync("RejectImprestAccounting", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.RejectImprestAccountingOperationCompleted, userState);
        }
        
        private void OnRejectImprestAccountingOperationCompleted(object arg) {
            if ((this.RejectImprestAccountingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RejectImprestAccountingCompleted(this, new RejectImprestAccountingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:ApproveStoreRequisi" +
            "tion", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="ApproveStoreRequisition_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool ApproveStoreRequisition(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("ApproveStoreRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ApproveStoreRequisitionAsync(string employeeNoa46, string documentNoa46) {
            this.ApproveStoreRequisitionAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void ApproveStoreRequisitionAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.ApproveStoreRequisitionOperationCompleted == null)) {
                this.ApproveStoreRequisitionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnApproveStoreRequisitionOperationCompleted);
            }
            this.InvokeAsync("ApproveStoreRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.ApproveStoreRequisitionOperationCompleted, userState);
        }
        
        private void OnApproveStoreRequisitionOperationCompleted(object arg) {
            if ((this.ApproveStoreRequisitionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ApproveStoreRequisitionCompleted(this, new ApproveStoreRequisitionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:RejectStoreRequisit" +
            "ion", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="RejectStoreRequisition_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool RejectStoreRequisition(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("RejectStoreRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RejectStoreRequisitionAsync(string employeeNoa46, string documentNoa46) {
            this.RejectStoreRequisitionAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void RejectStoreRequisitionAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.RejectStoreRequisitionOperationCompleted == null)) {
                this.RejectStoreRequisitionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRejectStoreRequisitionOperationCompleted);
            }
            this.InvokeAsync("RejectStoreRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.RejectStoreRequisitionOperationCompleted, userState);
        }
        
        private void OnRejectStoreRequisitionOperationCompleted(object arg) {
            if ((this.RejectStoreRequisitionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RejectStoreRequisitionCompleted(this, new RejectStoreRequisitionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:ApprovePurchaseRequ" +
            "isition", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="ApprovePurchaseRequisition_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool ApprovePurchaseRequisition(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("ApprovePurchaseRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ApprovePurchaseRequisitionAsync(string employeeNoa46, string documentNoa46) {
            this.ApprovePurchaseRequisitionAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void ApprovePurchaseRequisitionAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.ApprovePurchaseRequisitionOperationCompleted == null)) {
                this.ApprovePurchaseRequisitionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnApprovePurchaseRequisitionOperationCompleted);
            }
            this.InvokeAsync("ApprovePurchaseRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.ApprovePurchaseRequisitionOperationCompleted, userState);
        }
        
        private void OnApprovePurchaseRequisitionOperationCompleted(object arg) {
            if ((this.ApprovePurchaseRequisitionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ApprovePurchaseRequisitionCompleted(this, new ApprovePurchaseRequisitionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager:RejectPurchaseRequi" +
            "sition", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", ResponseElementName="RejectPurchaseRequisition_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/PortalApprovalManager", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public bool RejectPurchaseRequisition(string employeeNoa46, string documentNoa46) {
            object[] results = this.Invoke("RejectPurchaseRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void RejectPurchaseRequisitionAsync(string employeeNoa46, string documentNoa46) {
            this.RejectPurchaseRequisitionAsync(employeeNoa46, documentNoa46, null);
        }
        
        /// <remarks/>
        public void RejectPurchaseRequisitionAsync(string employeeNoa46, string documentNoa46, object userState) {
            if ((this.RejectPurchaseRequisitionOperationCompleted == null)) {
                this.RejectPurchaseRequisitionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRejectPurchaseRequisitionOperationCompleted);
            }
            this.InvokeAsync("RejectPurchaseRequisition", new object[] {
                        employeeNoa46,
                        documentNoa46}, this.RejectPurchaseRequisitionOperationCompleted, userState);
        }
        
        private void OnRejectPurchaseRequisitionOperationCompleted(object arg) {
            if ((this.RejectPurchaseRequisitionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RejectPurchaseRequisitionCompleted(this, new RejectPurchaseRequisitionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ApproveLeaveApplicationCompletedEventHandler(object sender, ApproveLeaveApplicationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ApproveLeaveApplicationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ApproveLeaveApplicationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void RejectLeaveApplicationCompletedEventHandler(object sender, RejectLeaveApplicationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RejectLeaveApplicationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RejectLeaveApplicationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ApproveImprestApplicationCompletedEventHandler(object sender, ApproveImprestApplicationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ApproveImprestApplicationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ApproveImprestApplicationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void RejectImprestApplicationCompletedEventHandler(object sender, RejectImprestApplicationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RejectImprestApplicationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RejectImprestApplicationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ApproveImprestAccountingCompletedEventHandler(object sender, ApproveImprestAccountingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ApproveImprestAccountingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ApproveImprestAccountingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void RejectImprestAccountingCompletedEventHandler(object sender, RejectImprestAccountingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RejectImprestAccountingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RejectImprestAccountingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ApproveStoreRequisitionCompletedEventHandler(object sender, ApproveStoreRequisitionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ApproveStoreRequisitionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ApproveStoreRequisitionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void RejectStoreRequisitionCompletedEventHandler(object sender, RejectStoreRequisitionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RejectStoreRequisitionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RejectStoreRequisitionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void ApprovePurchaseRequisitionCompletedEventHandler(object sender, ApprovePurchaseRequisitionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ApprovePurchaseRequisitionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ApprovePurchaseRequisitionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void RejectPurchaseRequisitionCompletedEventHandler(object sender, RejectPurchaseRequisitionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RejectPurchaseRequisitionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RejectPurchaseRequisitionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591