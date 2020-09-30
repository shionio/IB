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
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.AfiliacionIB
{
    public partial class GetNewUser : System.Web.UI.Page
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

        public GetNewUser()
        {
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ValidarCampos();
                List<KeyValuePair<short, string>> preguntasRespuestas = this.PreguntasDesafio.getPreguntasRespuestas();
                Contactos contacto = new Contactos()
                {
                    sCO_Nombres = this.lblNombreCompleto.Text,
                    sCO_Apellidos = string.Empty,
                    nTI_id = 10,
                    nES_id = (long)2,
                    sCO_DocId = this.lblCedula.Text.Substring(1, this.lblCedula.Text.Length - 1),
                    nCO_TipoDocId = 1,
                    sCO_Email = this.txtCorreo.Text.Trim(),
                    sCO_Telefono = string.Empty,
                    sCO_EdificioCasa = string.Empty,
                    sCO_Calle = string.Empty,
                    sCO_Urbanizacion = string.Empty,
                    sCO_CodigoPostal = string.Empty,
                    sCO_Ciudad = string.Empty,
                    sCO_Estado = string.Empty,
                    sCO_Pais = "Venezuela",
                    sCO_Empresa = string.Empty,
                    nCA_Id = 2,
                    sCO_Email2 = string.Empty,
                    sCO_Telefono2 = string.Empty,
                    sCO_Celular = this.afi.CO_Celular
                };
                this.afi.sAF_NombreUsuario = this.txtUsuario.Text.Trim();
                //Modificado 27/08/2018 por Liliana Guerra clave a mayuscula
                //this.afi.sAF_Password = CryptoUtils.EncryptMD5(this.txtClave.Trim());
                this.afi.sAF_Password = CryptoUtils.EncryptMD5(this.txtClave.Text.Trim().ToUpper());
                this.afi.nTI_Id = contacto.nTI_id;
                this.afi.nCO_Id = contacto.nCO_id;
                this.afi.sAF_PreguntaDesafio = "";
                this.afi.sAF_RespuestaPD = "";
                this.afi.nES_Id = contacto.nES_id;
                this.afi.sIP = WebUtils.GetClientIP(this);
                this.afi.AF_DiasPassword = int.Parse(this.ddlDiasCaducidad.SelectedValue);

                //WebUtils.MessageBox(this, this.afi+"/Fin AFI/ "+contacto+"/Fin Contactos/ "+string.Empty+"/Fin Empty/ "+string.Empty+"/Fin Empty/ "+preguntasRespuestas+"/Fin Preguntas Respuestas /");
                if (!HelperAfiliado.GuardarAfiliado(this.afi, contacto, string.Empty, string.Empty, preguntasRespuestas))
                {
                    this.liTitulo.Text = "Problemas en la Afiliación del usuario";
                    this.liMensaje1.Text = "Ocurrió un error al realizar la Afiliación";
                    this.liMensaje2.Text = "Por favor intente mas tarde.";
                    this.liMensaje3.Text = string.Empty;
                }
                else
                {

                    string text = this.txtCorreo.Text;
                    if (text != string.Empty)
                    {
                        string empty = string.Empty;
                        string str = this.lblNombreCompleto.Text;
                        string empty1 = string.Empty;
                        string str1 = string.Empty;
                        string empty2 = string.Empty;
                        string str2 = string.Empty;
                        string empty3 = string.Empty;
                        string str3 = string.Empty;
                        string empty4 = string.Empty;
                        string str4 = string.Empty;
                        string empty5 = string.Empty;
                        string str5 = string.Empty;
                        string empty6 = string.Empty;
                        string str6 = string.Empty;
                        string empty7 = string.Empty;
                        string str7 = string.Empty;
                        HelperEnvioCorreo.BuscarCamposCorreo(46, str, text, new decimal(0), empty3, empty1, str1, empty2, str2, str3, empty4, str4, empty5, empty6, str6, empty7, str7, empty);

                        this.liTitulo.Text = "Afiliación Exitosa";
                        this.liMensaje1.Text = "El proceso de Afiliación se ha realizado exitosamente";
                        this.liMensaje2.Text = "Te informamos que de ahora en adelante, debes utilizar este Usuario y Clave para ingresar a BAV en línea";
                        this.liMensaje3.Text = "Haz \"Click\" en ACEPTAR para ingresar a BAV en línea.";
                    }
                }
                this.panelAfiliacionExitosa.Visible = true;
                this.panelNuevoAfiliado.Visible = false;
            }
            catch (IBException bException)
            {
                WebUtils.MessageBox(this, bException.IBMessage);
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox(this, exception.Message);
            }
        }

        protected void btnAceptar2_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Login.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Modificado 03/07/2018 por Liliana Guerra para activar insercion de datos por teclado en input clave
            //this.txtClave.Attributes.Add("readonly", "readonly");
            //this.txtClaveConfirmar.Attributes.Add("readonly", "readonly");
            this.btnAceptar2.Attributes.Add("onclick", "alert('Afiliación Satisfactoria \n ahora será enviado a la pagína de Login')");
            if (this.afi == null)
            {
                if (this.Context.Items["Afiliado"] != null)
                {
                    this.afi = (Afiliado)this.Context.Items["Afiliado"];
                    this.lblNombreCompleto.Text = this.afi.sCO_Nombres;
                    this.lblCedula.Text = this.afi.cedRIF;
                }
                else
                {
                    this.liTitulo.Text = "Problemas en la Afiliación del usuario";
                    this.liMensaje1.Text = "Ocurrió un error al realizar la Afiliación";
                    this.liMensaje2.Text = "Por favor intente mas tarde.";
                    this.liMensaje3.Text = "";
                    this.panelAfiliacionExitosa.Visible = true;
                    this.panelNuevoAfiliado.Visible = false;
                }
            }
            if (!base.IsPostBack)
            {
                foreach (DataRow row in HelperGlobal.DiasPasswordGet().Tables[0].Rows)
                {
                    this.ddlDiasCaducidad.Items.Add(new ListItem(row["Descripcion"].ToString(), row["Dias"].ToString()));
                }
                this.ddlDiasCaducidad.ClearSelection();
            }
            //Modificado 03/07/2018 por Liliana Guerra para activar insercion de datos por teclado en input clave
            //this.txtClave.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
            //this.txtClaveConfirmar.Attributes.Add("onKeypress", "return OnlyNumeric(event,this.value);");
        }

        private void ValidarCampos()
        {
            Regex regex = new Regex("([a-z|A-Z|0-9]){6,10}$");
            if (this.txtUsuario.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("La identificación de usuario es requerida");
            }
            if (!regex.IsMatch(this.txtUsuario.Text.Trim()))
            {
                throw new Exception("El nombre de usuario debe tener entre 6 y 10 letras y/o números, sin espacios, comas ni puntos");
            }
            if (this.txtUsuario.Text.Trim().Contains(" "))
            {
                throw new Exception("El nombre de usuario debe tener entre 6 y 10 letras y/o números, sin espacios, comas ni puntos");
            }
            if (HelperAfiliado.ExisteAfiliado(string.Empty, string.Empty, this.txtUsuario.Text.Trim()))
            {
                throw new Exception(string.Concat("Ya existe un usuario con este nombre: ", this.txtUsuario.Text.Trim()));
            }
            if (HelperAfiliado.ExisteAfiliado(this.afi.cedRIF, string.Empty, string.Empty))
            {
                throw new Exception(string.Concat("Ya existe un usuario con esta identificación: ", this.afi.cedRIF));
            }
            if (this.txtClave.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("La clave de usuario es requerida");
            }
            if (this.txtClave.Text.Trim().Length < 8)
            {
                throw new Exception("La clave de usuario debe tener entre 8 y 10 caracteres");
            }
            if (!(new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$@!%*#?&\\.])[A-Za-z\\d$@!%*#?&\\.]{8,10}$")).IsMatch(this.txtClave.Text.Trim()))
            {
                throw new Exception("Su nueva clave debe tener al menos 1 caracter alfabético, 1 caracter numérico, 1 caracter especial, de longitud mínima 8 y máximo 10");
            }
            if (this.txtClaveConfirmar.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("La confirmacón de clave de usuario es requerida");
            }
            if (this.txtClaveConfirmar.Text.Trim().Length < 8)
            {
                throw new Exception("La clave de usuario debe tener entre 8 y 10 caracteres");
            }
            //Modificado 27/08/2018 por Liliana Guerra clave a mayuscula
            //if (!this.txtClave.Text.Trim().Equals(this.txtClaveConfirmar.Text))
            if (!this.txtClave.Text.Trim().ToUpper().Equals(this.txtClaveConfirmar.Text.Trim().ToUpper()))
            {
                throw new Exception("La clave y la confirmación no son iguales");
            }
            if (this.txtCorreo.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("El correo electrónico es requerido");
            }
            if (!Tools.TestEmailRegex(this.txtCorreo.Text.Trim()))
            {
                throw new Exception("El correo electrónico es inválido");
            }
            if (this.txtCorreoConfirmar.Text.Trim().Equals(string.Empty))
            {
                throw new Exception("El correo electrónico es requerido");
            }
            if (!Tools.TestEmailRegex(this.txtCorreoConfirmar.Text.Trim()))
            {
                throw new Exception("El correo electrónico es inválido");
            }
            if (!this.txtCorreo.Text.Trim().Equals(this.txtCorreoConfirmar.Text.Trim()))
            {
                throw new Exception("El correo electrónico y su confirmación no son iguales");
            }
            //////*************** IMPLEMENTACIÓN REGULACION 641-10 ******************************************************//
            ValUserNombre(this.afi.sCO_Nombres, this.txtUsuario.Text.Trim());
            ValUserTelefono(this.afi.CO_Celular, this.txtUsuario.Text.Trim());
            ValUserCedula(this.lblCedula.Text.Substring(1), this.txtUsuario.Text.Trim());
            ValUserCuenta(this.txtUsuario.Text.Trim());
            ClaveNombre(this.afi.sCO_Nombres, this.txtClave.Text.Trim());
            ClaveUser(this.txtUsuario.Text.Trim(), this.txtClave.Text.Trim());
            ClaveTelefono(this.afi.CO_Celular, this.txtClave.Text.Trim());
            ClaveCedula(this.lblCedula.Text.Substring(1), this.txtClave.Text.Trim());
            ClaveCuenta(this.txtClave.Text.Trim());
            PoliticaUsuarioClave.validaPoliticaUsuario(this.txtUsuario.Text.Trim());
            PoliticaUsuarioClave.validaPoliticaClave(this.txtUsuario.Text.Trim(), this.txtClave.Text.Trim(), this.txtClaveConfirmar.Text.Trim());
        }



        private void ValUserNombre(string nombre, string cadena)
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
                                throw new Exception("El nombre de usuario no pude contener datos personales");
                            }
                        }
                    }
                }
            }
        }

        private void ValUserTelefono(string telefono, string cadena)
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
                                throw new Exception("El nombre de usuario no pude contener datos de contacto");
                            }
                        }
                    }
                }
            }
        }

        private void ValUserCedula(string cedula, string cadena)
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
                                throw new Exception("El nombre de usuario no pude contener datos personales");
                            }
                        }
                    }
                }
            }
        }

        private void ValUserCuenta(string cadena)
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
                                throw new Exception("El nombre de usuario no pude contener datos de sus cuentas");
                            }
                        }
                        inicio = inicio + 1;
                    }
                }
            }
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