using IBBAV.UserControls.Menu;
using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace IBBAV
{
    public partial class frame : PrincipalNormal
    {
        
        public frame()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BAVMenu1.LlenarMenu();
            }
            if (string.IsNullOrEmpty(this.Session["SessionID"] as string))
            {
                base.Response.Redirect("~/Login.aspx");
            }
        }
    }
}