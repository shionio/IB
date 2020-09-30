using Functions;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV
{
    public partial class Login : System.Web.UI.Page
    {
        private Afiliado oAfiliado;

        private string login;

        private string password;

        public Login()
        {
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            this.liScript.Text = string.Empty;
            this.sError.Visible = false;
            base.ResolveUrl("~");
            this.login = this.AF_Login.Text;
            //Modificado 24/08/2018 por Liliana Guerra clave mayuscula
            //this.password = this.txtAF_Password.Text;
            this.password = this.txtAF_Password.Text.ToUpper();
            string empty = string.Empty;
            string str = "~/frame.aspx";
            this.sError.Text = "Error en Usuario y/o Clave Personal";

            try
            {
                if (this.login.Trim().Length == 0)
                {
                    throw new Exception("El Código de usuario no puede ser blanco o nulo.");
                }
                if (this.password.Trim().Length == 0)
                {
                    throw new Exception("Debe ingresar la Clave");
                }
                this.oAfiliado = HelperAfiliado.AutenticarAfiliado(this.login, this.password, WebUtils.GetClientIP(this));
                if (this.oAfiliado != null)
                {
                    if (this.oAfiliado.AF_FechaPassword == new DateTime(2000, 1, 1))
                    {
                        this.Context.Items.Add("Afiliado", this.oAfiliado);
                        this.Context.Items.Add("type", "0");
                        base.Server.Transfer("~/pages/IB/Claves/CaducoClave.aspx");
                    }
                    else if ((this.oAfiliado.AF_DiasPassword == 0 ? false : DateAndTime.DateDiff(DateInterval.Day, this.oAfiliado.AF_FechaPassword, DateTime.Now) >= (long)this.oAfiliado.AF_DiasPassword))
                    {
                        this.Context.Items.Add("Afiliado", this.oAfiliado);
                        this.Context.Items.Add("type", "1");
                        base.Server.Transfer("~/pages/IB/Claves/CaducoClave.aspx");
                    }
                    SessionAfiliado sessionAfiliado = new SessionAfiliado();
                    try
                    {
                        sessionAfiliado = HelperSession.SA_CreateSession(this.oAfiliado.nAF_Id);
                    }
                    catch (IBException bException)
                    {
                        base.Response.Redirect("~/forcelogout.aspx", false);
                        return;
                    }
                    empty = "redirect.aspx";
                    this.oAfiliado.sIP = WebUtils.GetClientIP(this);
                    this.Session.Add("SessionID", sessionAfiliado.Sesion);
                    string[] strArrays = IBBAVConfiguration.SessionTimeOut.Split(new char[] { ':' });
                    int num = int.Parse(strArrays[0]) * 60 + int.Parse(strArrays[1]);
                    this.Session.Timeout = num / 60;
                    this.Session.Add("Afiliado", this.oAfiliado);
                    this.sError.Text = string.Empty;
                }
            }
            catch (IBException bException2)
            {
                IBException bException1 = bException2;
                this.txtAF_Password.Text = string.Empty;
                if (bException1.ReturnCode == "1000")
                {
                    StringBuilder stringBuilder = new StringBuilder(50);
                    stringBuilder.Append("<script language='javascript' type='text/javascript'>");
                    stringBuilder.Append("var w = window.open('pages/IB/Claves/RecuperarClave.aspx?restore=','restorepass','width=1100,height=768,menubar=0,resizable=0,toolbar=0,left=300,top=150');");
                    stringBuilder.Append("w.focus(); ");
                    stringBuilder.Append("</script>");
                    this.liScript.Text = stringBuilder.ToString();
                }
                else
                {
                    this.sError.Text = bException1.IBMessage;
                    this.sError.Visible = true;
                    this.divmsg.Visible = true;
                    return;
                }
            }
            catch (Exception exception)
            {
                this.sError.Text = exception.Message;
                this.sError.Visible = true;
                return;
            }
            if (!empty.Equals(string.Empty))
            {
                this.LogLoginSuccessfull();
                base.Response.Redirect(str);
            }
        }

        protected void lnkRecPass_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/IB/Claves/RecuperarClave.aspx", false);
        }

        protected bool LogLoginSuccessfull()
        {
            bool flag = false;
            try
            {
                DataLog dataLog = new DataLog()
                {
                    NAF_Id = this.oAfiliado.nAF_Id,
                    SAF_NombreUsuario = this.oAfiliado.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString(),
                    SCod_Trans = "LOGEX",
                    SAF_IP = this.oAfiliado.sIP,
                    SBanco = string.Empty,
                    SCuenta_Origen = string.Empty,
                    SCuenta_Destino = string.Empty,
                    SMonto = string.Empty,
                    STipo_Tarjeta = string.Empty,
                    SBeneficiario = string.Empty,
                    SCedula_Id_B = string.Empty,
                    SSerial_Chequera = string.Empty,
                    SCheques = string.Empty,
                    STitular = this.oAfiliado.sCO_Nombres,
                    ICantidad = 0,
                    SReferencia = string.Empty,
                    SConcepto = string.Concat("Login Usuario al Site - Login Exitoso de: ", this.oAfiliado.sAF_NombreUsuario),
                    SMotivo_Suspension = string.Empty,
                    SDir_Envio_Chequera = string.Empty
                };
            }
            catch (IBException bException)
            {
                base.Response.Redirect("Login.aspx");
            }
            return flag;
        }
        //Modificado 03/07/2018 por Liliana Guerra para activar insercion de datos por teclado en input clave
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //	this.txtAF_Password.Attributes.Add("readonly", "readonly");
        //}
    }
}