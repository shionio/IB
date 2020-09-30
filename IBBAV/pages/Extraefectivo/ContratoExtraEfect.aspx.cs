using System;
using Functions;
using IBBAV.Helpers;

namespace IBBAV.pages.Extraefectivo
{
    public partial class ContratoExtraEfect : Principal
    {
        public ContratoExtraEfect()
        {
        }

        private String cedula;

        public bool PreguntasValidadas
        {
            get
            {
                bool flag;
                object item = this.Session["PreguntasValidadasExtraEfectivo"];
                flag = (item != null ? Convert.ToBoolean(item) : false);
                return flag;
            }
            set
            {
                this.Session["PreguntasValidadasExtraEfectivo"] = value;
            }
        }

        protected void btnAceptarPreguntas_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                flag = this.PreguntasDesafio.ValidarPreguntasRespuestas();
            }
            catch (IBException bException1)
            {
                IBException bException = bException1;
                if (bException.ReturnCode == "1003")
                {
                    base.Afiliado.nES_Id = (long)6;
                }
                WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                return;
            }
            catch (Exception exception)
            {
                WebUtils.MessageBootstrap(this, exception.Message, null);
                return;
            }
            this.PreguntasValidadas = flag;
            if (!flag)
            {
                WebUtils.MessageBootstrap(this, "Las respuestas no son correctas, por favor recuerde que al tercer intento, su usuario sera bloqueado por seguridad", null);
            }
            else
            {               
                
                this.panelValidacion.Visible = false;
                this.panelContrato.Visible = true;
            }
        }

        protected void btnCancelarPreguntas_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx", false);
        }

        protected void contbtn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("SolicitudExtraEfect.aspx", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cedula = 'V' + base.Afiliado.sCedula.PadLeft(9, '0');

            if (HelperExtracredito.tieneTarjeta(cedula))
            {
                if (this.PreguntasValidadas)
                {
                    this.panelValidacion.Visible = false;
                    this.panelContrato.Visible = true;
                }

            }
            else
            {
                this.panelValidacion.Visible = false;
                this.panelContrato.Visible = false;
                WebUtils.MessageBootstrap(this, string.Concat("Ud no dispone de tarjetas para solicitar Extra Efectivo en línea. Para regresar presione <a href=\"", base.ResolveUrl("~/pages/consolidada.aspx"), "\">aquí</a>"), null);

            }
        }
               
    }
}