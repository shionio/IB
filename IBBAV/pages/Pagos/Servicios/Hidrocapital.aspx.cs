using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Pagos.Servicios
{
    public partial class Hidrocapital : Principal
    {
        public Hidrocapital()
        {
        }

        protected void BtAceptar_Click(object sender, EventArgs e)
        {
        }

        protected void BtCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void ddlCtaAcreditar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.trResultadoNombre.Visible = false;
            this.panelRecibos.Visible = false;
            if (this.ddlCtaAcreditar.SelectedIndex > 0)
            {
                HelperHidrocapital.Resultado resultado = HelperHidrocapital.ConsultarRecibos(this.ddlCtaAcreditar.getAfiliadoFavorito().NumeroInstrumento);
                this.liNombreCliente.Text = resultado.NOMBRE;
                this.rptRecibos.DataSource = resultado.AVISOS;
                this.rptRecibos.DataBind();
                HtmlTableRow htmlTableRow = this.trResultadoNombre;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    this.ddlCtaAcreditar.TipoCuentaFavoritos = EnumTipoFavorito.PagoHidrocapital;
                    this.ddlCtaDebitar.BindCombo();
                    this.ddlCtaAcreditar.BindCombo();
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBox2005(this, bException.IBMessage);
                }
            }
        }

        protected void rptRecibos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                object dataItem = e.Item.DataItem;
            }
        }
    }
}