using IBBAV.Entidades;
using IBBAV.Helpers;
using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV
{
    public partial class logout : System.Web.UI.Page
    {
       
        public logout()
        {
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.liFecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            string item = this.Session["SessionID"] as string;
            if (!string.IsNullOrEmpty(item))
            {
                SessionAfiliado now = HelperSession.SA_GetSession(item);
                if (now != null)
                {
                    now.TiempoFin = DateTime.Now;
                    now.SES_CodStatus = "I";
                    HelperSession.SA_UpdateSession(now);
                }
            }
            else
            {
                base.Response.Redirect("~/Login.aspx");
            }
            this.Session.Abandon();
        }
    }
}