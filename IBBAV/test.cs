using IBBAV.UserControls;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV
{
	public class test : System.Web.UI.Page
	{
		protected HtmlForm form1;

		protected IBBAV.UserControls.PreguntasDesafio PreguntasDesafio;

		protected Button btnValidar;

		protected Literal resultado;

		public test()
		{
		}

		protected void btnValidar_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (KeyValuePair<short, string> preguntasRespuesta in this.PreguntasDesafio.getPreguntasRespuestas())
				{
					Literal literal = this.resultado;
					object text = literal.Text;
					literal.Text = string.Concat(new object[] { text, preguntasRespuesta.Key, ": ", preguntasRespuesta.Value, "<br>" });
				}
			}
			catch (Exception exception)
			{
				this.resultado.Text = exception.Message;
				return;
			}
			Literal literal1 = this.resultado;
			literal1.Text = string.Concat(literal1.Text, "<br>ok");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}