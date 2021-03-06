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

namespace IBBAV.WsServicios {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WsServiciosSoap", Namespace="http://tempuri.org/")]
    public partial class WsServicios : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback BDI_ServiciosGetOperationCompleted;
        
        private System.Threading.SendOrPostCallback BDI_CodigoAreaGetBySE_ServicioIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddUpdAfiliadoServicioOperationCompleted;
        
        private System.Threading.SendOrPostCallback DelAfiliadoServicioOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetServiciosByAfiliadoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAfiliadoServicioOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetNextReferenciaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WsServicios() {
            this.Url = global::IBBAV.Properties.Settings.Default.IBBAV_WsServicios_WsServicios;
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
        public event BDI_ServiciosGetCompletedEventHandler BDI_ServiciosGetCompleted;
        
        /// <remarks/>
        public event BDI_CodigoAreaGetBySE_ServicioIdCompletedEventHandler BDI_CodigoAreaGetBySE_ServicioIdCompleted;
        
        /// <remarks/>
        public event AddUpdAfiliadoServicioCompletedEventHandler AddUpdAfiliadoServicioCompleted;
        
        /// <remarks/>
        public event DelAfiliadoServicioCompletedEventHandler DelAfiliadoServicioCompleted;
        
        /// <remarks/>
        public event GetServiciosByAfiliadoCompletedEventHandler GetServiciosByAfiliadoCompleted;
        
        /// <remarks/>
        public event GetAfiliadoServicioCompletedEventHandler GetAfiliadoServicioCompleted;
        
        /// <remarks/>
        public event GetNextReferenciaCompletedEventHandler GetNextReferenciaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/BDI_ServiciosGet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet BDI_ServiciosGet(int SE_ServicioId) {
            object[] results = this.Invoke("BDI_ServiciosGet", new object[] {
                        SE_ServicioId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void BDI_ServiciosGetAsync(int SE_ServicioId) {
            this.BDI_ServiciosGetAsync(SE_ServicioId, null);
        }
        
        /// <remarks/>
        public void BDI_ServiciosGetAsync(int SE_ServicioId, object userState) {
            if ((this.BDI_ServiciosGetOperationCompleted == null)) {
                this.BDI_ServiciosGetOperationCompleted = new System.Threading.SendOrPostCallback(this.OnBDI_ServiciosGetOperationCompleted);
            }
            this.InvokeAsync("BDI_ServiciosGet", new object[] {
                        SE_ServicioId}, this.BDI_ServiciosGetOperationCompleted, userState);
        }
        
        private void OnBDI_ServiciosGetOperationCompleted(object arg) {
            if ((this.BDI_ServiciosGetCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.BDI_ServiciosGetCompleted(this, new BDI_ServiciosGetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/BDI_CodigoAreaGetBySE_ServicioId", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet BDI_CodigoAreaGetBySE_ServicioId(int SE_ServicioId) {
            object[] results = this.Invoke("BDI_CodigoAreaGetBySE_ServicioId", new object[] {
                        SE_ServicioId});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void BDI_CodigoAreaGetBySE_ServicioIdAsync(int SE_ServicioId) {
            this.BDI_CodigoAreaGetBySE_ServicioIdAsync(SE_ServicioId, null);
        }
        
        /// <remarks/>
        public void BDI_CodigoAreaGetBySE_ServicioIdAsync(int SE_ServicioId, object userState) {
            if ((this.BDI_CodigoAreaGetBySE_ServicioIdOperationCompleted == null)) {
                this.BDI_CodigoAreaGetBySE_ServicioIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnBDI_CodigoAreaGetBySE_ServicioIdOperationCompleted);
            }
            this.InvokeAsync("BDI_CodigoAreaGetBySE_ServicioId", new object[] {
                        SE_ServicioId}, this.BDI_CodigoAreaGetBySE_ServicioIdOperationCompleted, userState);
        }
        
        private void OnBDI_CodigoAreaGetBySE_ServicioIdOperationCompleted(object arg) {
            if ((this.BDI_CodigoAreaGetBySE_ServicioIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.BDI_CodigoAreaGetBySE_ServicioIdCompleted(this, new BDI_CodigoAreaGetBySE_ServicioIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddUpdAfiliadoServicio", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AddUpdAfiliadoServicio(int nAccion, int AF_ID, int ServicioId, string NumeroContrato, string Descripcion, string Email) {
            object[] results = this.Invoke("AddUpdAfiliadoServicio", new object[] {
                        nAccion,
                        AF_ID,
                        ServicioId,
                        NumeroContrato,
                        Descripcion,
                        Email});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void AddUpdAfiliadoServicioAsync(int nAccion, int AF_ID, int ServicioId, string NumeroContrato, string Descripcion, string Email) {
            this.AddUpdAfiliadoServicioAsync(nAccion, AF_ID, ServicioId, NumeroContrato, Descripcion, Email, null);
        }
        
        /// <remarks/>
        public void AddUpdAfiliadoServicioAsync(int nAccion, int AF_ID, int ServicioId, string NumeroContrato, string Descripcion, string Email, object userState) {
            if ((this.AddUpdAfiliadoServicioOperationCompleted == null)) {
                this.AddUpdAfiliadoServicioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddUpdAfiliadoServicioOperationCompleted);
            }
            this.InvokeAsync("AddUpdAfiliadoServicio", new object[] {
                        nAccion,
                        AF_ID,
                        ServicioId,
                        NumeroContrato,
                        Descripcion,
                        Email}, this.AddUpdAfiliadoServicioOperationCompleted, userState);
        }
        
        private void OnAddUpdAfiliadoServicioOperationCompleted(object arg) {
            if ((this.AddUpdAfiliadoServicioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddUpdAfiliadoServicioCompleted(this, new AddUpdAfiliadoServicioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DelAfiliadoServicio", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DelAfiliadoServicio(int AF_ID, int ServicioId, string NumeroContrato) {
            object[] results = this.Invoke("DelAfiliadoServicio", new object[] {
                        AF_ID,
                        ServicioId,
                        NumeroContrato});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void DelAfiliadoServicioAsync(int AF_ID, int ServicioId, string NumeroContrato) {
            this.DelAfiliadoServicioAsync(AF_ID, ServicioId, NumeroContrato, null);
        }
        
        /// <remarks/>
        public void DelAfiliadoServicioAsync(int AF_ID, int ServicioId, string NumeroContrato, object userState) {
            if ((this.DelAfiliadoServicioOperationCompleted == null)) {
                this.DelAfiliadoServicioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDelAfiliadoServicioOperationCompleted);
            }
            this.InvokeAsync("DelAfiliadoServicio", new object[] {
                        AF_ID,
                        ServicioId,
                        NumeroContrato}, this.DelAfiliadoServicioOperationCompleted, userState);
        }
        
        private void OnDelAfiliadoServicioOperationCompleted(object arg) {
            if ((this.DelAfiliadoServicioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DelAfiliadoServicioCompleted(this, new DelAfiliadoServicioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetServiciosByAfiliado", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetServiciosByAfiliado(int AF_ID) {
            object[] results = this.Invoke("GetServiciosByAfiliado", new object[] {
                        AF_ID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetServiciosByAfiliadoAsync(int AF_ID) {
            this.GetServiciosByAfiliadoAsync(AF_ID, null);
        }
        
        /// <remarks/>
        public void GetServiciosByAfiliadoAsync(int AF_ID, object userState) {
            if ((this.GetServiciosByAfiliadoOperationCompleted == null)) {
                this.GetServiciosByAfiliadoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetServiciosByAfiliadoOperationCompleted);
            }
            this.InvokeAsync("GetServiciosByAfiliado", new object[] {
                        AF_ID}, this.GetServiciosByAfiliadoOperationCompleted, userState);
        }
        
        private void OnGetServiciosByAfiliadoOperationCompleted(object arg) {
            if ((this.GetServiciosByAfiliadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetServiciosByAfiliadoCompleted(this, new GetServiciosByAfiliadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAfiliadoServicio", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAfiliadoServicio(int AF_ID, int ServicioId, string NumeroContrato) {
            object[] results = this.Invoke("GetAfiliadoServicio", new object[] {
                        AF_ID,
                        ServicioId,
                        NumeroContrato});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAfiliadoServicioAsync(int AF_ID, int ServicioId, string NumeroContrato) {
            this.GetAfiliadoServicioAsync(AF_ID, ServicioId, NumeroContrato, null);
        }
        
        /// <remarks/>
        public void GetAfiliadoServicioAsync(int AF_ID, int ServicioId, string NumeroContrato, object userState) {
            if ((this.GetAfiliadoServicioOperationCompleted == null)) {
                this.GetAfiliadoServicioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAfiliadoServicioOperationCompleted);
            }
            this.InvokeAsync("GetAfiliadoServicio", new object[] {
                        AF_ID,
                        ServicioId,
                        NumeroContrato}, this.GetAfiliadoServicioOperationCompleted, userState);
        }
        
        private void OnGetAfiliadoServicioOperationCompleted(object arg) {
            if ((this.GetAfiliadoServicioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAfiliadoServicioCompleted(this, new GetAfiliadoServicioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetNextReferencia", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetNextReferencia(int ServicioId) {
            object[] results = this.Invoke("GetNextReferencia", new object[] {
                        ServicioId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetNextReferenciaAsync(int ServicioId) {
            this.GetNextReferenciaAsync(ServicioId, null);
        }
        
        /// <remarks/>
        public void GetNextReferenciaAsync(int ServicioId, object userState) {
            if ((this.GetNextReferenciaOperationCompleted == null)) {
                this.GetNextReferenciaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNextReferenciaOperationCompleted);
            }
            this.InvokeAsync("GetNextReferencia", new object[] {
                        ServicioId}, this.GetNextReferenciaOperationCompleted, userState);
        }
        
        private void OnGetNextReferenciaOperationCompleted(object arg) {
            if ((this.GetNextReferenciaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNextReferenciaCompleted(this, new GetNextReferenciaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void BDI_ServiciosGetCompletedEventHandler(object sender, BDI_ServiciosGetCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BDI_ServiciosGetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal BDI_ServiciosGetCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void BDI_CodigoAreaGetBySE_ServicioIdCompletedEventHandler(object sender, BDI_CodigoAreaGetBySE_ServicioIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BDI_CodigoAreaGetBySE_ServicioIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal BDI_CodigoAreaGetBySE_ServicioIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void AddUpdAfiliadoServicioCompletedEventHandler(object sender, AddUpdAfiliadoServicioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddUpdAfiliadoServicioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddUpdAfiliadoServicioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void DelAfiliadoServicioCompletedEventHandler(object sender, DelAfiliadoServicioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DelAfiliadoServicioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DelAfiliadoServicioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void GetServiciosByAfiliadoCompletedEventHandler(object sender, GetServiciosByAfiliadoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetServiciosByAfiliadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetServiciosByAfiliadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void GetAfiliadoServicioCompletedEventHandler(object sender, GetAfiliadoServicioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAfiliadoServicioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAfiliadoServicioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void GetNextReferenciaCompletedEventHandler(object sender, GetNextReferenciaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNextReferenciaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetNextReferenciaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591