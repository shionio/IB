﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace IBBAV.WSTedexis {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="M4WSIntSRSoap11Binding", Namespace="http://core.ws.m4.tedexis.com")]
    public partial class M4WSIntSR : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback sendSMSappIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback responseSMSOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendSMSccappIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendSMSWithIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback responseSMSccOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendSMSccOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendSMSWithIDccOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendSMSOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public M4WSIntSR() {
            this.Url = global::IBBAV.Properties.Settings.Default.IBBAV_WSTedexis_M4WSIntSR;
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
        public event sendSMSappIDCompletedEventHandler sendSMSappIDCompleted;
        
        /// <remarks/>
        public event responseSMSCompletedEventHandler responseSMSCompleted;
        
        /// <remarks/>
        public event sendSMSccappIDCompletedEventHandler sendSMSccappIDCompleted;
        
        /// <remarks/>
        public event sendSMSWithIDCompletedEventHandler sendSMSWithIDCompleted;
        
        /// <remarks/>
        public event responseSMSccCompletedEventHandler responseSMSccCompleted;
        
        /// <remarks/>
        public event sendSMSccCompletedEventHandler sendSMSccCompleted;
        
        /// <remarks/>
        public event sendSMSWithIDccCompletedEventHandler sendSMSWithIDccCompleted;
        
        /// <remarks/>
        public event sendSMSCompletedEventHandler sendSMSCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendSMSappID", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void sendSMSappID([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string appID, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("sendSMSappID", new object[] {
                        passport,
                        password,
                        number,
                        text,
                        appID});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void sendSMSappIDAsync(string passport, string password, string number, string text, string appID) {
            this.sendSMSappIDAsync(passport, password, number, text, appID, null);
        }
        
        /// <remarks/>
        public void sendSMSappIDAsync(string passport, string password, string number, string text, string appID, object userState) {
            if ((this.sendSMSappIDOperationCompleted == null)) {
                this.sendSMSappIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendSMSappIDOperationCompleted);
            }
            this.InvokeAsync("sendSMSappID", new object[] {
                        passport,
                        password,
                        number,
                        text,
                        appID}, this.sendSMSappIDOperationCompleted, userState);
        }
        
        private void OnsendSMSappIDOperationCompleted(object arg) {
            if ((this.sendSMSappIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendSMSappIDCompleted(this, new sendSMSappIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:responseSMS", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void responseSMS([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string messageID, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("responseSMS", new object[] {
                        passport,
                        password,
                        number,
                        text,
                        messageID});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void responseSMSAsync(string passport, string password, string number, string text, string messageID) {
            this.responseSMSAsync(passport, password, number, text, messageID, null);
        }
        
        /// <remarks/>
        public void responseSMSAsync(string passport, string password, string number, string text, string messageID, object userState) {
            if ((this.responseSMSOperationCompleted == null)) {
                this.responseSMSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnresponseSMSOperationCompleted);
            }
            this.InvokeAsync("responseSMS", new object[] {
                        passport,
                        password,
                        number,
                        text,
                        messageID}, this.responseSMSOperationCompleted, userState);
        }
        
        private void OnresponseSMSOperationCompleted(object arg) {
            if ((this.responseSMSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.responseSMSCompleted(this, new responseSMSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendSMSccappID", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void sendSMSccappID([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string countryCode, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string appID, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("sendSMSccappID", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text,
                        appID});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void sendSMSccappIDAsync(string passport, string password, string countryCode, string number, string text, string appID) {
            this.sendSMSccappIDAsync(passport, password, countryCode, number, text, appID, null);
        }
        
        /// <remarks/>
        public void sendSMSccappIDAsync(string passport, string password, string countryCode, string number, string text, string appID, object userState) {
            if ((this.sendSMSccappIDOperationCompleted == null)) {
                this.sendSMSccappIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendSMSccappIDOperationCompleted);
            }
            this.InvokeAsync("sendSMSccappID", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text,
                        appID}, this.sendSMSccappIDOperationCompleted, userState);
        }
        
        private void OnsendSMSccappIDOperationCompleted(object arg) {
            if ((this.sendSMSccappIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendSMSccappIDCompleted(this, new sendSMSccappIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendSMSWithID", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void sendSMSWithID([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("sendSMSWithID", new object[] {
                        passport,
                        password,
                        number,
                        text});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void sendSMSWithIDAsync(string passport, string password, string number, string text) {
            this.sendSMSWithIDAsync(passport, password, number, text, null);
        }
        
        /// <remarks/>
        public void sendSMSWithIDAsync(string passport, string password, string number, string text, object userState) {
            if ((this.sendSMSWithIDOperationCompleted == null)) {
                this.sendSMSWithIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendSMSWithIDOperationCompleted);
            }
            this.InvokeAsync("sendSMSWithID", new object[] {
                        passport,
                        password,
                        number,
                        text}, this.sendSMSWithIDOperationCompleted, userState);
        }
        
        private void OnsendSMSWithIDOperationCompleted(object arg) {
            if ((this.sendSMSWithIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendSMSWithIDCompleted(this, new sendSMSWithIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:responseSMScc", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void responseSMScc([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string countryCode, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string messageID, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("responseSMScc", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text,
                        messageID});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void responseSMSccAsync(string passport, string password, string countryCode, string number, string text, string messageID) {
            this.responseSMSccAsync(passport, password, countryCode, number, text, messageID, null);
        }
        
        /// <remarks/>
        public void responseSMSccAsync(string passport, string password, string countryCode, string number, string text, string messageID, object userState) {
            if ((this.responseSMSccOperationCompleted == null)) {
                this.responseSMSccOperationCompleted = new System.Threading.SendOrPostCallback(this.OnresponseSMSccOperationCompleted);
            }
            this.InvokeAsync("responseSMScc", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text,
                        messageID}, this.responseSMSccOperationCompleted, userState);
        }
        
        private void OnresponseSMSccOperationCompleted(object arg) {
            if ((this.responseSMSccCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.responseSMSccCompleted(this, new responseSMSccCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendSMScc", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void sendSMScc([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string countryCode, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("sendSMScc", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void sendSMSccAsync(string passport, string password, string countryCode, string number, string text) {
            this.sendSMSccAsync(passport, password, countryCode, number, text, null);
        }
        
        /// <remarks/>
        public void sendSMSccAsync(string passport, string password, string countryCode, string number, string text, object userState) {
            if ((this.sendSMSccOperationCompleted == null)) {
                this.sendSMSccOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendSMSccOperationCompleted);
            }
            this.InvokeAsync("sendSMScc", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text}, this.sendSMSccOperationCompleted, userState);
        }
        
        private void OnsendSMSccOperationCompleted(object arg) {
            if ((this.sendSMSccCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendSMSccCompleted(this, new sendSMSccCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendSMSWithIDcc", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void sendSMSWithIDcc([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string countryCode, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("sendSMSWithIDcc", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void sendSMSWithIDccAsync(string passport, string password, string countryCode, string number, string text) {
            this.sendSMSWithIDccAsync(passport, password, countryCode, number, text, null);
        }
        
        /// <remarks/>
        public void sendSMSWithIDccAsync(string passport, string password, string countryCode, string number, string text, object userState) {
            if ((this.sendSMSWithIDccOperationCompleted == null)) {
                this.sendSMSWithIDccOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendSMSWithIDccOperationCompleted);
            }
            this.InvokeAsync("sendSMSWithIDcc", new object[] {
                        passport,
                        password,
                        countryCode,
                        number,
                        text}, this.sendSMSWithIDccOperationCompleted, userState);
        }
        
        private void OnsendSMSWithIDccOperationCompleted(object arg) {
            if ((this.sendSMSWithIDccCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendSMSWithIDccCompleted(this, new sendSMSWithIDccCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendSMS", RequestNamespace="http://core.ws.m4.tedexis.com", ResponseNamespace="http://core.ws.m4.tedexis.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void sendSMS([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string passport, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string text, out int @return, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool returnSpecified) {
            object[] results = this.Invoke("sendSMS", new object[] {
                        passport,
                        password,
                        number,
                        text});
            @return = ((int)(results[0]));
            returnSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void sendSMSAsync(string passport, string password, string number, string text) {
            this.sendSMSAsync(passport, password, number, text, null);
        }
        
        /// <remarks/>
        public void sendSMSAsync(string passport, string password, string number, string text, object userState) {
            if ((this.sendSMSOperationCompleted == null)) {
                this.sendSMSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendSMSOperationCompleted);
            }
            this.InvokeAsync("sendSMS", new object[] {
                        passport,
                        password,
                        number,
                        text}, this.sendSMSOperationCompleted, userState);
        }
        
        private void OnsendSMSOperationCompleted(object arg) {
            if ((this.sendSMSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendSMSCompleted(this, new sendSMSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void sendSMSappIDCompletedEventHandler(object sender, sendSMSappIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendSMSappIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendSMSappIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void responseSMSCompletedEventHandler(object sender, responseSMSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class responseSMSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal responseSMSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void sendSMSccappIDCompletedEventHandler(object sender, sendSMSccappIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendSMSccappIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendSMSccappIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void sendSMSWithIDCompletedEventHandler(object sender, sendSMSWithIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendSMSWithIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendSMSWithIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void responseSMSccCompletedEventHandler(object sender, responseSMSccCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class responseSMSccCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal responseSMSccCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void sendSMSccCompletedEventHandler(object sender, sendSMSccCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendSMSccCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendSMSccCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void sendSMSWithIDccCompletedEventHandler(object sender, sendSMSWithIDccCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendSMSWithIDccCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendSMSWithIDccCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void sendSMSCompletedEventHandler(object sender, sendSMSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendSMSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendSMSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int @return {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool returnSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591