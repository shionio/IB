using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV
{
    public partial class sessionlogout : PrincipalNormal
    {
        
        public sessionlogout()
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
        }
    }
}