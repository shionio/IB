using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Servicios.Referencias
{
    public partial class ReferenciaImpresion : System.Web.UI.Page
    {
        protected Label litipocta;

        public ReferenciaImpresion()
        {
        }

        protected bool LogRef(Afiliado af)
        {
            bool flag = false;
            try
            {
                flag = HelperGlobal.LogTransAdd(new DataLog()
                {
                    NAF_Id = af.nAF_Id,
                    SAF_NombreUsuario = af.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString(),
                    SCod_Trans = "REFCO",
                    SAF_IP = af.sIP,
                    SBanco = string.Empty,
                    SCuenta_Origen = string.Empty,
                    SCuenta_Destino = string.Empty,
                    SMonto = string.Empty,
                    STipo_Tarjeta = string.Empty,
                    SBeneficiario = string.Empty,
                    SCedula_Id_B = string.Empty,
                    SSerial_Chequera = string.Empty,
                    SCheques = string.Empty,
                    STitular = af.sCO_Nombres,
                    ICantidad = 0,
                    SReferencia = string.Empty,
                    SConcepto = string.Concat("Consulta de Referencia Bancaria de: ", af.sAF_NombreUsuario),
                    SMotivo_Suspension = string.Empty,
                    SDir_Envio_Chequera = string.Empty
                });
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["referencia"] != null)
            {
                GReferenciaBancaria item = (GReferenciaBancaria)this.Session["referencia"];
                this.liTitular.Text = item.Titular;
                if (item.IsDefaultDirigido)
                {
                    this.liDirigido.Text = item.Dirigido.ToUpper();
                }
                else
                {
                    this.liDirigido1.Text = item.Dirigido.ToUpper();
                }
                this.trDirigido.Visible = item.IsDefaultDirigido;
                this.trDirigido1.Visible = !item.IsDefaultDirigido;
                if (item.TipoCuenta.Contains("CTE"))
                {
                    this.litipocta.Text = "Cuenta Corriente";
                }
                else if (item.TipoCuenta.Contains("AHO"))
                {
                    this.litipocta.Text = "Cuenta Ahorro";
                }
                else
                {
                    this.litipocta.Text = item.TipoCuenta;
                }
                this.liFechaInicio.Text = item.FechaInicio;
                Label label = this.liDia;
                DateTime date = DateTime.Now;
                label.Text = date.ToString("dd");
                Label str1 = this.liMes;
                date = DateTime.Now;
                str1.Text = date.ToString("MMMM");
                Label label1 = this.liAnio;
                date = DateTime.Now;
                label1.Text = date.ToString("yyyy");
                string[] strArrays1 = new string[] { "", "en el último mes", "dos", "tres", "cuatro", "cinco", "seis" };
                int num = 0;
                this.liBase.Text = strArrays1[num];
                if ((!int.TryParse(item.Base, out num) ? false : num > 1))
                {
                    this.liBase.Text = string.Concat("en los últimos ", strArrays1[num], " meses");
                }
                this.liCedula.Text = item.AfiCedula;
                this.liLiteral.Text = item.Literal;
                this.liNroCuenta.Text = Formatos.formatoCuentaVisible(item.NroCuenta);
                this.liReferencia.Text = item.Referencia;
                long nAFId = item.Afiliado.nAF_Id;
                string titular = item.Titular;
                date = DateTime.Now.Date;
                string str = date.ToString("dd/MM/yyyy");
                DateTime now = DateTime.Now;
                HelperGlobal.LogRefBankAdd(nAFId, titular, str, now.ToLongTimeString(), item.Afiliado.sIP, string.Concat(item.sCod), item.TipoCuenta, item.NroCuenta, string.Empty, item.Dirigido.ToUpper(), item.FechaInicio, item.Base, string.Empty, item.Literal, "1", "A");
                this.LogRef(item.Afiliado);
            }
        }
    }
}