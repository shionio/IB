using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.Claves
{
    public partial class RecuperarClave : System.Web.UI.Page
    {       

        private Afiliado afi
        {
            get
            {
                return (Afiliado)this.ViewState["Afiliado"];
            }
            set
            {
                this.ViewState["Afiliado"] = value;
            }
        }

        public RecuperarClave()
        {
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool flag = false;
            this.afi = new Afiliado();
            try
            {
                this.ValidarCampos();
                List<Afiliado> afiliados = HelperAfiliado.AfiliadosGet(string.Concat(this.ddlTipoCedula.SelectedValue, this.txtCedula.Text), EnumTipoValor.CedulaAfiliado);
                if (afiliados.Count <= 0)
                {
                    throw new Exception("Usuario o tarjeta no válida");
                }
                this.afi = afiliados[0];
                if (this.afi != null)
                {
                    HelperLogons.ValidarIntentoRecuparecionCLV(this.afi.nAF_Id, this.tddnumtxt.Text, Tools.GetClientIP());
                    flag = true;
                    string text = this.txtCta.Text;
                    RespuestaAfilpedsjv respuestaAfilpedsjv = HelperIbs.ibsAfiliarNatural(this.afi.AF_CodCliente, this.afi.sAF_Rif, text, this.tddnumtxt.Text);
                    this.afi.AF_CodCliente = long.Parse(respuestaAfilpedsjv.afilpedsjv.SUserId);
                }
            }
            catch (IBException bException2)
            {
                IBException bException = bException2;
                string empty = string.Empty;
                bool flag1 = false;
                try
                {
                    if ((bException.IsErrorIB || this.afi == null ? false : this.afi.nAF_Id > (long)0))
                    {
                        HelperLogons.ValidarIntentoRecuparecionCLVFALLIDO(this.afi.nAF_Id, this.tddnumtxt.Text, Tools.GetClientIP());
                    }
                }
                catch (IBException bException1)
                {
                    empty = bException1.IBMessage;
                    flag1 = true;
                }
                if (flag1)
                {
                    WebUtils.MessageBox2005(this, empty);
                }
                else
                {
                    WebUtils.MessageBox2005(this, bException.IBMessage);
                }
                return;
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message);
                return;
            }
            if ((!flag ? false : this.afi != null))
            {
                this.PreguntasDesafio.llenarPreguntasAfiliado(this.afi.nAF_Id);
                this.panelDatos.Visible = false;
                this.panelValidacion.Visible = true;
            }
        }

        protected void btnAceptar2_Click(object sender, EventArgs e)
        {
            try
            {
                this.PreguntasDesafio.ValidarPreguntasRespuestas();
                this.panelValidacion.Visible = false;
                this.panelClave.Visible = true;
            }
            catch (IBException bException)
            {
                WebUtils.MessageBox2005(this, bException.IBMessage);
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message);
            }
        }

        protected void btnAceptar3_Click(object sender, EventArgs e)
        {
            try
            {
                 this.ValidarClaves();
                //Modificado 27 / 08 / 2018 por Liliana Guerra clave mayuscula
                //if (HelperLogons.ActualizarClaveLogin(this.afi.nAF_Id, this.afi.sAF_Password, this.txtClave.Text.Trim(), this.afi.AF_DiasPassword, WebUtils.GetClientIP(this), 0))
                if (HelperLogons.ActualizarClaveLogin(this.afi.nAF_Id, this.afi.sAF_Password, this.txtClave.Text.Trim().ToUpper(), this.afi.AF_DiasPassword, WebUtils.GetClientIP(this), 0))
                {
                    HelperLogons.ActivarUsuario(this.afi.nAF_Id);
                    this.liMensaje1.Text = "Recuperación de Clave realizada Satisfactoriamente";
                    this.liMensaje2.Text = "Por seguridad te recomendamos cambiar periódicamente tus claves";
                }
                else
                {
                    this.liMensaje1.Text = "Problemas para realizar la Recuperación de Clave";
                    this.liMensaje2.Text = "Por favor intenta más tarde";
                }
                this.panelClave.Visible = false;
                this.panelExito.Visible = true;
            }
            catch (PoliticaUsuarioClave.ErrorClaveException errorClaveException)
            {
                WebUtils.MessageBox(this, errorClaveException.Message);
            }
            catch (IBException bException)
            {
                WebUtils.MessageBox2005(this, bException.IBMessage);
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message);
            }
        }

        protected void btnAceptarPreguntas_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                flag = this.PreguntasDesafio.ValidarPreguntasRespuestas(this.afi.nAF_Id);
            }
            catch (IBException bException)
            {
                WebUtils.MessageBox2005(this, bException.IBMessage, null);
                return;
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox2005(this, exception.Message, null);
                return;
            }
            if (!flag)
            {
                WebUtils.MessageBox2005(this, "Las respuestas no son correctas, por favor recuerde que al tercer intento, su usuario sera bloqueado por seguridad", null);
            }
            else
            {
                this.panelValidacion.Visible = false;
                this.panelClave.Visible = true;
                this.liScript.Text = "<script>setInterval(valdata, 100);</script>";
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected void btnCancelar3_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected void btnCancelarPreguntas_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected bool LogRecuperarClave(IBBAV.Entidades.Afiliado Afiliado, string result)
        {
            bool flag = false;
            try
            {
                flag = HelperGlobal.LogTransAdd(new DataLog()
                {
                    NAF_Id = Afiliado.nAF_Id,
                    SAF_NombreUsuario = Afiliado.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString(),
                    SCod_Trans = "ACCLE",
                    SAF_IP = Afiliado.sIP,
                    SBanco = string.Empty,
                    SCuenta_Origen = string.Empty,
                    SCuenta_Destino = string.Empty,
                    SMonto = string.Empty,
                    STipo_Tarjeta = string.Empty,
                    SBeneficiario = string.Empty,
                    SCedula_Id_B = string.Empty,
                    SSerial_Chequera = string.Empty,
                    SCheques = string.Empty,
                    STitular = Afiliado.sCO_Nombres,
                    ICantidad = 0,
                    SReferencia = string.Empty,
                    SConcepto = string.Concat("Cambio de clave de transacciones: ", result),
                    SMotivo_Suspension = string.Empty,
                    SDir_Envio_Chequera = string.Empty
                });
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (base.Request.QueryString["restore"] != null)
            {
                this.trAviso.Visible = true;
            }
            this.txtCedula.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
            this.tddnumtxt.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
            //Modificado 03/07/2018 por Liliana Guerra para activar insercion de datos por teclado en input clave
            //this.txtClave.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
            //this.txtClave.Attributes.Add("readonly", "readonly");
            //this.txtClaveConfirmar.Attributes.Add("readonly", "readonly");
        }

        private void ValidarCampos()
        {
            if (this.tddnumtxt.Text.Equals(string.Empty))
            {
                throw new Exception("Debe colocar un número de Tarjeta de Débito");
            }
            if (this.tddnumtxt.Text.Length < 19)
            {
                throw new Exception("El número de Tarjeta de Débito debe poseer 19 números");
            }
            if (this.txtCedula.Text.Equals(string.Empty))
            {
                throw new Exception("Debe indicar el número de cédula");
            }
            if (this.txtCta.Text.Equals(string.Empty))
            {
                throw new Exception("Debe indicar lo números de la cuenta");
            }
            if (this.txtCta.Text.Length < 4)
            {
                throw new Exception("Debe indicar lo últimos 4 números de su cuenta");
            }
        }

        private void ValidarClaves()
        {
            List<Afiliado> afiliados = HelperAfiliado.AfiliadosGet(afi.AF_CodCliente);
            afi = afiliados[0];
            if (this.txtClave.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("Su nueva clave es requerida");
            }
            if (this.txtClave.Text.Trim().Length < 8)
            {
                throw new Exception("Su nueva clave debe tener al menos 8 caracteres");
            }
            if (this.txtClaveConfirmar.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("La confirmación de su nueva clave es requerida");
            }
            //Modificado 27/08/2018 por Liliana Guerra clave mayuscula
            //if (!this.txtClave.Text.Trim().Equals(this.txtClaveConfirmar.Text.Trim()))
            if (!this.txtClave.Text.Trim().ToUpper().Equals(this.txtClaveConfirmar.Text.Trim().ToUpper()))
            {
                throw new Exception("La nueva clave y su confirmación no coinciden");
            }
            if (!(new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$@!%*#?&\\.])[A-Za-z\\d$@!%*#?&\\.]{8,10}$")).IsMatch(this.txtClave.Text.Trim()))
            {
                throw new Exception("Su nueva clave debe tener al menos un (4) caracteres alfabéticos, un (4) caracteres numéricos, un (1) caracter especial, de longitud mínima 8 y máximo 10");
            }
            // //*************** IMPLEMENTACIÓN REGULACION 641-10 ******************************************************//
            ClaveNombre(this.afi.sCO_Nombres, this.txtClave.Text.Trim());
            ClaveUser(this.afi.sAF_NombreUsuario, this.txtClave.Text.Trim());
            ClaveTelefono(this.afi.CO_Celular, this.txtClave.Text.Trim());
            ClaveCedula(this.afi.sCedula, this.txtClave.Text.Trim());
            ClaveCuenta(this.txtClave.Text.Trim());
            PoliticaUsuarioClave.validaPoliticaClave(this.afi.sAF_NombreUsuario, this.txtClave.Text, this.txtClaveConfirmar.Text);            
        }

        private void ClaveNombre(string nombre, string cadena)
        {
            nombre = nombre.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ü', 'u').Replace('ñ', 'n').Replace(" ", "");
            cadena = cadena.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ü', 'u').Replace('ñ', 'n').Replace(" ", "");
            ArrayList partes = new ArrayList();

            for (int i = 0; i <= nombre.Length - 4; i++)
            {
                partes.Add(nombre.Substring(i, 4));
            }

            if (partes.Count > 0)
            {
                foreach (string parte in partes)
                {
                    for (int i = 0; i <= partes.Count; i++)
                    {
                        foreach (Match match in (new Regex("^[a-z]+$")).Matches(parte))
                        {
                            if (cadena.Contains(match.Value))
                            {
                                throw new Exception("La clave no pude contener datos personales");
                            }
                        }
                    }
                }
            }
        }

        private void ClaveUser(string nombre, string cadena)
        {
            nombre = nombre.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ü', 'u').Replace('ñ', 'n').Replace(" ", "");
            cadena = cadena.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ü', 'u').Replace('ñ', 'n').Replace(" ", "");
            ArrayList partes = new ArrayList();

            for (int i = 0; i <= nombre.Length - 4; i++)
            {
                partes.Add(nombre.Substring(i, 4));
            }

            if (partes.Count > 0)
            {
                foreach (string parte in partes)
                {
                    for (int i = 0; i <= partes.Count; i++)
                    {
                        foreach (Match match in (new Regex("^[a-z]+$")).Matches(parte))
                        {
                            if (cadena.Contains(match.Value))
                            {
                                throw new Exception("La clave no puede contener su nombre de usuario");
                            }
                        }
                    }
                }
            }
        }

        private void ClaveTelefono(string telefono, string cadena)
        {
            ArrayList partes = new ArrayList();

            for (int i = 0; i <= telefono.Length - 4; i++)
            {
                partes.Add(telefono.Substring(i, 4));
            }

            if (partes.Count > 0)
            {
                foreach (string parte in partes)
                {
                    for (int i = 0; i <= partes.Count; i++)
                    {
                        foreach (Match match in (new Regex("^[0-9]+$")).Matches(parte))
                        {
                            if (cadena.Contains(match.Value))
                            {
                                throw new Exception("La clave no pude contener datos de contacto");
                            }
                        }
                    }
                }
            }
        }

        private void ClaveCedula(string cedula, string cadena)
        {
            ArrayList partes = new ArrayList();

            for (int i = 0; i <= cedula.Length - 4; i++)
            {
                partes.Add(cedula.Substring(i, 4));
            }

            if (partes.Count > 0)
            {
                foreach (string parte in partes)
                {
                    for (int i = 0; i <= partes.Count; i++)
                    {
                        foreach (Match match in (new Regex("^[0-9]+$")).Matches(parte))
                        {
                            if (cadena.Contains(match.Value))
                            {
                                throw new Exception("La clave no pude contener datos personales");
                            }
                        }
                    }
                }
            }
        }

        private void ClaveCuenta(string cadena)
        {
            RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(this.afi.AF_CodCliente.ToString(), this.afi.sAF_Rif, TipoConsultaCuentasIBS.Todas);
            List<IbaConsDet> ibaConsDets = new List<IbaConsDet>();
            ibaConsDets.AddRange(respuestaIbaCon.sdjvCuentas.sdsjvDetalle);
            List<IbaConsDet> ibaConsDets2 = ibaConsDets.FindAll((IbaConsDet x) => x.SClasecuenta.Equals("02") && !x.STipoFirma.Contains("N"));
            ArrayList cuentas = new ArrayList();

            foreach (IbaConsDet item in ibaConsDets2)
            {
                cuentas.Add(item.SNroCuenta.Substring(10, 10));
            }

            if (cuentas.Count > 0)
            {
                string cuentaV = null;
                foreach (string cuenta in cuentas)
                {
                    int inicio = 0;
                    int fin = 4;
                    cuentaV = cuenta;
                    for (int i = 0; i <= 6; i++)
                    {
                        foreach (Match match in (new Regex("^[0-9]+$")).Matches(cuentaV.Substring(inicio, fin)))
                        {
                            if (cadena.Contains(match.Value))
                            {
                                throw new Exception("La clave no pude contener datos de sus cuentas");
                            }
                        }
                        inicio = inicio + 1;
                    }
                }
            }
        }
    }
}