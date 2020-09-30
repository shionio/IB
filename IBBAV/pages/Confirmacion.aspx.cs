using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Helpers;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages
{
    public partial class Confirmacion : Principal
    {
        private EncabezadoTransaccion encabezado
        {
            get
            {
                return (EncabezadoTransaccion)this.ViewState["EncabezadoTransaccion"];
            }
            set
            {
                this.ViewState["EncabezadoTransaccion"] = value;
            }
        }

        private bool recimp
        {
            get
            {
                object item = this.ViewState["recimp"];
                return (item != null ? (bool)item : false);
            }
            set
            {
                this.ViewState["recimp"] = value;
            }
        }

        private TipoTransaccionGenerica tipotransaccion
        {
            get
            {
                return (TipoTransaccionGenerica)this.ViewState["TipoTransaccionGenerica"];
            }
            set
            {
                this.ViewState["TipoTransaccionGenerica"] = value;
            }
        }

        public Confirmacion()
        {
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            IObjetoGenerico objetoTransaccion = (IObjetoGenerico)this.tipotransaccion.ObjetoTransaccion;
            base.Response.Redirect(objetoTransaccion.PaginaAnterior);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {            
            IObjetoGenerico objetoTransaccion = (IObjetoGenerico)this.tipotransaccion.ObjetoTransaccion;
            string str = "";
            bool flag = false;
            if (objetoTransaccion.GetType() == typeof(GAddNotificacion))
            {
                objetoTransaccion.EjecutarAccion();
            }
            else if (objetoTransaccion.GetType() == typeof(GAddExtraEfectivo))
            {
                objetoTransaccion.EjecutarAccion();
                //str = objetoTransaccion.EjecutarAccion();
                //this.txtPassword.Text = string.Empty;
                //this.liTextoReferencia.Text = "Número de Orden:";
                //this.liReferencia.Text = str;
                //this.PanelReferencia.Visible = true;
            }
            else
            {
                try
                {
                    this.ValidarCampos();
                    str = objetoTransaccion.EjecutarAccion();
                    this.txtPassword.Text = string.Empty;
                    if (objetoTransaccion.GetType() == typeof(GReferenciaBancaria))
                    {
                        this.encabezado.Lista.Rows[0].BeginEdit();
                        this.encabezado.Lista.Rows[0][2] = str;
                        this.encabezado.Lista.Rows[0].AcceptChanges();
                        this.dgDatosRecibo.DataSource = this.encabezado.Lista;
                        this.dgDatosRecibo.DataBind();
                        this.PanelReferencia.Visible = true;
                        this.liTextoReferencia.Visible = false;
                        this.liReferencia.Visible = false;
                        this.liTitulo.Text = "&nbsp;";
                        this.Session.Add("referencia", objetoTransaccion);
                        this.btnImprimirRecibo.Visible = true;
                    }
                    else if (objetoTransaccion.GetType() == typeof(GActualizarDatos))
                    {
                        base.Afiliado.CO_Celular = ((GActualizarDatos)objetoTransaccion).Celular;
                    }
                    else if (objetoTransaccion.GetType() == typeof(GSuspensionCheq))
                    {
                        this.panelCheques.Visible = true;
                    }
                    else if ((objetoTransaccion.GetType() != typeof(GTransferenciasPagos) ? false : objetoTransaccion.PaginaAnterior.Contains("RegistroOrden")))
                    {
                        this.liTextoReferencia.Text = "Número de Orden:";
                        this.liReferencia.Text = str;
                        this.PanelReferencia.Visible = true;
                    }
                    else
                    {
                        this.liReferencia.Text = str;
                        this.PanelReferencia.Visible = true;
                    }
                }
                catch (IBException bException1)
                {
                    IBException bException = bException1;
                    flag = true;
                    if (bException.CodigoSistema.Equals("IBSX"))
                    {
                        WebUtils.MessageBootstrap(this, string.Concat("Esta transacción presento problemas en su ejecución, por favor verifique antes de realizar otra operación (", bException.ReturnCode, ")"), null);
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
                    WebUtils.MessageBootstrap(this, exception.Message, this.btnConfirmar);
                   return;
                }
            }

            if (!flag)
            {
                this.panelConfirmacion.Visible = false;
                this.panelRecibo.Visible = true;
                this.liNota.Visible = true;
                this.liNota3.Visible = true;
                this.lblFechaRecibo.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss t");
                this.lblNombreUsuarioRecibo.Text = string.Concat(base.Afiliado.sCO_Nombres, " ", base.Afiliado.sCO_Apellidos);
                this.liNota.Text = "se realizó exitósamente";
                this.liNota3.Text = "Te recomendamos Imprimir este recibo para tu control y constancia de tu operación";
                this.panelBotonImpresion.Visible = true;
                ((BAVMaster)base.Master).TituloPage = this.tipotransaccion.Titulo.Replace("Confirmación", "Recibo");
            }
        }

        protected void btnContinuarSMS_Click(object sender, EventArgs e)
        {
            this.panelMSG.Visible = false;
            this.panelConfirmacion.Visible = true;
            string str = HelperAfiliado.CrearClaveTransaccionTemporal(base.Afiliado.nAF_Id);
            HelperEnvioCorreo.EnviarCorreoClaveTemporal(string.Concat(base.Afiliado.sCO_Nombres, " ", base.Afiliado.sCO_Apellidos), base.Afiliado.CO_Email, str);
            if (!string.IsNullOrEmpty(base.Afiliado.CO_Celular))
            {
                HelperTedexis.sendSMS(base.Afiliado.CO_Celular, string.Concat("BAV informa que su Clave de Transacciones Temporal es: ", str, ". Si usted no esta realizando esta operacion llame al 0500-288.00.01"));
            }
            this.Session.Add("SMS", true);
        }

        protected void btnImprimirRecibo_Click(object sender, EventArgs e)
        {
            string.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), "/pages/Servicios/Referencias/ReferenciaImpresion.aspx");
            StringWriter stringWriter = new StringWriter();
            this.Context.Server.Execute("~/pages/Servicios/Referencias/ReferenciaImpresion.aspx", stringWriter);
            StringBuilder stringBuilder = stringWriter.GetStringBuilder();
            byte[] pdf = HelperPDF.getPdf(stringBuilder.ToString(), "");
            HelperPDF.DownloadAsPDF(pdf, "Referencia.pdf");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Context.Items.Add("TipoTransaccionGenerica", this.tipotransaccion);
            IObjetoGenerico objetoTransaccion = (IObjetoGenerico)this.tipotransaccion.ObjetoTransaccion;
            if (!objetoTransaccion.PaginaAnterior.Contains("RegistroOrden.aspx"))
            {
                base.Server.Transfer(objetoTransaccion.PaginaAnterior);
            }
            else
            {
                base.Response.Redirect(objetoTransaccion.PaginaAnterior);
            }
        }

        protected void btnRegresar2_Click(object sender, EventArgs e)
        {
            IObjetoGenerico objetoTransaccion = (IObjetoGenerico)this.tipotransaccion.ObjetoTransaccion;
            base.Response.Redirect(objetoTransaccion.PaginaSiguiente);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Server);
            HttpCachePolicy cache = HttpContext.Current.Response.Cache;
            DateTime now = DateTime.Now;
            cache.SetExpires(now.AddSeconds(-100));
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Current.Response.ExpiresAbsolute = new DateTime(1970, 1, 1);
            Literal str = this.liFecha;
            now = DateTime.Now;
            str.Text = now.ToString("dd/MM/yyyy hh:mm");
            this.liiFrame.Text = "<iframe id=\"iImpresion\" name=\"iImpresion\" width=\"0px\" height=\"0px\" frameborder='no' src=\"\"></iframe>";
            this.liiFrame.Visible = true;
            if (this.tipotransaccion == null)
            {
                this.tipotransaccion = (TipoTransaccionGenerica)this.Context.Items["TipoTransaccionGenerica"];
                this.encabezado = this.tipotransaccion.EncabezadoTransaccion;
                if (this.tipotransaccion.IOpRep > 0)
                {
                    WebUtils.MessageBox2005(this, "BAV En Línea detectó que  realizaste una transacción que posee el mismo  origen, destino y monto el día de hoy. \n\n Si deseas realizar la operación continúa el procedimiento normal, de lo contrario aborta la transacción en el botón \n CANCELAR después de aceptar este mensaje. ");
                }
            }
            this.lblTitlePages.Text = this.tipotransaccion.Titulo.Replace("Confirmación", "Recibo");
            this.lblFechaRecibo.Text = ((BAVMaster)base.Master).Fecha;
            this.lblNombreUsuarioRecibo.Text = ((BAVMaster)base.Master).Usuario;
            ((BAVMaster)base.Master).TituloPage = this.tipotransaccion.Titulo;
            this.liNota.Visible = !this.tipotransaccion.Nota.Equals("");
            this.liNota.Text = this.tipotransaccion.Nota;
            this.liNota2.Visible = !this.tipotransaccion.Nota2.Equals("");
            this.liNota2.Text = this.tipotransaccion.Nota2;
            IObjetoGenerico objetoTransaccion = (IObjetoGenerico)this.tipotransaccion.ObjetoTransaccion;
            this.lblResultado.Text = objetoTransaccion.MensajeSatisfactorio;
            this.rptDatos.DataSource = this.encabezado.Lista;
            this.rptDatos.DataBind();
            this.dgDatosRecibo.DataSource = this.encabezado.Lista;
            this.dgDatosRecibo.DataBind();
            this.PanelReferencia.Visible = false;
            this.panelBotonImpresion.Visible = false;
            if (!base.IsPostBack)
            {
                if (base.Tipo_Seguridad == 1)
                {
                    this.trClaveTransacciones.Visible = true;
                    if (this.Session["SMS"] != null)
                    {
                        this.panelMSG.Visible = false;
                        this.panelConfirmacion.Visible = true;
                        return;
                    }
                }
                else
                {
                    this.trClaveTransacciones.Visible = false;
                    this.panelMSG.Visible = false;
                    this.panelConfirmacion.Visible = true;
                }
            }
        }

        public void RepeaterActivo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal)e.Item.FindControl("liCuenta");
            }
        }

        public void RepeaterPasivo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal)e.Item.FindControl("liTarjeta");
            }
        }

        private void ValidarCampos()
        {
            string str;
            if (base.Tipo_Seguridad == 1)
            {
                if (this.txtPassword.Text.Trim().Equals(string.Empty))
                {
                    throw new Exception("La Clave de Transacciones es requerida");
                }
                if (!HelperLogons.ValidarClaveTransacciones(base.Afiliado.nAF_Id, this.txtPassword.Text.Trim(), WebUtils.GetClientIP(this), base.sCod))
                {
                    throw new Exception("La Clave de Transacciones no es válida");
                }
                this.txtPassword.Text = string.Empty;
            }
            IObjetoGenerico objetoTransaccion = (IObjetoGenerico)this.tipotransaccion.ObjetoTransaccion;
            if (objetoTransaccion.GetType() == typeof(GTransferenciasPagos))
            {
                GTransferenciasPagos gTransferenciasPago = (GTransferenciasPagos)objetoTransaccion;
                string str1 = gTransferenciasPago.TipoTransaccion.ToString();
                if (base.sCod == 30)
                {
                    if (str1 != "PagoServicioCANTV")
                    {
                        str = (str1 == "PagoServicioCANTVNET" ? "PACNN" : "PAMOV");
                    }
                    else
                    {
                        str = "PAGSE";
                    }
                    str1 = str;
                }
                else if (str1.Equals("TransferenciaMismoTitularBAV"))
                {
                    str1 = "TBAMT";
                }
                else if (str1.Equals("TransferenciaTercerosBAV"))
                {
                    str1 = "TBTER";
                }
                else if (str1.Equals("TransferenciaOtrosBancosMismoTitular"))
                {
                    str1 = "TBOMT";
                }
                else if (str1.Equals("TransferenciaOtrosBancosTerceros"))
                {
                    str1 = "TBOTE";
                }
                else if (str1.Equals("PagoTarjetaCreditoMismoTitularBAV"))
                {
                    str1 = "PTBMT";
                }
                else if (str1.Equals("PagoTarjetaCreditoTercerosBAV"))
                {
                    str1 = "PTBTE";
                }
                else if (str1.Equals("PagoTarjetaCreditoOtrosBancosMismoTitular"))
                {
                    str1 = "PTOMT";
                }
                else if (str1.Equals("PagoTarjetaCreditoOtrosBancosTerceros"))
                {
                    str1 = "PTOTE";
                }
                else
                {
                    str1 = (!str1.Equals("PagoServicioElectricidadCaracas") ? "" : "PAEDC");
                }
                if ((str1.Equals("") ? false : HelperTransaccion.AcumuladorTransVerifyUpdate(true, base.Afiliado.nAF_Id, gTransferenciasPago.Monto, base.sCod, str1)))
                {
                    throw new Exception("El Monto supera al máximo diario");
                }
            }
        }
    }
}