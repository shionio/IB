using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.UserControls.BAVVK
{
	[ToolboxBitmap(typeof(TextBox))]
	[ToolboxData("<{0}:VK runat=server></{0}:VK>")]
	public class VK : TextBox
	{
		private IBBAV.UserControls.BAVVK.TipoTeclado tipoTeclado;

		public override bool Enabled
		{
			get
			{
				return false;
			}
		}

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(true)]
		[Localizable(false)]
		public IBBAV.UserControls.BAVVK.TipoTeclado TipoTeclado
		{
			get
			{
				return this.tipoTeclado;
			}
			set
			{
				this.tipoTeclado = value;
			}
		}

		public VK()
		{
		}

		protected override void OnPreRender(EventArgs e)
		{
			ClientScriptManager clientScript = this.Page.ClientScript;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<script>");
			stringBuilder.AppendFormat("var bavvk = $('{0}');", this.ClientID);
			stringBuilder.Append("</script>");
			clientScript.RegisterClientScriptBlock(this.Page.GetType(), "VKScript", stringBuilder.ToString());
			base.OnPreRender(e);
		}
	}
}