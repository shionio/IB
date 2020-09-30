using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.AfiliacionIB
{
    public partial class NewUser : System.Web.UI.Page
    {
        public Afiliado afi
        {
            get
            {
                return this.ViewState["Afiliado"] as Afiliado;
            }
            set
            {
                this.ViewState["Afiliado"] = value;
            }
        }

        public string clvTemporal
        {
            get
            {
                object item = this.ViewState["clvTemporal"];
                return (item != null ? item.ToString() : "");
            }
            set
            {
                this.ViewState["clvTemporal"] = value;
            }
        }

        public NewUser()
        {
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string empty = string.Empty;
            //Daba Error Objeto no instanciado ***** this.afi = new Afiliado(null); ****
            //Modificado 07/08/2016 por Liliana Guerra
            this.afi = new Afiliado();
            try
            {
                this.ValidarCampos();
                List<Afiliado> afiliados = HelperAfiliado.AfiliadosGet(string.Concat(this.ddlTipoCedula.SelectedValue, this.txtCedula.Text.Trim()), EnumTipoValor.CedulaAfiliado);
                
                if (afiliados.Count > 0)
                {
                    this.afi = afiliados[0];
                    throw new Exception("Usted ya se encuentra afiliado al servicio de Internet Banking");
                }                

                this.afi = new Afiliado()
                {
                    cedRIF = string.Concat(this.ddlTipoCedula.SelectedValue, this.txtCedula.Text.Trim()),
                    sCedula = this.txtCedula.Text.Trim(),
                    sAF_Rif = string.Concat(this.ddlTipoCedula.SelectedValue, this.txtCedula.Text.Trim()),
                    sTarjeta = this.tddnumtxt.Text.Trim()
                };

                string str = this.txtCta.Text.Trim();
                RespuestaAfilpedsjv respuestaAfilpedsjv = HelperIbs.ibsAfiliarNatural((long)0, this.afi.cedRIF, str, this.tddnumtxt.Text);
                this.afi.AF_CodCliente = long.Parse(respuestaAfilpedsjv.afilpedsjv.SUserId);
                this.afi.AF_CodOficina = int.Parse(respuestaAfilpedsjv.afilpedsjv.SOfcCte);
                this.afi.sCO_Nombres = respuestaAfilpedsjv.afilpedsjv.SNomCte.ToUpper();
                this.afi.CO_Celular = respuestaAfilpedsjv.afilpedsjv.SCelCte;
                bool flag1 = (this.afi.CO_Celular.StartsWith("0412") || this.afi.CO_Celular.StartsWith("0414") || this.afi.CO_Celular.StartsWith("0424") || this.afi.CO_Celular.StartsWith("0416") ? true : this.afi.CO_Celular.StartsWith("0426"));
                                
                if ((this.afi.CO_Celular.Trim().Equals(string.Empty) ? true : !flag1))
                {
                    throw new Exception("Número de celular no válido, por favor dirigirse a una agencia o llamar al número 0500-228.0001");
                }
                flag = true;
            }
            catch (IBException bException)
            {
                empty = bException.IBMessage;
                WebUtils.MessageBox(this, empty);
                return;
            }
            catch (Exception exception)
            {
                empty = exception.Message;
            }
            if ((!flag ? false : this.afi != null))
            {
                Random random = new Random(DateTime.Now.Millisecond);
                int num = random.Next(1000000, 2147483647);
                this.clvTemporal = num.ToString();
                HelperTedexis.sendSMS(this.afi.CO_Celular, string.Concat("BAV informa que su Clave de Afiliacion Temporal es: ", this.clvTemporal, ". Si usted no esta realizando esta operacion llame al 0500-288.00.01"));
                HelperLogons.ValidarAfiliacionIntentos(this.afi.sTarjeta, this.clvTemporal, "1");
                this.panelDatos.Visible = false;
                this.panelMSG.Visible = true;
            }
            else
            {
                try
                {
                    HelperLogons.FallidoIntentoAfiliacion(this.tddnumtxt.Text, Tools.GetClientIP());
                }
                catch (IBException bException1)
                {
                    empty = bException1.IBMessage;
                }
            }
            if (!empty.Equals(string.Empty))
            {
                WebUtils.MessageBox(this, empty);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Login.aspx", false);
        }

        protected void btnCancelarSMS_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Login.aspx", false);
        }

        protected void btnContinuarSMS_Click(object sender, EventArgs e)
        {            
            bool flag = false;
            try
            {
                if (this.txtClave.Text.Trim().Equals(string.Empty))
                {
                    throw new Exception("Debe indicar la clave de Afiliación Temporal");
                }
                string str = HelperLogons.ValidarAfiliacionIntentos(this.afi.sTarjeta, this.txtClave.Text.Trim(), "2");
                if (str.Equals("1"))
                {
                    throw new Exception("Clave inválida, por favor intente de nuevo");
                }
                if (str.Equals("2"))
                {
                    throw new Exception("Afiliación bloqueada");
                }
                if (str.Equals("3"))
                {
                    this.btnContinuarSMS.Visible = false;
                    throw new Exception("Clave caducada");
                }
                flag = true;
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox(this, exception.Message);
                return;
            }

            if (flag)
            {
                this.Context.Items.Add("Afiliado", this.afi);
                base.Server.Transfer("GetNewUser.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tddnumtxt.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
            this.txtCta.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
        }

        private void ValidarCampos()
        {
            if (this.tddnumtxt.Text.Equals(string.Empty))
            {
                throw new Exception("Debe colocar un número de Tarjeta de Débito");
            }
            if (this.tddnumtxt.Text.Length < 19)
            {
                throw new Exception("El número de Tarjeta de Débito debe poseer 19 dígitos");
            }
            if ((this.tddnumtxt.Text.StartsWith("6397816") ? true : this.tddnumtxt.Text.StartsWith("901")))
            {
                throw new Exception("Número de Tarjeta de Débito no permitida");
            }
            HelperLogons.ValidarIntentoAfiliacion(this.tddnumtxt.Text, Tools.GetClientIP());
            if (this.txtCedula.Text.Equals(string.Empty))
            {
                throw new Exception("Debe indicar el número de cédula");
            }
            if (this.txtCta.Text.Equals(string.Empty))
            {
                throw new Exception("Indique los últimos 4 números de su cuenta");
            }
            if (this.txtCta.Text.Length < 4)
            {
                throw new Exception("Se requieren los últimos 4 números de su cuenta");
            }
            if (HelperLogons.ValidarAfiliacionIntentos(this.tddnumtxt.Text, string.Empty, "3").Equals("3"))
            {
                throw new Exception("Tarjeta bloqueada para realizar la afiliación, por favor dirigirse a una agencia o llamar al número 0500-228.0001");
            }
        }
    }
}