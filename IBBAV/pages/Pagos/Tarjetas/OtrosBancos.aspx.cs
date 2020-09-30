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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Pagos.Tarjetas
{
    public partial class OtrosBancos : Principal
    {
        public OtrosBancos()
        {
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            TipoTransaccionGenerica tipoTransaccionGenerica = new TipoTransaccionGenerica();
            try
            {
                this.Validar();
            }
            catch (Exception exception)
            {
                //Modificado 28/08/2018 por Liliana Guerra el metodo original muestra la mensajeria con estilo CSS. 
                //Se sustituye por el metodo ´para mostrar mesnaje en un alert.
                WebUtils.MessageBox2005(this, exception.Message.ToString());

                return;
            }
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod);
            AfiliadoFavorito afiliadoFavorito = this.ddlCtaAcreditar.getAfiliadoFavorito();
            gTransferenciasPago.CtaDebitar = this.ddlCtaDebitar.getCuenta().SNroCuenta;
            gTransferenciasPago.Monto = decimal.Parse(this.txtMonto.Text);
            gTransferenciasPago.SCodBco = this.TxtCodeBanco.Text;
            gTransferenciasPago.PaginaSiguiente = "~/pages/consolidada.aspx";
            gTransferenciasPago.MensajeSatisfactorio = "Transacción realizada exitosamente ";
            tipoTransaccionGenerica.Nota = IBBAVConfiguration.ConfirmarTransaccion;
            gTransferenciasPago.Beneficiario = afiliadoFavorito.Beneficiario;
            gTransferenciasPago.CedulaBeneficiario = afiliadoFavorito.CedulaRif;
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Debitar:", "El Pago de la cuenta:", Formatos.formatoCuenta(gTransferenciasPago.CtaDebitar));
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Banco Destino:", "Al Banco:", this.txtBanco.Text);
            if (base.sCod == 14)
            {
                gTransferenciasPago.CtaAcreditar = afiliadoFavorito.NumeroInstrumento;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular;
                tipoTransaccionGenerica.Titulo = "Confirmación de Pago de Tarjeta de Crédito Mismo Titular a otros bancos";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Pagos/Tarjetas/OtrosBancos.aspx?type=0&sCod=", base.sCod);
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Tarjeta a Transferir:", "A la Tarjeta:", Formatos.formatoTarjeta(gTransferenciasPago.CtaAcreditar));
            }
            else
            {
                gTransferenciasPago.CtaAcreditar = afiliadoFavorito.NumeroInstrumento;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros;
                gTransferenciasPago.Email = this.txtCorreo.Text;
                tipoTransaccionGenerica.Titulo = "Confirmación de Pago de Tarjeta de Crédito a Terceros a otros bancos";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Pagos/Tarjetas/OtrosBancos.aspx?type=0&sCod=", base.sCod);
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Tarjeta a Transferir:", "A la Tarjeta:", gTransferenciasPago.CtaAcreditar);
            }
            gTransferenciasPago.NumBanco = HelperGlobal.BankNameGet(afiliadoFavorito.BankId);
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Beneficiario:", "Cuyo Titular es:", Formatos.formatocedula(gTransferenciasPago.CedulaBeneficiario));
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cédula/RIF Beneficiario:", "Con Cédula/RIF:", gTransferenciasPago.Beneficiario);
            gTransferenciasPago.Concepto = this.txtConcepto.Text;
            tipoTransaccionGenerica.ObjetoTransaccion = gTransferenciasPago;
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Monto a Transferir:", "Por un Monto de BsF.:", gTransferenciasPago.Monto.ToString());
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Concepto:", "Bajo el concepto:", gTransferenciasPago.Concepto);
            tipoTransaccionGenerica.IOpRep = HelperGlobal.GetNumeroOperDiarias(base.Afiliado.nAF_Id, gTransferenciasPago.CtaDebitar, gTransferenciasPago.CtaAcreditar, this.txtMonto.Text);
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
        }

        protected void ddlCtaAcreditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnConfirmar.Enabled = false;
            this.txtTarjetaPagar.Text = string.Empty;
            this.TxtCodeBanco.Text = string.Empty;
            this.txtBanco.Text = string.Empty;
            this.txtBeneficiario.Text = string.Empty;
            this.txtCedulaBeneficiario.Text = string.Empty;
            this.txtCorreo.Text = string.Empty;
            if (this.ddlCtaAcreditar.SelectedIndex > 0)
            {
                AfiliadoFavorito afiliadoFavorito = this.ddlCtaAcreditar.getAfiliadoFavorito();
                if (HelperGlobal.BancosGet(false).Find((Banco b) => b.BANK_ID.Trim().Equals(afiliadoFavorito.BankId.Trim())) == null)
                {
                    WebUtils.MessageBox2005(this, "La Institución financiera no se encuentra activa");
                }
                else
                {
                    this.txtTarjetaPagar.Text = afiliadoFavorito.NumeroInstrumento;
                    this.TxtCodeBanco.Text = afiliadoFavorito.BankId;
                    this.txtBanco.Text = HelperGlobal.BankNameGet(this.TxtCodeBanco.Text);
                    this.txtBeneficiario.Text = afiliadoFavorito.Beneficiario;
                    this.txtCedulaBeneficiario.Text = afiliadoFavorito.CedulaRif;
                    this.txtCorreo.Text = afiliadoFavorito.Email.Trim();
                    this.btnConfirmar.Enabled = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RangoIP rangoIP = new RangoIP();
            ValidacionesViaje validacion = new ValidacionesViaje();

            if ((base.sCod != 14) && (!rangoIP.ValidaConexion(base.Afiliado.sIP)))
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

            this.txtMonto.Attributes.Add("onKeypress", "return OnlyNumericXDecimal(event,this,2,',');");
            this.txtMonto.Attributes.Add("onKeyup", "return ReplacePointToComma(this);");
            this.txtMonto.Attributes.Add("onBlur", "return Refill(this);");
            this.liLimites.Text = string.Concat("Limite diario en operaciones desde BAV en Línea mínimo de Bs. ", Formatos.formatoMonto(base.Min), "  hasta un máximo de Bs. ", Formatos.formatoMonto(base.Max));
            if (base.MontoComision > new decimal(0))
            {
                this.liComision.Text = string.Concat("Nota: Esta transacción generará una comisión de Bs.F. ", Formatos.formatoMonto(base.MontoComision));

            }
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
                    this.ddlCtaAcreditar.TipoCombo = TipoCombo.CuentasFavoritos;
                    if (base.sCod == 14)
                    {
                        this.trCorreo.Visible = false;
                        this.ddlCtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular;
                        this.liDescription.Text = "Paga tus Tarjetas de Crédito de otros Bancos, de forma rápida y segura.";
                    }
                    else
                    {
                        this.ddlCtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros;
                        this.liDescription.Text = "Paga las Tarjetas de Crédito de Terceros en otros Bancos, de forma rápida y segura.";
                    }
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        TipoTransaccionGenerica item = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
                        GTransferenciasPagos objetoTransaccion = item.ObjetoTransaccion as GTransferenciasPagos;
                        this.ddlCtaDebitar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaDebitar);
                        TextBox textBox = this.txtMonto;
                        decimal monto = objetoTransaccion.Monto;
                        textBox.Text = monto.ToString().Replace(".", "");
                        this.ddlCtaAcreditar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaAcreditar);
                        this.txtConcepto.Text = objetoTransaccion.Concepto;
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
                    WebUtils.MessageBox2005(this, bException.IBMessage);
                    return;
                }
                if ((this.ddlCtaAcreditar.TipoCombo != TipoCombo.CuentasFavoritos ? false : this.ddlCtaAcreditar.Items.Count == 1))
                {
                    WebUtils.MessageBootstrap(this, string.Concat("No existen Favoritos para este tipo de transacción. Para crearlos, ingresa a la opción del menú \"Administración y Seguridad / Registro de Favoritos\" o presione <a href=\"", base.ResolveUrl("~/pages/IB/Favoritos/MenuFavoritos.aspx?sCod=22"), "\">aquí</a>"), null);
                }
            }
        }

        private void Validar()
        {
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod);
            if (this.ddlCtaDebitar.SelectedValue == "0")
            {
                throw new Exception("Debe indicar la cuenta a Debitar");
            }
            if (this.ddlCtaAcreditar.SelectedValue == "0")
            {
                throw new Exception("Debe indicar la tarjeta a pagar");
            }
            if (this.ddlCtaAcreditar.SelectedValue.Equals(this.ddlCtaDebitar.SelectedValue))
            {
                throw new Exception("La cuenta a debitar y la cuenta a acreditar son iguales");
            }
            decimal montoComision = base.MontoComision;
            decimal num = decimal.Parse(this.ddlCtaDebitar.getCuenta().SDisponible.Replace('.', ','));
            gTransferenciasPago.Monto = decimal.Parse(this.txtMonto.Text) + base.MontoComision;
            if (gTransferenciasPago.Monto > num)
            {
                throw new Exception("El monto a transferir es mayor al disponible de la cuenta seleccionada, por favor, verifique. Recuerde que esta transacción genera comisión");
            }
            string str = this.txtMonto.Text.Trim();
            string str1 = "";
            if (str.Length == 0)
            {
                throw new Exception("Debe indicar el monto de la transferencia");
            }
            //Modificado 30/07/2018 por Liliana Guerra 
            //if (!eFunctions.validarMonto(ref str, ref str1, base.Min, base.Max))
            if (!eFunctions.validarMonto(ref str, ref str1, base.Min, base.MtoLimiteTrans))
            {
                throw new Exception(str1);
            }
            if (HelperTransaccion.AcumuladorTransVerifyUpdate(true, base.Afiliado.nAF_Id, decimal.Parse(str), base.sCod, (base.sCod != 14 ? "PTOTE" : "PTOMT")))
            {
                throw new Exception("El Monto supera al máximo diario");
            }
            if (this.txtConcepto.Text.Length > 80)
            {
                throw new Exception("La longitud del Concepto no puede exceder los 80 caracteres.");
            }
            if ((this.txtCorreo.Text.Length <= 0 ? false : !Tools.TestEmailRegex(this.txtCorreo.Text)))
            {
                throw new Exception("El correo electrónico no es válido");
            }
            this.txtMonto.Text = str;
        }
    }
}