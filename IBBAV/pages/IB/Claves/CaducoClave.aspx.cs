using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.Claves
{
    public partial class CaducoClave : System.Web.UI.Page
    {
        private IBBAV.Entidades.Afiliado Afiliado
        {
            get
            {
                return this.ViewState["Afiliado"] as IBBAV.Entidades.Afiliado;
            }
            set
            {
                this.ViewState["Afiliado"] = value;
            }
        }

        public int sCod
        {
            get
            {
                int num;
                num = (this.ViewState["sCod"] != null ? int.Parse(this.ViewState["sCod"].ToString()) : 0);
                return num;
            }
            set
            {
                this.ViewState["sCod"] = value;
            }
        }

        public CaducoClave()
        {
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ValidarClaves();
                //Modificado 27/08/2018 por Liliana Guerra clave a mayuscula
                //if (HelperLogons.ActualizarClaveLogin(this.Afiliado.nAF_Id, this.Afiliado.sAF_Password, this.txtPassNew.Text.Trim(), int.Parse(this.ddlDiasCaducidad.SelectedValue), this.Afiliado.sIP, this.sCod))
                if (HelperLogons.ActualizarClaveLogin(this.Afiliado.nAF_Id, this.Afiliado.sAF_Password, this.txtPassNew.Text.Trim().ToUpper(), int.Parse(this.ddlDiasCaducidad.SelectedValue), this.Afiliado.sIP, this.sCod))
                {
                    this.LogCaducoClave(" Exitoso");
                    this.liMensaje1.Text = "Cambio de Clave realizada Satisfactoriamente";
                    this.liMensaje2.Text = "Por seguridad te recomendamos cambiar periódicamente tus claves";
                    HelperEnvioCorreo.BuscarCamposCorreo(37, this.Afiliado.sCO_Nombres.ToUpper(), this.Afiliado.CO_Email, new decimal(0), "", "", "", this.Afiliado.sCO_Nombres, "", "", "", "", "", "", "", "", "", "");
                }
                else
                {
                    this.LogCaducoClave(" Fallido");
                    this.liMensaje1.Text = "Problemas para realizar el Cambio de Clave";
                    this.liMensaje2.Text = "Por favor intenta más tarde";
                }
                this.liMensaje.Visible = false;
                this.panelClaves.Visible = false;
                this.panelResultado.Visible = true;
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/login.aspx");
        }

        protected bool LogCaducoClave(string result)
        {
            bool flag = false;
            try
            {
                flag = HelperGlobal.LogTransAdd(new DataLog()
                {
                    NAF_Id = this.Afiliado.nAF_Id,
                    SAF_NombreUsuario = this.Afiliado.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString(),
                    SCod_Trans = "AFCCV",
                    SAF_IP = this.Afiliado.sIP,
                    SBanco = string.Empty,
                    SCuenta_Origen = string.Empty,
                    SCuenta_Destino = string.Empty,
                    SMonto = string.Empty,
                    STipo_Tarjeta = string.Empty,
                    SBeneficiario = string.Empty,
                    SCedula_Id_B = string.Empty,
                    SSerial_Chequera = string.Empty,
                    SCheques = string.Empty,
                    STitular = this.Afiliado.sCO_Nombres,
                    ICantidad = 0,
                    SReferencia = string.Empty,
                    SConcepto = string.Concat("Cambio de clave por expiro de clave de login: ", result),
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
            if (this.Afiliado == null)
            {
                if (this.Context.Items["Afiliado"] != null)
                {
                    this.Afiliado = this.Context.Items["Afiliado"] as IBBAV.Entidades.Afiliado;
                }
                else
                {
                    base.Response.Redirect("~/login.aspx");
                }
            }
            if (this.Context.Items["type"] != null)
            {
                if (this.Context.Items["type"].ToString().Equals("0"))
                {
                    this.liMensaje.Text = "Por su seguridad debe cambiar su clave y seleccionar el tiempo de caducidad de la misma.";
                }
                else
                {
                    this.liMensaje.Text = "Su clave ha caducado, por favor cambie su clave por una nueva para seguir disfrutando de nuestros servicios.";
                }
            }
            if (!base.IsPostBack)
            {
                foreach (DataRow row in HelperGlobal.DiasPasswordGet().Tables[0].Rows)
                {
                    this.ddlDiasCaducidad.Items.Add(new ListItem(row["Descripcion"].ToString(), row["Dias"].ToString()));
                }
                ListItemCollection items = this.ddlDiasCaducidad.Items;
                int aFDiasPassword = this.Afiliado.AF_DiasPassword;
                ListItem listItem = items.FindByValue(aFDiasPassword.ToString());
                if (listItem != null)
                {
                    listItem.Selected = true;
                }
                //Modificado 03/07/2018 por Liliana Guerra para activar insercion de datos por teclado en input clave
                //this.txtPassNew.Attributes.Add("readonly", "readonly");
                //this.txtPassNewConfirm.Attributes.Add("readonly", "readonly");
            }
        }

        private void ValidarClaves()
        {
            if (this.txtPassNew.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("La clave de usuario es requerida");
            }
            if (this.txtPassNew.Text.Trim().Length < 8)
            {
                throw new Exception("La clave de usuario debe tener al menos 8 caracteres");
            }
            if (!(new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$@!%*#?&\\.])[A-Za-z\\d$@!%*#?&\\.]{8,10}$")).IsMatch(this.txtPassNew.Text.Trim()))
            {
                throw new Exception("Su nueva clave debe tener al menos un (4) caracteres alfabéticos, un (4) caracteres numéricos, un (1) caracter especial, de longitud mínima 8 y máximo 10");
            }
            if (this.txtPassNewConfirm.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("La confirmacón de clave de usuario es requerida");
            }
            //Modificado 27/08/2018 por Liliana Guerra clave mayuscula
            //if (!this.txtPassNew.Text.Trim().Equals(this.txtPassNewConfirm.Text))
            if (!this.txtPassNew.Text.Trim().ToUpper().Equals(this.txtPassNewConfirm.Text.Trim().ToUpper()))
            {
                throw new Exception("La clave y la confirmación no son iguales");
            }

            ////*************** IMPLEMENTACIÓN REGULACION 641-10 ******************************************************//
            ClaveNombre(this.Afiliado.sCO_Nombres,this.txtPassNew.Text.Trim());
            ClaveUser(this.Afiliado.sAF_NombreUsuario, this.txtPassNew.Text.Trim());
            ClaveTelefono(this.Afiliado.CO_Celular, this.txtPassNew.Text.Trim());
            ClaveCedula(this.Afiliado.sCedula,this.txtPassNew.Text.Trim());
            ClaveCuenta(this.txtPassNew.Text.Trim());
            PoliticaUsuarioClave.validaPoliticaClave(this.Afiliado.sAF_NombreUsuario,this.txtPassNew.Text, this.txtPassNewConfirm.Text);
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
            RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(this.Afiliado.AF_CodCliente.ToString(), this.Afiliado.sAF_Rif, TipoConsultaCuentasIBS.Todas);
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