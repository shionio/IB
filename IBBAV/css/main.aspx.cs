using System;
using System.Web;
using System.Web.UI;

namespace IBBAV.css
{
    public class main : System.Web.UI.Page
    {
        public main()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            base.Response.ContentType = "text/css";
        }
    }
}