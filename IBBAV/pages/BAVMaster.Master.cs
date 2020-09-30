using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages
{
    public partial class BAVMaster : MasterPage
    {
        public string Fecha
        {
            get
            {
                return this.liFecha.Text;
            }
            set
            {
                this.liFecha.Text = value;
            }
        }

        public string TituloPage
        {
            get
            {
                return this.liTituloPagina.Text;
            }
            set
            {
                this.liTituloPagina.Text = value;
            }
        }

        public string UltimoAcceso
        {
            get
            {
                return this.liFechaHoraUltSes.Text;
            }
            set
            {
                this.liFechaHoraUltSes.Text = value;
            }
        }

        public string Usuario
        {
            get
            {
                return this.liNobmreUsuario.Text;
            }
            set
            {
                this.liNobmreUsuario.Text = value;
            }
        }

        public BAVMaster()
        {
        }

        public void HideMensaje()
        {
            this.liMensaje.Text = string.Empty;
            this.pnlMensaje.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.Title.Contains("Untitled"))
            {
                this.Page.Title = "BAV - Banco Agricola de Venezuela";
            }
            else
            {
                this.Page.Title = string.Concat("BAV - Banco Agricola de Venezuela - ", this.Page.Title);
            }
            Principal parent = this.Parent as Principal;
            Afiliado afiliado = parent.Afiliado;
            if (afiliado != null)
            {
                this.Usuario = string.Concat(afiliado.sCO_Nombres.ToUpper(), " ", afiliado.sCO_Apellidos);
                if (this.TituloPage == string.Empty)
                {
                    this.TituloPage = parent.NombrePantalla;
                }
                this.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                DateTime tiempoInicio = HelperSession.SA_GetLastSession(afiliado.nAF_Id).TiempoInicio;
                this.UltimoAcceso = tiempoInicio.ToString("dd/MM/yyyy hh:mm");
            }
        }

        public void ShowMensaje(string mensaje, TipoError tipoError)
        {
            this.liMensaje.Text = mensaje;
            this.lblMensajeIcon.Attributes.Add("aria-hidden", "true");
            switch (tipoError)
            {
                case TipoError.Alerta:
                    {
                        this.lblMensajeIcon.CssClass = "glyphicon glyphicon-exclamation-sign";
                        this.pnlMensaje.CssClass = "alert alert-danger";
                        break;
                    }
                case TipoError.Informacion:
                    {
                        this.lblMensajeIcon.CssClass = "glyphicon glyphicon-info-sign";
                        this.pnlMensaje.CssClass = "alert alert-info";
                        break;
                    }
                case TipoError.Satisfactorio:
                    {
                        this.lblMensajeIcon.CssClass = "glyphicon glyphicon-ok-circle";
                        this.pnlMensaje.CssClass = "alert alert-succes";
                        break;
                    }
                case TipoError.Aviso:
                    {
                        this.lblMensajeIcon.CssClass = "glyphicon glyphicon-exclamation-sign";
                        this.pnlMensaje.CssClass = "alert alert-warning";
                        break;
                    }
            }
            this.pnlMensaje.Visible = true;
        }
    }
}