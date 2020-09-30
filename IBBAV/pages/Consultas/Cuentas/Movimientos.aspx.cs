using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.IBS;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Consultas.Cuentas
{
    public partial class Movimientos : Principal
    {
        private const int cantMov = 20;

        private DateTime fechad;

        private DateTime fechah;

        private ConsultaMovimientoTipoConsulta tipoConsulta;        

        private bool loaded
        {
            get
            {
                bool flag;
                object item = this.ViewState["loaded"];
                flag = (this.ViewState["loaded"] != null ? (bool)item : false);
                return flag;
            }
            set
            {
                this.ViewState["loaded"] = value;
            }
        }

        private RespuestaStmrdsjv respuesta
        {
            get
            {
                return this.ViewState["data"] as RespuestaStmrdsjv;
            }
            set
            {
                this.ViewState["data"] = value;
            }
        }

        private int TotalPaginas
        {
            get
            {
                return (int)this.ViewState["TotalPaginas"];
            }
            set
            {
                this.ViewState["TotalPaginas"] = value;
            }
        }

        public Movimientos()
        {
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                this.llenarGrid();
            }
            catch (Exception exception)
            {
                WebUtils.MessageBootstrap(this, exception.Message, null);
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SChqRef", typeof(string));
            dataTable.Columns.Add("SDesctrans", typeof(string));
            dataTable.Columns.Add("SFechaEfect", typeof(string));
            dataTable.Columns.Add("SFechaProc", typeof(string));
            dataTable.Columns.Add("SIndDebCre", typeof(string));
            dataTable.Columns.Add("SMonto", typeof(string));
            RespuestaStmrdsjv respuestaStmrdsjv = this.respuesta;
            if (respuestaStmrdsjv != null)
            {
                StmrdsjvDet[] stmrdsjvDetArray = respuestaStmrdsjv.stmjvCuentas.smtDetalle;
                for (int i = 0; i < (int)stmrdsjvDetArray.Length; i++)
                {
                    StmrdsjvDet stmrdsjvDet = stmrdsjvDetArray[i];
                    if (stmrdsjvDet != null)
                    {
                        DataRow sChqRef = dataTable.NewRow();
                        sChqRef["SChqRef"] = stmrdsjvDet.SChqRef;
                        sChqRef["SDesctrans"] = stmrdsjvDet.SDesctrans;
                        DateTime fecha = Formatos.ISOToFecha(stmrdsjvDet.SFechaEfect);
                        sChqRef["SFechaEfect"] = fecha.ToString("dd/MM/yyyy");
                        DateTime dateTime = Formatos.ISOToFecha(stmrdsjvDet.SFechaProc);
                        sChqRef["SFechaProc"] = dateTime.ToString("dd/MM/yyyy");
                        sChqRef["SIndDebCre"] = stmrdsjvDet.SIndDebCre;
                        decimal num = Formatos.ISOToDecimal(stmrdsjvDet.SMonto);
                        if (stmrdsjvDet.SIndDebCre.Trim().Contains("0"))
                        {
                            num *= new decimal(-1);
                        }
                        sChqRef["SMonto"] = num;
                        dataTable.Rows.Add(sChqRef);
                    }
                }
                List<ExportUtils.ExportUtilDatos> exportUtilDatos = new List<ExportUtils.ExportUtilDatos>()
                {
                    new ExportUtils.ExportUtilDatos("Fecha Transacci&oacute;n", "SFechaProc", ExportUtils.Align.center, "", "#c12f2f", "#ffffff", false),
                    new ExportUtils.ExportUtilDatos("Fecha Efectiva", "SFechaEfect", ExportUtils.Align.center, "", "#c12f2f", "#ffffff", false),
                    new ExportUtils.ExportUtilDatos("Referencia", "SChqRef", ExportUtils.Align.center, "", "#c12f2f", "#ffffff", false),
                    new ExportUtils.ExportUtilDatos("Descripci&oacute;n", "SDesctrans", ExportUtils.Align.left, "", "#c12f2f", "#ffffff", false),
                    new ExportUtils.ExportUtilDatos("Monto", "SMonto", ExportUtils.Align.right, "", "#c12f2f", "#ffffff", false)
                };
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<table>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td><b>Fecha:</b></td>");
                DateTime now = DateTime.Now;
                stringBuilder.Append(string.Concat("<td style='text-align:center;'>", now.ToString("dd/MM/yyyy"), "</td>"));
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td><b>Criterio seleccionado:</b></td>");
                stringBuilder.Append(string.Concat("<td style='text-align:center;'>", this.liCritero.Text, "</td></tr>"));
                stringBuilder.Append("</table>");
                string[] afiliado = new string[] { "Movimientos_", base.Afiliado.sCedula, "_", null, null };
                afiliado[3] = DateTime.Now.ToString("ddMMyyyyhhmmss");
                afiliado[4] = ".xls";
                ExportUtils.Export2Excel(string.Concat(afiliado), stringBuilder.ToString(), dataTable, exportUtilDatos);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlCuenta.SelectedIndex > 0)
            {
                CuentaIBS cuenta = this.ddlCuenta.getCuenta();
                UnidadCuenta uc = new UnidadCuenta();
                
                this.lblSaldoDisponible.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(cuenta.SDisponible));
                this.lblSaldoTotal.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(cuenta.SContable));
                this.lblSaldoDif.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(cuenta.SDiferido));
                this.lblSaldoBloq.Text = Formatos.formatoMonto(Formatos.ISOToDecimal(cuenta.SBloqueado));
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                this.lblSaldoPetro.Text = Formatos.formatoSaldoPetro(decimal.Parse(uc.SPetro(cuenta.SDisponible)));
            }
            else
            {
                this.lblSaldoDisponible.Text = "0,00";
                this.lblSaldoTotal.Text = "0,00";
            }
            try
            {
                this.llenarGrid();
            }
            catch (Exception exception)
            {
                WebUtils.MessageBootstrap(this, exception.Message, null);
            }
        }

        private void llenarGrid()
        {
            if (this.rdoDia.Checked)
            {
                this.tipoConsulta = ConsultaMovimientoTipoConsulta.Dia;
                this.liCritero.Text = "Del Día";
            }
            if (this.rdoDiaAnt.Checked)
            {
                this.tipoConsulta = ConsultaMovimientoTipoConsulta.DiaAnterior;
                this.liCritero.Text = "Del Día Anterior";
            }
            if (this.rdoMes.Checked)
            {
                this.tipoConsulta = ConsultaMovimientoTipoConsulta.Mes;
                this.liCritero.Text = "Del Mes";
            }
            if (this.rdoMesAnt.Checked)
            {
                this.tipoConsulta = ConsultaMovimientoTipoConsulta.MesAnterior;
                this.liCritero.Text = "Del Mes Anterior";
            }
            if (this.rdoUltimosMov.Checked)
            {
                this.tipoConsulta = ConsultaMovimientoTipoConsulta.UltMovimientos;
                this.liCritero.Text = "Últimos movimientos";
            }
            if (this.rdoRango.Checked)
            {
                this.tipoConsulta = ConsultaMovimientoTipoConsulta.RangoFecha;
                this.liCritero.Text = string.Concat("Rango de fechas del ", this.FD.Text, " al ", this.FH.Text);
                this.fechad = DateTime.Parse(this.FD.Text);
                this.fechah = DateTime.Parse(this.FH.Text);
                if (this.fechad > this.fechah)
                {
                    throw new Exception("La Fecha Desde no puede ser mayor a la Fecha Hasta");
                }
                if (DateAndTime.DateDiff(DateInterval.Day, this.fechad, this.fechah) > (long)60)
                {
                    throw new Exception("El rango de fechas a consultar no puede ser mayor a dos(2) meses");
                }
            }
            this.respuesta = HelperIbs.ibsConsultaMovimientos(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.ddlCuenta.getCuenta().SNroCuenta, this.tipoConsulta, this.fechad, this.fechah, 20);
            List<MovimientosR> movimientosRs = new List<MovimientosR>();
            StmrdsjvDet[] stmrdsjvDetArray = this.respuesta.stmjvCuentas.smtDetalle;
            for (int i = 0; i < (int)stmrdsjvDetArray.Length; i++)
            {
                StmrdsjvDet stmrdsjvDet = stmrdsjvDetArray[i];
                if (stmrdsjvDet != null)
                {
                    MovimientosR movimientosR = new MovimientosR()
                    {
                        SChqRef = stmrdsjvDet.SChqRef,
                        SDesctrans = stmrdsjvDet.SDesctrans,
                        SFechaEfect = Formatos.ISOToFecha(stmrdsjvDet.SFechaEfect),
                        SFechaProc = Formatos.ISOToFecha(stmrdsjvDet.SFechaProc),
                        SIndDebCre = stmrdsjvDet.SIndDebCre
                    };
                    decimal num = Formatos.ISOToDecimal(stmrdsjvDet.SMonto);
                    if (stmrdsjvDet.SIndDebCre.Trim().Contains("0"))
                    {
                        num *= new decimal(-1);
                    }
                    movimientosR.SMonto = num;
                    movimientosRs.Add(movimientosR);
                }
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script>");
            stringBuilder.AppendFormat("myData = {0};loadTable();$('.iconconsultar').hide();", javaScriptSerializer.Serialize(movimientosRs));
            this.loaded = true;
            stringBuilder.Append("</script>");
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "MyData", stringBuilder.ToString(), false);
        }

        protected bool LogMovimientos()
        {
            bool flag = false;
            try
            {
                string text = this.liCritero.Text;
                string sNroCuenta = this.ddlCuenta.getCuenta().SNroCuenta;
                flag = HelperGlobal.LogTransAdd(new DataLog()
                {
                    NAF_Id = base.Afiliado.nAF_Id,
                    SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString(),
                    SCod_Trans = "COMOV",
                    SAF_IP = base.Afiliado.sIP,
                    SBanco = string.Empty,
                    SCuenta_Origen = sNroCuenta,
                    SCuenta_Destino = string.Empty,
                    SMonto = string.Empty,
                    STipo_Tarjeta = string.Empty,
                    SBeneficiario = string.Empty,
                    SCedula_Id_B = string.Empty,
                    SSerial_Chequera = string.Empty,
                    SCheques = string.Empty,
                    STitular = base.Afiliado.sCO_Nombres,
                    ICantidad = 0,
                    SReferencia = string.Empty,
                    SConcepto = string.Concat("Consulta de Movimientos de la cuenta ", sNroCuenta, " con criterio de búsqueda ", text),
                    SMotivo_Suspension = string.Empty,
                    SDir_Envio_Chequera = string.Empty
                });
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.rdoDia.Checked = true;
                try
                {
                    this.ddlCuenta.HasTextoInicial = true;
                    this.ddlCuenta.TextoInicial = "Seleccione una cuenta a consultar";
                    this.ddlCuenta.TipoCombo = TipoCombo.CuentasCliente;
                    this.ddlCuenta.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.Todas;
                    this.ddlCuenta.TipoCuentaConsulta = TipoCuentaConsulta.CuentaAhorroCorriente;
                    this.ddlCuenta.TipoComboCuentaFormato = TipoComboCuentaFormato.CuentaDescripcion;
                    this.ddlCuenta.BindCombo();
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                    return;
                }
                if (base.Request.QueryString["sCta"] != null)
                {
                    this.ddlCuenta.SetValue = base.Request.QueryString["sCta"];
                    this.ddlCuenta.BindCombo();
                    this.rdoDia.Checked = false;
                    this.rdoDiaAnt.Checked = false;
                    this.rdoMes.Checked = false;
                    this.rdoUltimosMov.Checked = true;
                    this.rdoMesAnt.Checked = false;
                    this.rdoRango.Checked = false;
                    this.tipoConsulta = ConsultaMovimientoTipoConsulta.UltMovimientos;
                    this.ddlCuenta_SelectedIndexChanged(null, null);
                }
            }
        }

        protected void rdoCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.ddlCuenta_SelectedIndexChanged(null, null);
        }
    }
}