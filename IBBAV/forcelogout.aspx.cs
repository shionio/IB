using IBBAV.Entidades;
using IBBAV.Helpers;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV
{
    public partial class forcelogout : PrincipalNormal
    {
        
        public forcelogout()
        {
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string item = base.Request.QueryString["SessionId"];
                if (string.IsNullOrEmpty(item))
                {
                    item = this.Session["SessionId"] as string;
                }
                if (!string.IsNullOrEmpty(item))
                {
                    SessionAfiliado sessionAfiliado = HelperSession.SA_GetSession(item);
                    if (sessionAfiliado.SES_CodStatus.Equals("D"))
                    {
                        sessionAfiliado.SES_CodStatus = "I";
                        HelperSession.SA_UpdateSession(sessionAfiliado);
                    }
                }
            }
        }
    }
}