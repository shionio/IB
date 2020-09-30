using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls;
using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.Claves
{
    public partial class ActualizarDatos : Principal
    {
        private EncabezadoTransaccion encabezado;
        

        public bool PreguntasValidadas
        {
            get
            {
                bool flag;
                object item = this.Session["PreguntasValidadasActualizarDatos"];
                flag = (item != null ? Convert.ToBoolean(item) : false);
                return flag;
            }
            set
            {
                this.Session["PreguntasValidadasActualizarDatos"] = value;
            }
        }

        private TipoTransaccionGenerica tg
        {
            get
            {
                TipoTransaccionGenerica tipoTransaccionGenerica;
                object item = this.ViewState["TipoTransaccionGenerica"];
                if (item != null)
                {
                    tipoTransaccionGenerica = (TipoTransaccionGenerica)item;
                }
                else
                {
                    tipoTransaccionGenerica = null;
                }
                return tipoTransaccionGenerica;
            }
            set
            {
                this.ViewState["TipoTransaccionGenerica"] = value;
            }
        }

        public ActualizarDatos()
        {
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string selectedValue = this.ddlistNumeroMovil.SelectedValue;
            string str = string.Concat(selectedValue, this.txtNumeroMovil.Text);
            string text = this.txtCorreo.Text;
            if ((this.rdoEmailSi.Checked ? true : this.rdoCelSi.Checked))
            {
                this.encabezado = new EncabezadoTransaccion();
                GActualizarDatos gActualizarDato = (this.tg != null ? (GActualizarDatos)this.tg.ObjetoTransaccion : new GActualizarDatos(base.Afiliado, base.sCod));
                if (this.rdoEmailSi.Checked)
                {
                    this.encabezado.AddEncabezado("Nuevo Correo Electrónico a Afiliar", Formatos.formatoEmail(text));
                    gActualizarDato.Correo = text;
                }
                else
                {
                    gActualizarDato.Correo = "";
                }
                if (this.rdoCelSi.Checked)
                {
                    this.encabezado.AddEncabezado("Nuevo Número de Celular a Afiliar", Formatos.formatoTarjeta(str));
                    gActualizarDato.Celular = str;
                }
                else
                {
                    gActualizarDato.Celular = "";
                }
                gActualizarDato.PaginaAnterior = string.Concat("~/pages/IB/Claves/ActualizarDatos.aspx?sCod=", base.sCod);
                gActualizarDato.PaginaSiguiente = "~/pages/Consolidada.aspx";
                if (this.tg == null)
                {
                    this.tg = new TipoTransaccionGenerica();
                }
                this.tg.EncabezadoTransaccion = this.encabezado;
                this.tg.ObjetoTransaccion = gActualizarDato;
                this.tg.Titulo = "Actualización de Datos";
                this.tg.Nota = "";
                this.tg.Nota2 = "";
                flag = true;
            }
            if (flag)
            {
                this.Context.Items.Add("TipoTransaccionGenerica", this.tg);
                base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
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
                this.panelDatos.Visible = true;
            }
        }

        protected void btnCancelarPreguntas_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/Consolidada.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.PreguntasValidadas)
            {
                this.panelValidacion.Visible = false;
                this.panelDatos.Visible = true;
            }
            if (!base.IsPostBack)
            {
                base.Afiliado = HelperAfiliado.AfiliadosGet(base.Afiliado.nAF_Id, EnumTipoCodigo.AF_ID);
                this.Session["Afiliado"] = base.Afiliado;
                if (!string.IsNullOrEmpty(base.Afiliado.CO_Celular))
                {
                    this.liNumeroActual.Text = Formatos.formatoTarjeta(base.Afiliado.CO_Celular);
                }
                else
                {
                    this.liNumeroActual.Text = "No Afiliado";
                }
                this.liCorreoActual.Text = Formatos.formatoEmail(base.Afiliado.CO_Email);
                this.tg = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
            }
            if (this.tg != null)
            {
                StringBuilder stringBuilder = new StringBuilder("<script>setTimeout(function(){");
                this.panelDatos.Visible = true;
                this.panelValidacion.Visible = false;
                GActualizarDatos objetoTransaccion = this.tg.ObjetoTransaccion as GActualizarDatos;
                if ((string.IsNullOrEmpty(objetoTransaccion.Correo) ? false : !base.IsPostBack))
                {
                    this.rdoEmailSi.Checked = true;
                    this.rdoEmailNo.Checked = false;
                    this.txtCorreo.Text = objetoTransaccion.Correo;
                    this.txtCorreoConfirmar.Text = objetoTransaccion.Correo;
                    stringBuilder.Append(string.Concat("$('#", this.rdoEmailSi.ClientID, "').prop('checked',true).trigger('change');"));
                }
                if ((string.IsNullOrEmpty(objetoTransaccion.Celular) ? false : !base.IsPostBack))
                {
                    this.rdoCelSi.Checked = true;
                    this.rdoCelNo.Checked = false;
                    string celular = objetoTransaccion.Celular;
                    string str = celular.Substring(0, 4);
                    this.ddlistNumeroMovil.ClearSelection();
                    this.ddlistNumeroMovil.Items.FindByValue(str).Selected = true;
                    this.ddlistNumeroMovilConfirm.ClearSelection();
                    this.ddlistNumeroMovilConfirm.Items.FindByValue(str).Selected = true;
                    TextBox textBox = this.txtNumeroMovil;
                    TextBox textBox1 = this.txtNumeroMovilConfirm;
                    string str1 = celular.Substring(4, celular.Length - 4);
                    string str2 = str1;
                    textBox1.Text = str1;
                    textBox.Text = str2;
                    stringBuilder.Append(string.Concat("$('#", this.rdoCelSi.ClientID, "').prop('checked',true).trigger('change');"));
                }
                stringBuilder.Append("}, 150);</script>");
                if (!base.IsPostBack)
                {
                    this.liScript.Text = stringBuilder.ToString();
                }
            }
        }
    }
}