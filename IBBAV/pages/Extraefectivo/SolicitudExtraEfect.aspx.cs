using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IBBAV.Entidades.Notificaciones;
using System.Collections.Generic;
using IBBAV.Entidades.Extraefectivo;
using System.IO;
using System.Text;

namespace IBBAV.pages.Extraefectivo
{
    public partial class SolicitudExtraEfect : Principal
    {
        string cuotas = "";

        public SolicitudExtraEfect()
        {
            base.sCod = 0; //codigo de operacion en la base de datos
            base.NombrePantalla = "ExtraEfectivo";
        }

        private string cedula;

        public LineaExtracredito solicitud = new LineaExtracredito();


        protected void CtaAcreditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CtaAcreditar.SelectedIndex > 0)
            {
                //this.tarjetaCred.Text = String.Concat(this.TarjCredito.getExtraEfectivo().numeroTDC.Substring(0, 4), "********", this.TarjCredito.getExtraEfectivo().numeroTDC.Substring(12));
                this.txtMonto.Enabled = true;
                //this.txtdisponible.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(this.TarjCredito.getExtraEfectivo().saldo));
                this.txtCuentaAcreditar.Text = String.Concat(this.CtaAcreditar.getCuenta().SNroCuenta.Substring(0, 4), "************", this.CtaAcreditar.getCuenta().SNroCuenta.Substring(16));
            }
            else
            {
                this.txtCuentaAcreditar.Text = "";
            }
        }

        protected void TarjCredito_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.TarjCredito.SelectedIndex > 0)
            {
                //Luis
                this.txtdisponible.Text = this.TarjCredito.getExtraEfectivo().disponible;
                this.tarjetaCred.Text = String.Concat(this.TarjCredito.getExtraEfectivo().numeroTDC.Substring(0, 4), "********", this.TarjCredito.getExtraEfectivo().numeroTDC.Substring(12));
            }
            else {
                this.txtdisponible.Text = "";
                this.tarjetaCred.Text = "";
            }
        }

        protected void btnCancelar1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void btnConfirmar1_Click(object sender, EventArgs e)
        {
            this.txtMonto.Attributes.Add("onKeyup", "");
            this.txtMonto.Attributes.Add("onBlur", "");

            cedula = 'V' + base.Afiliado.cedRIF.PadLeft(9, '0');
            string solicitado = this.txtMonto.Text;
            solicitado = solicitado.Replace(".", "");
            solicitado = solicitado.Replace(".", "");
            solicitado = solicitado.Replace(".", "");
            solicitado = solicitado.Replace(".", "");
            solicitado = solicitado.Replace(",", "");

            this.solicitud = HelperExtracredito.consultaCuotas(cedula, this.CtaAcreditar.getCuenta().SNroCuenta, this.TarjCredito.getExtraEfectivo().numeroTDC, solicitado);

            if (solicitud.respuestaCod.Equals("000"))
            {
                Session["codigoResp"] = solicitud.respuestaCod;
                Session["cuota6"] = Formatos.formatoMontoAgrDecimal(solicitud.cuota6);
                Session["cuota12"] = Formatos.formatoMontoAgrDecimal(solicitud.cuota12);
                Session["cuota24"] = Formatos.formatoMontoAgrDecimal(solicitud.cuota24);
                Session["cuota36"] = Formatos.formatoMontoAgrDecimal(solicitud.cuota36);

                this.cuotasCant.Visible = true;
                this.MontoCuota.Visible = true;
                this.btnConfirmar2.Visible = true;
                this.TarjCredito.Enabled = false;
                this.CtaAcreditar.Enabled = false;
                this.btnConfirmar1.Visible = false;
                this.txtMonto.Enabled = false;
            }
            else
            {
                this.UpdatePanel1.Visible = false;
                WebUtils.MessageBootstrap(this, string.Concat(this.solicitud.respuestaDesc, ". Para regresar presione <a href=\"", base.ResolveUrl("~/pages/consolidada.aspx"), "\">aquí</a>"), null);
            }
        }

        protected void Cuotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtMonto.Attributes.Add("onKeyup", "");
            this.txtMonto.Attributes.Add("onBlur", "");

            if (this.CantCuotas.SelectedIndex <= 0)
            {
                this.txtMontoCuota.Text = string.Empty;
                Session["cuotaCantidad"] = 0; 
            }
            else if (this.CantCuotas.SelectedIndex == 1)
            {
                this.txtMontoCuota.Text = Session["cuota6"].ToString();
                Session["cuotaCantidad"] = 6;
            }
            else if (this.CantCuotas.SelectedIndex == 2)
            {
                this.txtMontoCuota.Text = Session["cuota12"].ToString();
                Session["cuotaCantidad"] = 12;
            }
            else if (this.CantCuotas.SelectedIndex == 3)
            {
                this.txtMontoCuota.Text = Session["cuota24"].ToString();
                Session["cuotaCantidad"] = 24;
            }
            else if (this.CantCuotas.SelectedIndex == 4)
            {
                this.txtMontoCuota.Text =Session["cuota36"].ToString();
                Session["cuotaCantidad"] = 36;
            }
            //INCLUIR CODIGO QUE CONSULTE A TRAVES DEL WS Y DEVUELVE EL MONTO DE LA CUOTA MENSUAL

        }

        protected void btnConfirmar2_Click(object sender, EventArgs e)
        {

            try
            {

                cedula = 'V' + base.Afiliado.cedRIF.PadLeft(9, '0');

                string solicitado = this.txtMonto.Text;
                solicitado = solicitado.Replace(".", "");
                solicitado = solicitado.Replace(".", "");
                solicitado = solicitado.Replace(".", "");
                solicitado = solicitado.Replace(".", "");
                solicitado = solicitado.Replace(",", "");

                string cuotaMes = this.txtMontoCuota.Text;
                cuotaMes = cuotaMes.Replace(".", "");
                cuotaMes = cuotaMes.Replace(",", "");

                string monto = this.txtMonto.Text;
                string cuentaAbono = this.CtaAcreditar.getCuenta().SNroCuenta;
                string tdc = this.TarjCredito.getExtraEfectivo().numeroTDC;
                string cuotas = this.CantCuotas.Text;
                //solicitado = "1200000";
                this.solicitud = HelperExtracredito.solicAprobacion(cedula, cuentaAbono, tdc, solicitado, cuotas, cuotaMes);
                
                //WebUtils.MessageBox(this, this.solicitud.respuestaCod + solicitud.respuestaDesc);

                if (solicitud.respuestaCod.Equals("000"))
                {
                    Session["tdc_numReferencia"] = solicitud.referencia;
                    Session["tdc_fechaRecibo"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss t");
                    Session["tdc_nombreUsuarioRecibo"] = string.Concat(base.Afiliado.sCO_Nombres, " ", base.Afiliado.sCO_Apellidos);
                    Session["tdc_tdc"] = Formatos.formatoTarjeta(tdc);
                    Session["tdc_cuentaAbono"] = Formatos.formatoCuenta(cuentaAbono);
                    Session["tdc_monto"] = monto;
                    Session["tdc_cuotas"] = cuotas;
                    Session["tdc_cuotaMes"] = Formatos.formatoMontoAgrDecimal(cuotaMes);

                    this.UpdatePanel1.Visible = false;
                    this.panelRecibo.Visible = true;
                    this.panelBotonImpresion.Visible = true;
                    this.liTextoReferencia.Text = "Número de Referencia: ";
                    this.liReferencia.Text = Session["tdc_numReferencia"].ToString();
                    this.PanelReferencia.Visible = true;
                    this.liNota.Visible = true;
                    this.liNota3.Visible = true;
                    this.lblFechaRecibo.Text = Session["tdc_fechaRecibo"].ToString();
                    this.lblNombreUsuarioRecibo.Text = Session["tdc_nombreUsuarioRecibo"].ToString();
                    this.liNota.Text = "se realizó exitósamente";
                    this.liNota3.Text = "Te recomendamos Imprimir este recibo para tu control y constancia de tu operación";
                    this.panelBotonImpresion.Visible = true;
                    ((BAVMaster)base.Master).TituloPage = "ExtraEfectivo";
                    
                    this.liDebito.Text = " Tarjeta de Crédito: ";
                    this.liValordebito.Text = Session["tdc_tdc"].ToString();
                    this.liCredito.Text = "Abonado en Cuenta:";
                    this.liValorcredito.Text = Session["tdc_cuentaAbono"].ToString();
                    this.liConcepto.Text = "Concepto:";
                    this.liValorConcepto.Text = "Extra Efectivo";
                    this.liMonto.Text = "Monto Abonado:";
                    this.liValormonto.Text = Session["tdc_monto"].ToString();
                    this.liTotalcuotas.Text = "Cantidad de cuotas a pagar:";
                    this.liValorcuotas.Text = Session["tdc_cuotas"].ToString();
                    this.liMontocuota.Text = "Monto mensual:";
                    this.liValormontocuota.Text = Session["tdc_cuotaMes"].ToString();

                    btnImprimirRecibo.Visible = true;

                    string var_monto = monto.Replace(".", "");
                    var_monto = var_monto.Replace(".", "");
                    var_monto = var_monto.Replace(".", "");
                    var_monto = var_monto.Replace(".", "");
                    var_monto = var_monto.Replace(".", "");
                    var_monto = var_monto.Replace(",", ".");
                    decimal var_monto_dec  = Formatos.ISOToDecimal(var_monto);

                   // string respuesta = HelperExtracredito.ejecutarAccionEETDC(Session["cedula_cliente"].ToString(), tdc, Session["cedula_cliente"].ToString(), cuentaAbono, var_monto_dec);
                   // Session["tdc_numReferencia"] = "res - " + respuesta;
                    /*
                    GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod)
                    {
                        CtaDebitar = cuentaAbono,
                        Monto = var_monto_dec,
                        CtaAcreditar = cuentaAbono,
                        PaginaSiguiente = "",
                        MensajeSatisfactorio = "Transacción realizada exitosamente "
                    };
                    //gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.ExtraEfectivoTDC;
                   string respuesta =  gTransferenciasPago.ejecutarAccionEETDC(var_monto_dec);
                     //string respuesta = gTransferenciasPago.ejecutarAccionEETDC();

                    Session["tdc_numReferencia"] = "res - " + respuesta;*/
                }
                else
                {
                    this.UpdatePanel1.Visible = false;
                    WebUtils.MessageBootstrap(this, string.Concat(this.solicitud.respuestaDesc, ". Para regresar presione <a href=\"", base.ResolveUrl("~/pages/consolidada.aspx"), "\">aquí</a>"), null);

                }


            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message.ToString());
                return;
            }

        }

        protected void btnImprimirRecibo_Click(object sender, EventArgs e)
        {
            string.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), "/pages/Extraefectivo/pdf/ReciboExtraEfect.aspx");
            StringWriter stringWriter = new StringWriter();
            this.Context.Server.Execute("~/pages/Extraefectivo/pdf/ReciboExtraEfect.aspx", stringWriter);
            StringBuilder stringBuilder = stringWriter.GetStringBuilder();
            byte[] pdf = HelperPDF.getPdf(stringBuilder.ToString(), "");
            HelperPDF.DownloadAsPDF(pdf, "ReciboExtraEfect.pdf");
        }

        protected void btnRegresar2_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TarjetaConsulta tarj = new TarjetaConsulta();
            cedula = 'V' + base.Afiliado.cedRIF.PadLeft(9, '0');
            Session["cedula_cliente"] = cedula;
            tarj = (TarjetaConsulta)HelperExtracredito.consulta(cedula)[0];
            //WebUtils.MessageBox(this, tarj.respuestaCod + "/" + tarj.numeroTDC + "/" + tarj.saldo);

            //this.txtMonto.Attributes.Add("onKeypress", "return OnlyNumericXDecimal(event,this,2,',');");
            //this.txtMonto.Attributes.Add("onKeyup", "return ReplacePointToComma(this);");
            
            if (!base.IsPostBack)
            {
                try
                {
                    this.txtMonto.Attributes.Add("onKeyup", "formatoMoneda();");
                    this.txtMonto.Attributes.Add("onBlur", "return Refill(this);");

                    this.TarjCredito.HasTextoInicial = true;
                    this.TarjCredito.TextoInicial = "Seleccione";
                    this.TarjCredito.TipoCombo = TipoCombo.ExtraEfectivoCte; // =6
                    this.TarjCredito.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaConDisponible;
                    this.CtaAcreditar.HasTextoInicial = true;
                    this.CtaAcreditar.TextoInicial = "Seleccione";
                    this.trCuentaAcreditar.Visible = true;
                    this.CtaAcreditar.TipoCombo = TipoCombo.CuentasCliente;
                    this.CtaAcreditar.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.CuentasCredito;
                    this.CtaAcreditar.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaConDisponibleDescripcion;
                    this.lblDescription.Text = "Solicite efectivo inmediato a su centa BAV.";

                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        TipoTransaccionGenerica item = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
                        GTransferenciasPagos objetoTransaccion = (GTransferenciasPagos)item.ObjetoTransaccion;
                        //this.CtaDebitar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaDebitar);
                        //this.CtaAcreditar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaAcreditar);
                        //this.txtMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                        //this.txtCorreo.Text = objetoTransaccion.Email;
                        //this.txtConcepto.Text = objetoTransaccion.Concepto;
                    }
                    this.TarjCredito.BindCombo();//luis:combo con tarjetas
                    this.CtaAcreditar.BindCombo();
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        this.CtaAcreditar_SelectedIndexChanged(null, null);
                    }

                }
                catch (IBException bException)
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                    return;
                }
                if ((this.CtaAcreditar.TipoCombo != TipoCombo.CuentasCliente ? false : this.CtaAcreditar.Items.Count == 1))
                {
                    WebUtils.MessageBootstrap(this, string.Concat("No dispone de una cuenta donde acreditar su Extraefectivo. Diríjase a una agencia o comuníquese al centro de atención al cliente."), null);
                }

                //this.txtMonto.Attributes.Add("onKeypress", "return OnlyNumericXDecimal(event,this,2,',');");
                //this.txtMonto.Attributes.Add("onKeyup", "return ReplacePointToComma(this);");
                //this.txtMonto.Attributes.Add("onKeyup", "formatoMoneda();");
                //this.txtMonto.Attributes.Add("onBlur", "return Refill(this);");
            }

        }


        private void Validar()
        {

            if (this.TarjCredito.SelectedIndex == 0)
            {
                throw new Exception("Debe indicar la tarjeta de crédito para el Extra Efectivo");
            }
            if (this.CtaAcreditar.SelectedIndex == 0)
            {
                throw new Exception("Debe indicar la cuenta a Acreditar");
            }
            decimal num = decimal.Parse(this.TarjCredito.getExtraEfectivo().disponible.Replace('.', ','));

            if (decimal.Parse(this.txtMonto.Text) > num)
            {
                throw new Exception("El monto a solicitar es mayor al disponible en el ExtraEfectivo, por favor, verifique");
            }
            string str = this.txtMonto.Text.Trim();
            if (str.Length == 0)
            {
                throw new Exception("Debe indicar el monto a solicitar");
            }
            this.txtMonto.Text = str;
        }
    }
}