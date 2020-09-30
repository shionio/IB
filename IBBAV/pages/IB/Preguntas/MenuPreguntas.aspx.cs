using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.UserControls;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.Preguntas
{
    public partial class MenuPreguntas : Principal
    {
        public IBBAV.Entidades.Afiliado afi
        {
            get
            {
                return this.ViewState["Afiliado"] as IBBAV.Entidades.Afiliado;
            }
            set
            {
                this.ViewState["Afiliado"] = value;
            }
        }

        public string clvTemporal
        {
            get
            {
                object item = this.ViewState["clvTemporal"];
                return (item != null ? item.ToString() : "");
            }
            set
            {
                this.ViewState["clvTemporal"] = value;
            }
        }

        public MenuPreguntas()
        {
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx", false);
        }

        protected void btnCancelarSMS_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx", false);
        }

        protected void btnceptar_Click(object sender, EventArgs e)
        {
            long nAFId = this.afi.nAF_Id;
            try
            {
                List<KeyValuePair<short, string>> preguntasRespuestas = this.PreguntasDesafio.getPreguntasRespuestas();
                if ((new HelperAfiliado()).PreguntasAfiliadoAddRes(nAFId, preguntasRespuestas))
                {
                    this.panelCambiOk.Visible = true;
                    this.panelDatos.Visible = false;
                    this.panelHome.Visible = false;
                }
            }
            catch (Exception exception)
            {
                WebUtils.MessageBootstrap(this, exception.Message, null);
            }
        }

        protected void btnContinuarSMS_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                if (this.txtClave.Text.Trim().Equals(string.Empty))
                {
                    throw new Exception("Debe indicar la clave de Afiliación Temporal");
                }
                if (!HelperLogons.ValidarClaveTransacciones(base.Afiliado.nAF_Id, this.txtClave.Text.Trim(), WebUtils.GetClientIP(this), base.sCod))
                {
                    throw new Exception("Clave inválida, por favor intente de nuevo");
                }
                flag = true;
            }
            catch (IBException bException1)
            {
                IBException bException = bException1;
                flag = true;
                if (bException.CodigoSistema.Equals("IBSX"))
                {
                    WebUtils.MessageBootstrap(this, string.Concat("Se ha detectado un problema de ejecución, por favor verifique antes de realizar otra operación (", bException.ReturnCode, ")"), null);
                }
                else if ((!bException.CodigoSistema.Equals("SQLIB") ? false : bException.ReturnCode.Equals("4")))
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                }
                else
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                }
                return;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                flag = true;
                WebUtils.MessageBootstrap(this, exception.Message, null);
                return;
            }
            if (flag)
            {
                this.panelDatos.Visible = true;
                this.panelValidacion.Visible = false;
            }
        }

        protected void btnhome_Click(object sender, EventArgs e)
        {
            if ((!base.IsPostBack ? false : base.Tipo_Seguridad == 1))
            {
                if (this.Session["SMS"] == null)
                {
                    string str = HelperAfiliado.CrearClaveTransaccionTemporal(base.Afiliado.nAF_Id);
                    if (!string.IsNullOrEmpty(base.Afiliado.CO_Celular))
                    {
                        HelperTedexis.sendSMS(base.Afiliado.CO_Celular, string.Concat("BAV informa que su Clave de Transacciones Temporal es: ", str, ". Si usted no esta realizando esta operacion llame al 0500-288.00.01"));
                        this.Session.Add("SMS", true);
                        this.panelDatos.Visible = false;
                        this.panelHome.Visible = false;
                        this.panelValidacion.Visible = true;
                    }
                }
                else
                {
                    this.panelValidacion.Visible = true;
                    this.panelHome.Visible = false;
                    this.panelDatos.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}