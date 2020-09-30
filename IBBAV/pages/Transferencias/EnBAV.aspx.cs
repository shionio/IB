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

namespace IBBAV.pages.Transferencias
{
    public partial class EnBAV : Principal
    {
        public EnBAV()
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
                WebUtils.MessageBox2005(this, exception.Message.ToString());
                return;
            }
            GTransferenciasPagos gTransferenciasPago = new GTransferenciasPagos(base.Afiliado, base.sCod)
            {
                CtaDebitar = this.CtaDebitar.getCuenta().SNroCuenta,
                Monto = decimal.Parse(this.txtMonto.Text),
                PaginaSiguiente = "~/pages/consolidada.aspx",
                MensajeSatisfactorio = "Transacción realizada exitosamente "
            };
            tipoTransaccionGenerica.Nota = IBBAVConfiguration.ConfirmarTransaccion;
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Debitar:", "La Transferencia de la cuenta:", Formatos.formatoCuenta(gTransferenciasPago.CtaDebitar));
            if (base.sCod == 5)
            {
                gTransferenciasPago.CtaAcreditar = this.CtaAcreditar.getCuenta().SNroCuenta;
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Acreditar:", "A la cuenta:", Formatos.formatoCuenta(gTransferenciasPago.CtaAcreditar));
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.TransferenciaMismoTitularBAV;
                tipoTransaccionGenerica.Titulo = "Confirmación de Transferencias Mismo Titular en BAV";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Transferencias/EnBAV.aspx?sCod=", base.sCod, "&type=0");
            }
            else
            {
                gTransferenciasPago.CtaAcreditar = this.CtaAcreditar.getAfiliadoFavorito().NumeroInstrumento;
                gTransferenciasPago.TipoTransaccion = EnumTipoFavorito.TransferenciaTercerosBAV;
                gTransferenciasPago.CedulaBeneficiario = this.CtaAcreditar.getAfiliadoFavorito().CedulaRif;
                gTransferenciasPago.Beneficiario = this.CtaAcreditar.getAfiliadoFavorito().Beneficiario;
                gTransferenciasPago.Concepto = this.txtConcepto.Text;
                gTransferenciasPago.Email = this.txtCorreo.Text;
                tipoTransaccionGenerica.Titulo = "Confirmación de Transferencias a Terceros  en BAV";
                gTransferenciasPago.PaginaAnterior = string.Concat("~/pages/Transferencias/EnBAV.aspx?sCod=", base.sCod, "&type=1");
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Cuenta a Acreditar:", "A la cuenta;", Formatos.formatoCuenta(gTransferenciasPago.CtaAcreditar));
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Beneficiario:", "Cuyo titular es:", gTransferenciasPago.Beneficiario);
            }
            tipoTransaccionGenerica.IOpRep = HelperGlobal.GetNumeroOperDiarias(base.Afiliado.nAF_Id, gTransferenciasPago.CtaDebitar, gTransferenciasPago.CtaAcreditar, this.txtMonto.Text);
            tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Monto a Transferir:", "Por un Monto de BsF.:", Formatos.formatoMonto(gTransferenciasPago.Monto));
            if (base.sCod != 5)
            {
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Concepto:", "Bajo el Concepto de:", gTransferenciasPago.Concepto);
            }
            tipoTransaccionGenerica.ObjetoTransaccion = gTransferenciasPago;
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
        }

        protected void CtaAcreditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CtaAcreditar.SelectedIndex <= 0)
            {
                this.txtCuentaAcreditar.Text = string.Empty;
                this.txtCorreo.Text = string.Empty;
            }
            else if (base.sCod == 7)
            {
                AfiliadoFavorito afiliadoFavorito = this.CtaAcreditar.getAfiliadoFavorito();
                this.txtCuentaAcreditar.Text = afiliadoFavorito.NumeroInstrumento;
                this.txtCorreo.Text = afiliadoFavorito.Email;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RangoIP rangoIP = new RangoIP();
            ValidacionesViaje validacion = new ValidacionesViaje();           

            if ((base.sCod != 5) && (!rangoIP.ValidaConexion(base.Afiliado.sIP)))
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

                if (validacion.AfiliadoRestricc(base.Afiliado.nAF_Id,base.Afiliado.sIP,ipDec,tipoInstrumento,numInstrumento))
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
            this.lblLimites.Text = string.Concat("Mínimo Bs. ", Formatos.formatoMonto(base.Min), " - Máximo Bs. ", Formatos.formatoMonto(base.Max));
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
                    if (base.sCod == 5)
                    {
                        this.trCuentaAdreditar.Visible = false;
                        this.trConcepto.Visible = false;
                        this.trCorreo.Visible = false;
                        this.CtaAcreditar.TipoCombo = TipoCombo.CuentasCliente;
                        this.CtaAcreditar.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.CuentasCredito;
                        this.CtaAcreditar.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaConDisponibleDescripcion;
                        this.lblDescription.Text = "Transfiere dinero entre tus cuentas de forma automática, sin hacer colas ni llamadas telefónicas.";
                    }
                    else
                    {
                        this.liCuentaAcreditar.Text = "Registrados:";
                        this.CtaAcreditar.TipoCombo = TipoCombo.CuentasFavoritos;
                        this.CtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.TransferenciaTercerosBAV;
                        this.lblDescription.Text = "Transfiere dinero a otras cuentas en BAV de forma automática, sin hacer colas ni llamadas telefónicas.";
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
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
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
            if (this.CtaDebitar.SelectedIndex == 0)
            {
                throw new Exception("Debe indicar la cuenta a Debitar");
            }
            if (this.CtaAcreditar.SelectedIndex == 0)
            {
                throw new Exception("Debe indicar la cuenta a Acreditar");
            }
            if (this.CtaAcreditar.SelectedValue.Equals(this.CtaDebitar.SelectedValue))
            {
                throw new Exception("La cuenta a debitar y la cuenta a acreditar son iguales");
            }
            decimal num = decimal.Parse(this.CtaDebitar.getCuenta().SDisponible.Replace('.', ','));
            gTransferenciasPago.Monto = decimal.Parse(this.txtMonto.Text);
            if (gTransferenciasPago.Monto > num)
            {
                throw new Exception("El monto a transferir es mayor al disponible de la cuenta seleccionada, por favor, verifique");
            }
            string str = this.txtMonto.Text.Trim();
            string str1 = "";
            if (str.Length == 0)
            {
                throw new Exception("Debe indicar el monto de la transferencia");
            }
            if (!eFunctions.validarMonto(ref str, ref str1, base.Min, base.MtoLimiteTrans))
            {
                throw new Exception(str1);
            }
            string empty = string.Empty;
            if (HelperTransaccion.AcumuladorTransVerifyUpdate(true, base.Afiliado.nAF_Id, decimal.Parse(str), base.sCod, (base.sCod != 5 ? "TBTER" : "TBAMT")))
            {
                throw new Exception("El Monto supera al máximo diario");
            }
            if (this.txtConcepto.Text.Length > 80)
            {
                throw new Exception("La longitud del Concepto no puede exceder los 80 caracteres.");
            }
            if ((this.txtCorreo.Text.Length <= 0 ? false : !Tools.TestEmailRegex(this.txtCorreo.Text.Trim())))
            {
                throw new Exception("El correo electrónico no es válido");
            }
            this.txtMonto.Text = str;
        }
    }
}