using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.AfiliacionIB
{
    public partial class terms_conditions : System.Web.UI.Page
    {
       public terms_conditions()
       {
       }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Login.aspx", false);
        }

        protected void contbtn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("NewUser.aspx", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}