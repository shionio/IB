using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.Entidades.Notificaciones;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace IBBAV.pages.Tarjetas
{
    public partial class EnBAV : Principal
    {
        public EnBAV()
        {
        }

        protected void Aceptar_Click(object sender, EventArgs e)
        {
            TipoTransaccionGenerica tipoTransaccionGenerica = new TipoTransaccionGenerica();
            try
            {
                this.Validar();
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message.ToString());
                return;
            }
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod)
            {
                CtaDebitar = this.ddlCtaDebitar.getCuenta().SNroCuenta,
                Monto = decimal.Parse(this.txtMonto.Text),
                PaginaSiguiente = "~/pages/consolidada.aspx",
                MensajeSatisfactorio = "Transacción realizada exitosamente "
            };
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Debitar:", "El pago de la cuenta:", Formatos.formatoCuenta(gTransferenciasPago.CtaDebitar));
            if (base.sCod == 13)
            {
                if (this.rb_actual.Checked)
                {
                    gTransferenciasPago.RdoSelected = 1;
                }
                if (this.rb_minimo.Checked)
                {
                    gTransferenciasPago.RdoSelected = 2;
                }
                if (this.rb_otro.Checked)
                {
                    gTransferenciasPago.RdoSelected = 3;
                }
                gTransferenciasPago.Instrumento = this.ddlCtaAcreditar.getCuenta().SNroCuenta;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.PagoTarjetaCreditoMismoTitularBAV;
                tipoTransaccionGenerica.Titulo = "Confirmación de Pagos de Tarjetas de Crédito Mismo Titular en BAV";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Pagos/Tarjetas/EnBAV.aspx?type=0&sCod=", base.sCod);
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Transferir:", "A la Tarjeta:", Formatos.formatoTarjeta(gTransferenciasPago.Instrumento));
            }
            else
            {
                gTransferenciasPago.Instrumento = this.ddlCtaAcreditar.getAfiliadoFavorito().NumeroInstrumento;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV;
                gTransferenciasPago.CedulaBeneficiario = this.ddlCtaAcreditar.getAfiliadoFavorito().CedulaRif;
                gTransferenciasPago.Beneficiario = this.ddlCtaAcreditar.getAfiliadoFavorito().Beneficiario;
                gTransferenciasPago.Concepto = this.txtConcepto.Text;
                gTransferenciasPago.Email = this.TxtCorreo.Text;
                tipoTransaccionGenerica.Titulo = "Confirmación de Pagos de Tarjetas de Crédito a Terceros  en BAV";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Pagos/Tarjetas/EnBAV.aspx?type=0&sCod=", base.sCod);
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Transferir:", "A la Tarjeta:", gTransferenciasPago.Instrumento);
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Beneficiario:", "Cuyo titular es:", gTransferenciasPago.Beneficiario);
            }
            if (gTransferenciasPago.Instrumento.Trim().StartsWith("4"))
            {
                gTransferenciasPago.CtaAcreditar = base.CuentaAdministrativa1;
            }
            if (gTransferenciasPago.Instrumento.Trim().StartsWith("5"))
            {
                gTransferenciasPago.CtaAcreditar = base.CuentaAdministrativa2;
            }
            tipoTransaccionGenerica.ObjetoTransaccion = gTransferenciasPago;
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Monto a Transferir:", "Por un Monto de BsF.:", gTransferenciasPago.Monto.ToString());
            if (base.sCod != 24)
            {
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Concepto:", "Bajo el Concepto de:", gTransferenciasPago.Concepto);
            }
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
        }

        protected void BtCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void ddlCtaAcreditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.liSaldoActual.Text = "0,00";
            this.liPagoMinimo.Text = "0,00";
            this.txtTarjetaPagar.Text = string.Empty;
            this.TxtCorreo.Text = string.Empty;
            this.txtOtroMonto.Text = "0,00";
            this.BtAceptar.Enabled = false;
            this.panelTipoPago.Enabled = false;
            bool flag = false;
            if (this.ddlCtaAcreditar.SelectedIndex > 0)
            {
                if (base.sCod == 13)
                {
                    string sNroCuenta = this.ddlCtaAcreditar.getCuenta().SNroCuenta;
                    try
                    {
                        RespuestaContdcdsjv respuestaContdcdsjv = HelperIbs.ibsConSaldoTdc(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, sNroCuenta);
                        TextBox str = this.txtFechaPago;
                        DateTime fecha = Formatos.ISOToFecha(respuestaContdcdsjv.contdcdsjv.SFechaProxPgo);
                        str.Text = fecha.ToString("dd/MM/yyyy");
                        this.liPagoMinimo.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaContdcdsjv.contdcdsjv.SCuotaMes));
                        this.liSaldoActual.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaContdcdsjv.contdcdsjv.SSaldoActual));
                        if (this.Context.Items["TipoTransaccionGenerica"] != null)
                        {
                            TipoTransaccionGenerica item = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
                            GTransferenciasPagos objetoTransaccion = (GTransferenciasPagos)item.ObjetoTransaccion;
                            if (objetoTransaccion.RdoSelected == 1)
                            {
                                this.rb_actual.Checked = true;
                            }
                            if (objetoTransaccion.RdoSelected == 2)
                            {
                                this.rb_minimo.Checked = true;
                            }
                            if (objetoTransaccion.RdoSelected == 3)
                            {
                                this.rb_otro.Checked = true;
                                this.txtMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                            }
                        }
                        this.UpdatePanel1.Update();
                        flag = true;
                    }
                    catch (IBException bException)
                    {
                        WebUtils.MessageBox2005(this, "Sistema de consulta de saldos de tarjetas de créditos no disponible en este momento, por favor intente más tarde.");
                        return;
                    }
                    catch (Exception exception)
                    {
                        WebUtils.MessageBox2005(this, exception.Message);
                        return;
                    }
                }
                else
                {
                    AfiliadoFavorito afiliadoFavorito = this.ddlCtaAcreditar.getAfiliadoFavorito();
                    this.txtTarjetaPagar.Text = afiliadoFavorito.NumeroInstrumento;
                    this.TxtCorreo.Text = afiliadoFavorito.Email.Trim();
                    flag = true;
                }
                if (flag)
                {
                    this.panelTipoPago.Enabled = true;
                    this.BtAceptar.Enabled = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RangoIP rangoIP = new RangoIP();
            ValidacionesViaje validacion = new ValidacionesViaje();

            if ((base.sCod != 13) && (!rangoIP.ValidaConexion(base.Afiliado.sIP)))
            {
                // ****** Captura el tipo y numero de instrumento para agregar a la notificacion ***//
                RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(base.Afiliado.AF_CodCliente.ToString(), base.Afiliado.sAF_Rif, TipoConsultaCuentasIBS.Todas);
                List<IbaConsDet> ibaConsDets = new List<IbaConsDet>();
                ibaConsDets.AddRange(respuestaIbaCon.sdjvCuentas.sdsjvDetalle);
                IbaConsDet dataItem = ibaConsDets.Find((IbaConsDet x) => !x.STipoFirma.Contains("N"));
                string tipoInstrumento = dataItem.STipocuenta;
                string numInstrumento = dataItem.SNroCuenta;
                string ipDec = rangoIP.StringToInt(base.Afiliado.sIP);
                // ************************************************************************************//

                if (validacion.AfiliadoRestricc(base.Afiliado.nAF_Id, base.Afiliado.sIP, ipDec, tipoInstrumento, numInstrumento))
                {
                    this.UpdatePanel1.Visible = false;
                    this.mensaje.Visible = false;
                    this.pnlRestriccion.Visible = true;
                }
                else
                {
                    Notificacion ultDestino = Notificacion.UltimoDestino(base.Afiliado.nAF_Id);
                    DateTime fechaVence = Convert.ToDateTime(ultDestino.FechaFin);
                    DateTime hoy = DateTime.Today;

                    if (fechaVence < hoy)
                    {
                        this.UpdatePanel1.Visible = false;
                        this.mensaje.Visible = false;
                        this.pnlRestriccion.Visible = true;
                        if (HelperNotificacionIBP.NotificacionVencida(base.Afiliado.nAF_Id))
                        {
                            WebUtils.MessageBootstrap(this, string.Concat("Su notificación de viaje a caducado. Para continuar realizando operaciones desde el exterior debe crear una nueva notificación. Para crear una nueva pulse <a href=\"", base.ResolveUrl("~/pages/NotificacionViajes/NotificacionNueva.aspx"), "\">aquí</a>"), null);
                        }
                        else
                        {
                            WebUtils.MessageBootstrap(this, string.Concat("Su notificación de viaje a caducado o presenta problemas, para continuar realizando operaciones a través de BAV en Línea, comuníquese con centro de atención al cliente <a>0800.228.00.00</a>"), null);
                        }
                    }
                }
            }

            this.BtAceptar.Attributes.Add("onClick", string.Concat("this.disabled = true;", base.GetPostBackEventReference(this.BtAceptar)));
            this.txtMonto.Attributes.Add("onKeypress", "return OnlyNumericXDecimal(event,this,2,',');");
            this.txtOtroMonto.Attributes.Add("onKeypress", "return OnlyNumericXDecimal(event,this,2,',');");
            this.txtMonto.Attributes.Add("onKeyup", "return ReplacePointToComma(this);");
            this.txtOtroMonto.Attributes.Add("onKeyup", "return ReplacePointToComma(this);");
            this.txtMonto.Attributes.Add("onBlur", "return Refill(this);");
            this.txtOtroMonto.Attributes.Add("onBlur", "return Refill(this);");
            this.lblLimites.Text = string.Concat("Limite diario en operaciones desde BAV en Línea mínimo de Bs. ", Formatos.formatoMonto(base.Min), Environment.NewLine, " hasta un máximo de Bs. ", Formatos.formatoMonto(base.Max));
            AttributeCollection attributes = this.rb_actual.Attributes;
            attributes.Add("onClick", string.Concat(new string[] { "$('", this.txtOtroMonto.ClientID, "').value ='0,00'; $('", this.txtOtroMonto.ClientID, "').setAttribute('readOnly','readonly');" }));
            AttributeCollection attributeCollection = this.rb_minimo.Attributes;
            attributeCollection.Add("onClick", string.Concat(new string[] { "$('", this.txtOtroMonto.ClientID, "').value ='0,00'; $('", this.txtOtroMonto.ClientID, "').setAttribute('readOnly','readonly');" }));
            AttributeCollection attributes1 = this.rb_otro.Attributes;
            attributes1.Add("onClick", string.Concat(new string[] { "$('", this.txtOtroMonto.ClientID, "').value ='';$('", this.txtOtroMonto.ClientID, "').removeAttribute('readOnly');$('", this.txtOtroMonto.ClientID, "').focus()" }));
            if (!base.IsPostBack)
            {
                try
                {
                    this.ddlCtaDebitar.HasTextoInicial = true;
                    this.ddlCtaDebitar.TextoInicial = "Seleccione";
                    this.ddlCtaDebitar.TipoCombo = TipoCombo.CuentasCliente;
                    this.ddlCtaDebitar.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.CuentasDebito;
                    this.ddlCtaDebitar.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaConDisponibleDescripcion;
                    this.ddlCtaAcreditar.HasTextoInicial = true;
                    this.ddlCtaAcreditar.TextoInicial = "Seleccione";
                    if (base.sCod == 13)
                    {
                        this.trFechaPago.Visible = true;
                        this.panelTerceros.Visible = false;
                        this.panelTipoPago.Visible = true;
                        this.ddlCtaAcreditar.TipoCombo = TipoCombo.CuentasCliente;
                        this.ddlCtaAcreditar.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.TarjetasCredito;
                        this.lblDescription.Text = "Paga tus tarjetas de crédito de forma rápida y segura por BAV en línea.";
                    }
                    else
                    {
                        this.trFechaPago.Visible = false;
                        this.liDescripcionTarjeta.Text = "Registrados:";
                        this.panelTipoPago.Visible = false;
                        this.panelTerceros.Visible = true;
                        this.ddlCtaAcreditar.TipoCombo = TipoCombo.CuentasFavoritos;
                        this.ddlCtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV;
                        this.lblDescription.Text = "Paga tarjetas de crédito de terceros en BAV en línea de forma fácil y segura.";
                    }
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        TipoTransaccionGenerica item = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
                        GTransferenciasPagos objetoTransaccion = (GTransferenciasPagos)item.ObjetoTransaccion;
                        this.ddlCtaDebitar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaDebitar);
                        this.ddlCtaAcreditar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.Instrumento);
                        if (base.sCod == 13)
                        {
                            if ((objetoTransaccion.RdoSelected == 1 ? true : objetoTransaccion.RdoSelected == 2))
                            {
                                this.txtOtroMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                            }
                            if (objetoTransaccion.RdoSelected == 3)
                            {
                                this.txtOtroMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                            }
                        }
                        else
                        {
                            if ((objetoTransaccion.RdoSelected == 1 ? true : objetoTransaccion.RdoSelected == 2))
                            {
                                this.txtMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                            }
                            if (objetoTransaccion.RdoSelected == 3)
                            {
                                this.txtMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                            }
                            this.TxtCorreo.Text = objetoTransaccion.Email;
                            this.txtConcepto.Text = objetoTransaccion.Concepto;
                        }
                    }
                    this.ddlCtaDebitar.BindCombo();
                    this.ddlCtaAcreditar.BindCombo();
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        this.ddlCtaAcreditar_SelectedIndexChanged(null, null);
                    }
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                    return;
                }
                if ((this.ddlCtaAcreditar.TipoCombo != TipoCombo.CuentasFavoritos ? false : this.ddlCtaAcreditar.Items.Count == 1))
                {
                    WebUtils.MessageBootstrap(this, string.Concat("No existen Favoritos para este tipo de transacción. Para crearlos, ingresa a la opción del menú \"Administración y Seguridad / Registro de Favoritos\" o presione <a href=\"", base.ResolveUrl("~/pages/IB/Favoritos/MenuFavoritos.aspx?sCod=22"), "\">aquí</a>"), null);
                }
                if ((this.ddlCtaAcreditar.TipoCombo != TipoCombo.CuentasCliente ? false : this.ddlCtaAcreditar.Items.Count == 1))
                {
                    WebUtils.MessageBootstrap(this, "Usted no posee Tarjetas de Crédito asociadas", null);
                }
            }
        }

        private void Validar()
        {
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod);
            if (this.ddlCtaDebitar.SelectedValue == "0")
            {
                throw new Exception("Debe indicar la cuenta a debitar");
            }
            if (this.ddlCtaAcreditar.SelectedValue == "0")
            {
                throw new Exception("Debe indicar la tarjeta a pagar");
            }
            decimal montoComision = base.MontoComision;
            decimal num = decimal.Parse(this.ddlCtaDebitar.getCuenta().SDisponible.Replace('.', ','));
            if (this.rb_actual.Checked)
            {
                gTransferenciasPago.Monto = decimal.Parse(this.liSaldoActual.Text) + base.MontoComision;
            }
            else if (this.rb_minimo.Checked)
            {
                gTransferenciasPago.Monto = decimal.Parse(this.liPagoMinimo.Text) + base.MontoComision;
            }
            else if (this.rb_otro.Checked)
            {
                gTransferenciasPago.Monto = decimal.Parse(this.txtOtroMonto.Text) + base.MontoComision;
            }
            if (gTransferenciasPago.Monto > num)
            {
                throw new Exception("El monto a transferir es mayor al disponible de la cuenta seleccionada, por favor, verifique");
            }
            if (base.sCod == 13)
            {
                if (this.rb_actual.Checked)
                {
                    this.txtMonto.Text = this.liSaldoActual.Text;
                    this.lblLimites.Text = this.txtMonto.Text;
                }
                if (this.rb_minimo.Checked)
                {
                    this.txtMonto.Text = this.liPagoMinimo.Text;
                    this.lblLimites.Text = this.txtMonto.Text;
                }
                if (this.rb_otro.Checked)
                {
                    this.txtMonto.Text = this.txtOtroMonto.Text;
                    this.lblLimites.Text = this.txtMonto.Text;
                }
            }
            string str = this.txtMonto.Text.Trim().Replace(".", "");
            string str1 = "";
            if (str.Length == 0)
            {
                throw new Exception("Debe indicar el monto a pagar");
            }
            //Modificado 30/07/2018 por Liliana Guerra 
            //if (!eFunctions.validarMonto(ref str, ref str1, base.Min, base.Max))
            if (!eFunctions.validarMonto(ref str, ref str1, base.Min, base.MtoLimiteTrans))
            {
                throw new Exception(str1);
            }
            if (HelperTransaccion.AcumuladorTransVerifyUpdate(true, base.Afiliado.nAF_Id, decimal.Parse(str), base.sCod, (base.sCod != 13 ? "PTBTE" : "PTBMT")))
            {
                throw new Exception("El Monto supera al máximo diario");
            }
            if (this.txtConcepto.Text.Length > 80)
            {
                throw new Exception("La longitud del Concepto no puede exceder los 80 caracteres.");
            }
            if ((this.TxtCorreo.Text.Length <= 0 ? false : !Tools.TestEmailRegex(this.TxtCorreo.Text.Trim())))
            {
                throw new Exception("El correo electrónico no es válido");
            }
            this.txtMonto.Text = str;
        }
    }
}