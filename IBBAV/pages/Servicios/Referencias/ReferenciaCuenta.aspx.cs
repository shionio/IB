using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.IBS;
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

namespace IBBAV.pages.Servicios.Referencias
{
    public partial class Cuenta : Principal
    {
        protected Literal liComision; //OJO

        public Cuenta()
        {
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            TipoTransaccionGenerica tipoTransaccionGenerica = new TipoTransaccionGenerica();
            try
            {
                this.Validar();
                GReferenciaBancaria gReferenciaBancarium = new GReferenciaBancaria(base.Afiliado, base.sCod)
                {
                    IsDefaultDirigido = this.ddlDirigido.SelectedIndex == 0
                };
                string str = (gReferenciaBancarium.IsDefaultDirigido ? this.ddlDirigido.SelectedItem.Text : this.txtNombreInstitucion.Text);
                gReferenciaBancarium.Afiliado = base.Afiliado;
                CuentaIBS cuenta = this.ddlCuenta.getCuenta();
                gReferenciaBancarium.Titular = base.Afiliado.sCO_Nombres.ToUpper();
                gReferenciaBancarium.NroCuenta = cuenta.SNroCuenta;
                gReferenciaBancarium.TipoCuenta = cuenta.SDescipcionproducto;
                gReferenciaBancarium.Dirigido = str;
                gReferenciaBancarium.MensajeSatisfactorio = "Presiona Imprimir si deseas realizar esta operación.";
                gReferenciaBancarium.PaginaAnterior = base.Request.RawUrl;
                gReferenciaBancarium.PaginaSiguiente = "~/pages/Consolidada.aspx";
                tipoTransaccionGenerica.ObjetoTransaccion = gReferenciaBancarium;
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Tipo de Cuenta:", "La Referencia Bancaria identificada con el Número:", gReferenciaBancarium.TipoCuenta);
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Número de Cuenta:", "De la Cuenta Número:", Formatos.formatoCuenta(gReferenciaBancarium.NroCuenta));
                tipoTransaccionGenerica.EncabezadoTransaccion.AddEncabezado("Dirigida a:", str);
                tipoTransaccionGenerica.Titulo = "Confirmación de Solicitud de Referencia Bancaria";
                tipoTransaccionGenerica.Nota = IBBAVConfiguration.ConfirmarTransaccion;
                tipoTransaccionGenerica.Nota2 = "Por motivos de seguridad tu referencia no se visualizará en pantalla, pero se imprimirá directamente cuando hagas click en el botón Imprimir.  Por favor verifica que tu impresora esté encendida y conectada.";
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message);
                return;
            }
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
        }

        protected void btnAyuda_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx?sCod=1");
        }

        protected void ddlDirigido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlDirigido.SelectedIndex <= 0)
            {
                this.trNombreInstitucion.Visible = false;
                this.txtNombreInstitucion.Text = string.Empty;
            }
            else
            {
                this.trNombreInstitucion.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.MontoComision > new decimal(0))
            {
                this.liComision.Text = string.Concat("Nota: Esta transacción generará una comisión de Bs.F. ", Formatos.formatoMonto(base.MontoComision));
            }
            if (!base.IsPostBack)
            {
                try
                {
                    this.ddlCuenta.HasTextoInicial = true;
                    this.ddlCuenta.TextoInicial = "Seleccione una Cuenta";
                    this.ddlCuenta.TipoCombo = TipoCombo.CuentasCliente;
                    this.ddlCuenta.TipoCuentaConsulta = TipoCuentaConsulta.CuentaAhorroCorriente;
                    this.ddlCuenta.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.Todas;
                    this.ddlCuenta.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaDescripcion;
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        TipoTransaccionGenerica item = (TipoTransaccionGenerica)this.Context.Items["TipoTransaccionGenerica"];
                        GReferenciaBancaria objetoTransaccion = (GReferenciaBancaria)item.ObjetoTransaccion;
                        this.ddlCuenta.SetValue = CryptoUtils.EncryptMD5(objetoTransaccion.NroCuenta);
                        if (!objetoTransaccion.Dirigido.Equals("A quien pueda interesar"))
                        {
                            this.ddlDirigido.ClearSelection();
                            this.ddlDirigido.Items[1].Selected = true;
                            this.txtNombreInstitucion.Text = objetoTransaccion.Dirigido;
                            this.trNombreInstitucion.Visible = true;
                        }
                    }
                    this.ddlCuenta.BindCombo();
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                }
            }
        }

        private void Validar()
        {
            if (this.ddlCuenta.SelectedIndex == 0)
            {
                throw new Exception("Debe seleccionar una cuenta");
            }
            if ((this.ddlDirigido.SelectedIndex != 1 ? false : this.txtNombreInstitucion.Text.Trim().Equals(string.Empty)))
            {
                throw new Exception("Debes colocar el nombre a quien va dirigida la referencia.");
            }
        }
    }
}