using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Consultas.Chequeras
{
    public partial class ConsultaChequera : Principal
    {
        protected HiddenField hSelChqra;

        protected HiddenField hChqra;

        protected HiddenField hSelChq;

        private List<ConsultaChequera.Chequera> data
        {
            get
            {
                return (List<ConsultaChequera.Chequera>)this.ViewState["data"];
            }
            set
            {
                this.ViewState["data"] = value;
            }
        }

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

        public ConsultaChequera()
        {
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected string chqStatus(string cod)
        {
            string str2;
            string str = cod;
            string str1 = str;
            if (str != null)
            {
                string str3 = str1;
                switch (str3)
                {
                    case "D":
                        {
                            str2 = "Disponible";
                            break;
                        }
                    case "A":
                        {
                            str2 = "Anulado";
                            break;
                        }
                    case "F":
                        {
                            str2 = "Certificado";
                            break;
                        }
                    case "P":
                        {
                            str2 = "Pagado";
                            break;
                        }
                    case "S":
                        {
                            str2 = "Suspendido";
                            break;
                        }
                    case "C":
                        {
                            str2 = "Conformado";
                            break;
                        }
                    case "X":
                        {
                            str2 = "Protestado";
                            break;
                        }
                    case "I":
                        {
                            str2 = "Inexistente";
                            break;
                        }
                    case "K":
                        {
                            str2 = "En proceso";
                            break;
                        }
                    default:
                        {
                            if (str3 == "T")
                            {
                                str2 = "Suspendido por Caja";
                                return str2;
                            }
                            str2 = "No Definido";
                            return str2;
                        }
                }
            }
            else
            {
                str2 = "No Definido";
                return str2;
            }
            return str2;
        }

        protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.ddlCuenta.SelectedIndex <= 0 ? true : this.ddlCuenta.SelectedValue.Equals("-")))
            {
                WebUtils.MessageBootstrap(this, "Debes seleccionar una cuenta", null);
                this.data = null;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<script>");
                stringBuilder.Append("$(document).ready(function(){");
                stringBuilder.Append("myData = [];loadTable();");
                stringBuilder.Append("});");
                this.loaded = true;
                stringBuilder.Append("</script>");
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "MyData", stringBuilder.ToString(), false);
            }
            else
            {
                this.data = null;
                this.mostrarChqras(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.ddlCuenta.getCuenta().SNroCuenta, this.ddlCuenta.SelectedItem.ToString());
            }
        }

        protected bool LogChequeras()
        {
            bool flag = false;
            try
            {
                flag = HelperGlobal.LogTransAdd(new DataLog()
                {
                    NAF_Id = base.Afiliado.nAF_Id,
                    SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString(),
                    SCod_Trans = "COCHQ",
                    SAF_IP = base.Afiliado.sIP,
                    SBanco = string.Empty,
                    SCuenta_Origen = string.Empty,
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
                    SConcepto = string.Concat("Consulta de Chequeras Exitosa de: ", base.Afiliado.sAF_NombreUsuario),
                    SMotivo_Suspension = string.Empty,
                    SDir_Envio_Chequera = string.Empty
                });
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        protected void mostrarChqras(long codCliente, string rif, string nrocta, string nroctamask)
        {
            try
            {
                this.data = new List<ConsultaChequera.Chequera>();
                RespuestaChequedsjv respuestaChequedsjv = HelperIbs.ibsConsultaChq(codCliente, rif, nrocta);
                if (respuestaChequedsjv.chequedsjv.chequedsDet.Length == 0)
                {
                    WebUtils.MessageBootstrap(this, "No existen chequeras asignadas para esta cuenta", null);
                }
                else
                {
                    ChequedsjvDet[] chequedsjvDetArray = respuestaChequedsjv.chequedsjv.chequedsDet;
                    for (int i = 0; i < (int)chequedsjvDetArray.Length; i++)
                    {
                        ChequedsjvDet chequedsjvDet = chequedsjvDetArray[i];
                        ConsultaChequera.Chequera chequera = new ConsultaChequera.Chequera()
                        {
                            Serial = chequedsjvDet.SSerial,
                            CantidadCheques = int.Parse(chequedsjvDet.SCantCheq),
                            CantidadDisponibles = int.Parse(chequedsjvDet.SNumCheqHabil),
                            NroInicial = Convert.ToInt32(chequedsjvDet.SCheqIni),
                            NroFinal = Convert.ToInt32(chequedsjvDet.SCheqFin),
                            Cheques = new List<ConsultaChequera.Cheque>()
                        };
                        int num = 0;
                        for (int j = chequera.NroInicial; j < chequera.NroFinal + 1; j++)
                        {
                            ConsultaChequera.Cheque cheque = new ConsultaChequera.Cheque()
                            {
                                id = j,
                                //status = chequedsjvDet.SEstatusCheq[num].ToString()
                            };
                            //char sEstatusCheq = chequedsjvDet.SEstatusCheq[num];
                            //cheque.descripcionstatus = this.chqStatus(sEstatusCheq.ToString());
                            chequera.Cheques.Add(cheque);
                            num++;
                        }
                        this.data.Add(chequera);
                    }
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("<script>");
                    stringBuilder.Append("$(document).ready(function(){");
                    stringBuilder.AppendFormat("myData = {0};loadTable();", javaScriptSerializer.Serialize(this.data));
                    stringBuilder.Append("});");
                    this.loaded = true;
                    stringBuilder.Append("</script>");
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "MyData", stringBuilder.ToString(), false);
                }
            }
            catch (IBException bException)
            {
                WebUtils.MessageBootstrap(this, "No se pudo mostrar las chequeras", null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                try
                {
                    this.ddlCuenta.HasTextoInicial = true;
                    this.ddlCuenta.TextoInicial = "Seleccione una cuenta a consultar";
                    this.ddlCuenta.TipoConsultaCuentasIBS = TipoConsultaCuentasIBS.CuentasDebito;
                    this.ddlCuenta.TipoCombo = TipoCombo.CuentasCliente;
                    this.ddlCuenta.TipoCuentaConsulta = TipoCuentaConsulta.TodasCorrientes;
                    this.ddlCuenta.TipoComboCuentaFormato = TipoComboCuentaFormato.Cuenta;
                    this.ddlCuenta.BindCombo();
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                }
            }
        }

        [Serializable]
        public class Cheque
        {
            public string descripcionstatus
            {
                get;
                set;
            }

            public int id
            {
                get;
                set;
            }

            public string status
            {
                get;
                set;
            }

            public Cheque()
            {
            }
        }

        [Serializable]
        public class Chequera
        {
            public int CantidadCheques
            {
                get;
                set;
            }

            public int CantidadDisponibles
            {
                get;
                set;
            }

            public int CantidadUsados
            {
                get;
                set;
            }

            public List<ConsultaChequera.Cheque> Cheques
            {
                get;
                set;
            }

            public int NroFinal
            {
                get;
                set;
            }

            public int NroInicial
            {
                get;
                set;
            }

            public string Serial
            {
                get;
                set;
            }

            public Chequera()
            {
            }
        }
    }
}