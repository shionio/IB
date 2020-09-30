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

namespace IBBAV.pages.Transferencias
{
    public partial class OtrosBancos : Principal
    {
        public OtrosBancos()
        {
        }

        protected void BtCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
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
                WebUtils.MessageBox2005(this, exception.Message.ToString());
                return;
            }
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod);
            AfiliadoFavorito afiliadoFavorito = this.CtaAcreditar.getAfiliadoFavorito();
            gTransferenciasPago.CtaDebitar = this.CtaDebitar.getCuenta().SNroCuenta;
            gTransferenciasPago.Monto = decimal.Parse(this.txtMonto.Text);
            gTransferenciasPago.SCodBco = this.TxtCodeBanco.Text;
            gTransferenciasPago.PaginaSiguiente = "~/pages/consolidada.aspx";
            gTransferenciasPago.MensajeSatisfactorio = "Transacción realizada exitosamente ";
            gTransferenciasPago.Afiliado = base.Afiliado;
            tipoTransaccionGenerica.Nota = IBBAVConfiguration.ConfirmarTransaccion;
            gTransferenciasPago.Beneficiario = afiliadoFavorito.Beneficiario;
            gTransferenciasPago.CedulaBeneficiario = afiliadoFavorito.CedulaRif;
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Debitar:", "La Transferencia de la cuenta:", Formatos.formatoCuenta(gTransferenciasPago.CtaDebitar));
            if (base.sCod == 6)
            {
                gTransferenciasPago.CtaAcreditar = afiliadoFavorito.NumeroInstrumento;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular;
                tipoTransaccionGenerica.Titulo = "Confirmación de Transferencias Mismo Titular a otros bancos";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Transferencias/OtrosBancos.aspx?sCod=", base.sCod, "&type=0");
            }
            else
            {
                gTransferenciasPago.CtaAcreditar = afiliadoFavorito.NumeroInstrumento;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.TransferenciaOtrosBancosTerceros;
                gTransferenciasPago.Email = this.txtCorreo.Text;
                tipoTransaccionGenerica.Titulo = "Confirmación de Transferencias a Terceros a otros bancos";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Transferencias/OtrosBancos.aspx?sCod=", base.sCod, "&type=1");
            }
            gTransferenciasPago.NumBanco = HelperGlobal.BankNameGet(afiliadoFavorito.BankId);
            gTransferenciasPago.Concepto = this.txtConcepto.Text.Trim();
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Acreditar:", "A la cuenta:", Formatos.formatoCuenta(gTransferenciasPago.CtaAcreditar));
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Banco:", "En el banco:", this.txtBanco.Text);
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Beneficiario:", "Cuyo titular es:", gTransferenciasPago.Beneficiario);
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cédula/RIF:", "Con Cédula/RIF:", gTransferenciasPago.CedulaBeneficiario);
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Monto a Transferir:", "Por un Monto de BsF.:", Formatos.formatoMonto(gTransferenciasPago.Monto));
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Concepto:", "Bajo el Concepto de:", gTransferenciasPago.Concepto);
            tipoTransaccionGenerica.ObjetoTransaccion = gTransferenciasPago;
            tipoTransaccionGenerica.IOpRep = HelperGlobal.GetNumeroOperDiarias(base.Afiliado.nAF_Id, gTransferenciasPago.CtaDebitar, gTransferenciasPago.CtaAcreditar, this.txtMonto.Text);
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
        }

        protected void CtaAcreditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBanco.Text = string.Empty;
            this.txtBeneficiario.Text = string.Empty;
            this.txtCedulaBeneficiario.Text = string.Empty;
            this.txtCorreo.Text = string.Empty;
            this.txtCuentaAcreditar.Text = string.Empty;
            if (this.CtaAcreditar.SelectedValue != "0")
            {
                AfiliadoFavorito afiliadoFavorito = this.CtaAcreditar.getAfiliadoFavorito();
                if (HelperGlobal.BancosGet(false).Find((Banco b) => b.BANK_ID.Trim().Equals(afiliadoFavorito.BankId.Trim())) == null)
                {
                    WebUtils.MessageBox2005(this, "La Institución financiera no se encuentra activa");
                }
                else
                {
                    this.txtCuentaAcreditar.Text = afiliadoFavorito.NumeroInstrumento;
                    this.TxtCodeBanco.Text = afiliadoFavorito.BankId;
                    this.txtBanco.Text = HelperGlobal.BankNameGet(this.TxtCodeBanco.Text);
                    this.txtBeneficiario.Text = afiliadoFavorito.Beneficiario;
                    this.txtCedulaBeneficiario.Text = afiliadoFavorito.CedulaRif;
                    this.txtCorreo.Text = afiliadoFavorito.Email.Trim();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RangoIP rangoIP = new RangoIP();
            ValidacionesViaje validacion = new ValidacionesViaje();

            if ((base.sCod != 6) && (!rangoIP.ValidaConexion(base.Afiliado.sIP)))
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
            this.liLimites.Text = string.Concat("Limite diario en operaciones desde BAV en Línea mínimo de Bs. ", Formatos.formatoMonto(base.Min), Environment.NewLine, " hasta un máximo de Bs. ", Formatos.formatoMonto(base.Max));

            // Modificado 21/07/2018 por Liliana Guerra, para validar operación mismo titular (sCod == 6) y mostrar mensaje de comision en %
            if (base.sCod == 6)
            {
                this.liComision.Text = string.Concat("Nota: Esta transacción generará una comisión del ", Formatos.formatoMonto3Decimales(base.MontoComision), "% sobre el monto transferido");
            }
            else
            {
                this.liComision.Text = string.Concat("Nota: Esta transacción generará una comisión de Bs. ", Formatos.formatoMonto(base.MontoComision));
            }

            if (!base.IsPostBack)
            {
                try
                {
                    this.CtaDebitar.HasTextoInicial = true;
                    this.CtaDebitar.TextoInicial = "Seleccione";
                    this.CtaDebitar.TipoCombo = TipoCombo.CuentasCliente;
                    this.CtaDebitar.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.CuentasDebito;
                    this.CtaDebitar.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaConDisponibleDescripcion;
                    this.CtaAcreditar.HasTextoInicial = true;
                    this.CtaAcreditar.TextoInicial = "Seleccione";
                    this.CtaAcreditar.TipoCombo = TipoCombo.CuentasFavoritos;
                    if (base.sCod == 6)
                    {
                        this.trCorreo.Visible = false;
                        this.CtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular;
                    }
                    else
                    {
                        this.CtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.TransferenciaOtrosBancosTerceros;
                    }
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        TipoTransaccionGenerica item = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
                        GTransferenciasPagos objetoTransaccion = (GTransferenciasPagos)item.ObjetoTransaccion;
                        this.CtaDebitar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaDebitar);
                        this.CtaAcreditar.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.CtaAcreditar);
                        this.txtMonto.Text = Formatos.formatoMonto(objetoTransaccion.Monto).Replace(".", "");
                        this.txtCorreo.Text = objetoTransaccion.Email;
                        this.txtConcepto.Text = objetoTransaccion.Concepto;
                    }
                    this.CtaDebitar.BindCombo();
                    this.CtaAcreditar.BindCombo();
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        this.CtaAcreditar_SelectedIndexChanged(null, null);
                    }
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBox2005(this, bException.IBMessage);
                    return;
                }
                if ((this.CtaAcreditar.TipoCombo != TipoCombo.CuentasFavoritos ? false : this.CtaAcreditar.Items.Count == 1))
                {
                    WebUtils.MessageBootstrap(this, string.Concat("No existen Favoritos para este tipo de transacción. Para crearlos, ingresa a la opción del menú \"Administración y Seguridad / Registro de Favoritos\" o presione <a href=\"", base.ResolveUrl("~/pages/IB/Favoritos/MenuFavoritos.aspx?sCod=22"), "\">aquí</a>"), null);
                }
            }
        }

        private void Validar()
        {
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod);
            if (this.CtaDebitar.SelectedValue == "0")
            {
                throw new Exception("Debe indicar la cuenta a Debitar");
            }
            if (this.CtaAcreditar.SelectedValue == "0")
            {
                throw new Exception("Debe indicar la cuenta a Acreditar");
            }
            if (this.CtaAcreditar.SelectedValue.Equals(this.CtaDebitar.SelectedValue))
            {
                throw new Exception("La cuenta a debitar y la cuenta a acreditar son iguales");
            }
            decimal montoComision = base.MontoComision;
            decimal num = decimal.Parse(this.CtaDebitar.getCuenta().SDisponible.Replace('.', ','));
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
            if (HelperTransaccion.AcumuladorTransVerifyUpdate(true, base.Afiliado.nAF_Id, decimal.Parse(str), base.sCod, (base.sCod != 6 ? "TBOTE" : "TBOMT")))
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