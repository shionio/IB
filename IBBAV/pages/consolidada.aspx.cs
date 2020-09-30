using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.Extraefectivo;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsExtraefectivo;
using IBBAV.WsIbsService;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using static IBBAV.Entidades.Extraefectivo.LineaExtracredito;

namespace IBBAV.pages
{
    public partial class consolidada : Principal
    {
        public LineaExtracredito solicitud = new LineaExtracredito();
        public consolidada()
        {
            base.sCod = 0;
            base.NombrePantalla = "Posicion Consolidada";
        }

        private void bindGrid()
        {
            long aFCodCliente = base.Afiliado.AF_CodCliente;
            RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(aFCodCliente.ToString(), base.Afiliado.sAF_Rif, TipoConsultaCuentasIBS.Todas);
            List<IbaConsDet> ibaConsDets = new List<IbaConsDet>();
            ibaConsDets.AddRange(respuestaIbaCon.sdjvCuentas.sdsjvDetalle);
            List<IbaConsDet> ibaConsDets1 = ibaConsDets.FindAll((IbaConsDet x) => x.SClasecuenta.Equals("01"));
            //List<IbaConsDet> ibaConsDets2 = ibaConsDets.FindAll((IbaConsDet x) => x.SClasecuenta.Equals("02"));
            // Agregado 02/05/2019 por Liliana Guerra para ocultar las cuentas JURIDICA en la sesión natural {
            List<IbaConsDet> ibaConsDets2 = ibaConsDets.FindAll((IbaConsDet x) => x.SClasecuenta.Equals("02") && !x.STipoFirma.Contains("N"));
            this.dtgCuentas.DataSource = ibaConsDets2.ToArray();            
            this.dtgCuentas.DataBind();
            this.pnlCuentas.Visible = ibaConsDets2.Count > 0;
            List<IbaConsDet> ibaConsDets3 = ibaConsDets1.FindAll((IbaConsDet x) => x.STipocuenta.Equals("TDC"));
            this.rptTarjetas.DataSource = ibaConsDets3.ToArray();
            this.rptTarjetas.DataBind();
            this.pnlTarjetas.Visible = ibaConsDets3.Count > 0;
            List<IbaConsDet> ibaConsDets4 = ibaConsDets1.FindAll((IbaConsDet x) => (!x.STipocuenta.Equals("CDS") ? x.STipocuenta.Equals("TDS") : true));
            this.rptDepositos.DataSource = ibaConsDets4.ToArray();
            this.rptDepositos.DataBind();
            this.pnlDepositos.Visible = ibaConsDets4.Count > 0;
            List<IbaConsDet> ibaConsDets5 = ibaConsDets1.FindAll((IbaConsDet x) => (!x.STipocuenta.Equals("LNS") ? x.STipocuenta.Equals("PLP") : true));
            this.rptCreditos.DataSource = ibaConsDets5.ToArray();
            this.rptCreditos.DataBind();
            this.pnlCreditos.Visible = ibaConsDets5.Count > 0;           
        }

        protected void dtgCuentas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                IbaConsDet dataItem = e.Item.DataItem as IbaConsDet;
                UnidadCuenta uc = new UnidadCuenta();

                HyperLink hyperLink = e.Item.FindControl("lnkCuenta") as HyperLink;
                hyperLink.Text = Formatos.formatoCuenta(dataItem.SNroCuenta);
                Literal literal = e.Item.FindControl("liContable") as Literal;
                Literal literal1 = e.Item.FindControl("liDisponible") as Literal;
                hyperLink.NavigateUrl = base.ResolveUrl(string.Concat("~/pages/Consultas/Cuentas/Movimientos.aspx?sCod=&sCta=", CryptoUtils.EncryptMD5(dataItem.SNroCuenta)));
                literal.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SContable));
                literal1.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SDisponible));
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                Literal literalP = e.Item.FindControl("liPetro") as Literal;                
                literalP.Text = Formatos.formatoSaldoPetro((decimal.Parse(uc.SPetro(dataItem.SDisponible))));
            }
        }
        //Entidades.Extraefectivo.TarjetaConsulta tarjeta = new Entidades.Extraefectivo.TarjetaConsulta();
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.solicitud = HelperExtracredito.consultaCuotas("V015710717", "01660101211011098312", "5182150215010671", "5000,00");

            //Session["codigoResp"] = solicitud.respuestaCod;
            //Session["respuesta"] = solicitud.respuestaDesc;
            //Session["cuota6"] = solicitud.cuota6;
            //Session["cuota12"] = solicitud.cuota12;
            //Session["cuota24"] = solicitud.cuota24;
            //Session["cuota36"] = solicitud.cuota36;

            //WebUtils.MessageBox(this, Session["codigoResp"]+ " "+ Session["respuesta"]);

            if (!base.IsPostBack)
            {
                try
                {
                    this.bindGrid();
                    Literal literal = this.liFechaCuenta;
                    Literal literal1 = this.liFechaTarjetas;
                    Literal literal2 = this.liFechaDepositos;
                    Literal literal3 = this.liFechaCreditos;
                    string str = DateTime.Now.ToString("dd/MM/yyyy");
                    string str1 = str;
                    literal3.Text = str;
                    string str2 = str1;
                    string str3 = str2;
                    literal2.Text = str2;
                    string str4 = str3;
                    string str5 = str4;
                    literal1.Text = str4;
                    literal.Text = str5;
                }
                catch (IBException bException)
                {
                    ((BAVMaster)base.Master).ShowMensaje(bException.IBMessage, TipoError.Alerta);
                }
                catch (Exception exception)
                {
                    ((BAVMaster)base.Master).ShowMensaje(exception.Message, TipoError.Alerta);
                }
            }
        }

        protected void rptCreditos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                IbaConsDet dataItem = e.Item.DataItem as IbaConsDet;
                UnidadCuenta uc = new UnidadCuenta();

                HyperLink hyperLink = e.Item.FindControl("lnkCuenta") as HyperLink;
                hyperLink.Text = Formatos.formatoCuenta(dataItem.SNroCuenta);
                Literal literal = e.Item.FindControl("liContable") as Literal;
                Literal literal1 = e.Item.FindControl("liDisponible") as Literal;
                literal.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SContable));
                literal1.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SDisponible));
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                Literal literalP = e.Item.FindControl("liPetro") as Literal;
                literalP.Text = Formatos.formatoSaldoPetro((decimal.Parse(uc.SPetro(dataItem.SDisponible))));
            }
        }

        protected void rptDepositos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                IbaConsDet dataItem = e.Item.DataItem as IbaConsDet;
                UnidadCuenta uc = new UnidadCuenta();

                HyperLink hyperLink = e.Item.FindControl("lnkCuenta") as HyperLink;
                hyperLink.Text = Formatos.formatoCuenta(dataItem.SNroCuenta);
                Literal literal = e.Item.FindControl("liContable") as Literal;
                Literal literal1 = e.Item.FindControl("liDisponible") as Literal;
                literal.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SContable));
                literal1.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SDisponible));
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                Literal literalP = e.Item.FindControl("liPetro") as Literal;
                literalP.Text = Formatos.formatoSaldoPetro((decimal.Parse(uc.SPetro(dataItem.SDisponible))));
            }
        }

        protected void rptTarjetas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                IbaConsDet dataItem = e.Item.DataItem as IbaConsDet;
                UnidadCuenta uc = new UnidadCuenta();

                HyperLink hyperLink = e.Item.FindControl("lnkCuenta") as HyperLink;
                hyperLink.Text = Formatos.formatoCuenta(dataItem.SNroCuenta);
                Literal literal = e.Item.FindControl("liContable") as Literal;
                Literal literal1 = e.Item.FindControl("liDisponible") as Literal;
                literal.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SContable));
                literal1.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(dataItem.SDisponible));
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                Literal literalP = e.Item.FindControl("liPetro") as Literal;
                literalP.Text = Formatos.formatoSaldoPetro((decimal.Parse(uc.SPetro(dataItem.SDisponible))));
            }
        }
    }
}