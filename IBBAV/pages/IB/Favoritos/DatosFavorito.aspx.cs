using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.IB.Favoritos
{
    public partial class DatosFavorito : Principal
    {
        private bool servicio;

        private EncabezadoTransaccion encabezado;
        
        private string banco
        {
            get
            {
                object item = this.ViewState["banco"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["banco"] = value;
            }
        }

        private string beneficiario
        {
            get
            {
                object item = this.ViewState["beneficiario"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["beneficiario"] = value;
            }
        }

        private string cedula
        {
            get
            {
                object item = this.ViewState["cedula"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["cedula"] = value;
            }
        }

        private string codarea
        {
            get
            {
                object item = this.ViewState["codarea"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["codarea"] = value;
            }
        }

        private string descripcion
        {
            get
            {
                object item = this.ViewState["descripcion"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["descripcion"] = value;
            }
        }

        private string email
        {
            get
            {
                object item = this.ViewState["email"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["email"] = value;
            }
        }

        private string numeroinstrumento
        {
            get
            {
                object item = this.ViewState["numeroinstrumento"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["numeroinstrumento"] = value;
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

        private string tipocedula
        {
            get
            {
                object item = this.ViewState["tipocedula"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["tipocedula"] = value;
            }
        }

        private string tipotarjeta
        {
            get
            {
                object item = this.ViewState["tipotarjeta"];
                return (item != null ? (string)item : string.Empty);
            }
            set
            {
                this.ViewState["tipotarjeta"] = value;
            }
        }

        public DatosFavorito()
        {
        }

        private void AddDescripcionEmail()
        {
            TableRowCollection rows = this.tblDatos.Rows;
            Control[] controlArray = new Control[] { WebUtils.CreateTextBox("txtDescripcion", 60, this.descripcion, false, "form-control input-sm txtDescripcion") };
            rows.Add(WebUtils.BuildTableRow("Descripción:", controlArray, ""));
            TableRowCollection tableRowCollections = this.tblDatos.Rows;
            Control[] controlArray1 = new Control[] { WebUtils.CreateTextBox("txtEmail", 50, this.email, false, "form-control input-sm txtEmail") };
            tableRowCollections.Add(WebUtils.BuildTableRow("Correo Electrónico:", controlArray1, ""));
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool flag = false;
            this.CamposServicio();
            try
            {
                this.ValidarCampos();
                TipoFavorito tipoFavorito = this.ddlTipoFavoritos.getTipoFavorito();
                GAfiliacionFavorito gAfiliacionFavorito = (this.tg != null ? (GAfiliacionFavorito)this.tg.ObjetoTransaccion : new GAfiliacionFavorito(base.Afiliado, base.sCod));
                if (gAfiliacionFavorito.Accion == EnumAccionAddUpdateAfiliadoFavoritos.Insert)
                {
                    if (HelperFavorito.AfiliadoFavoritosGet(base.Afiliado.nAF_Id, tipoFavorito.TipoFavoritoID, this.numeroinstrumento) != null)
                    {
                        WebUtils.MessageBootstrap(this, "Favorito ya esta registrado", null);
                        return;
                    }
                }
                gAfiliacionFavorito.AfiliadoFavorito = new AfiliadoFavorito()
                {
                    Activo = "A",
                    BankId = this.banco,
                    Beneficiario = this.beneficiario,
                    CedulaRif = this.cedula,
                    Descripcion = this.descripcion,
                    Email = this.email,
                    nAF_Id = base.Afiliado.nAF_Id,
                    NumeroInstrumento = this.numeroinstrumento,
                    TipoDescripcion = string.Empty,
                    TipoFavoritoId = tipoFavorito.TipoFavoritoID,
                    TipoTarjetaCredito = this.tipotarjeta
                };
                gAfiliacionFavorito.PaginaAnterior = string.Concat("~/pages/IB/Favoritos/DatosFavorito.aspx?sCod=", base.sCod);
                gAfiliacionFavorito.PaginaSiguiente = string.Concat("~/pages/IB/Favoritos/MenuFavoritos.aspx?sCod=", base.sCod);
                if (this.servicio)
                {
                    gAfiliacionFavorito.MensajeSatisfactorio = "";
                }
                else
                {
                    gAfiliacionFavorito.MensajeSatisfactorio = "";
                }
                if (this.tg == null)
                {
                    this.tg = new TipoTransaccionGenerica();
                }
                this.tg.EncabezadoTransaccion = this.encabezado;
                this.tg.ObjetoTransaccion = gAfiliacionFavorito;
                this.tg.Titulo = "Registro de Favoritos";
                this.tg.Nota = "";
                this.tg.Nota2 = "";
                flag = true;
            }
            catch (IBException bException)
            {
                WebUtils.MessageBootstrap(this, bException.IBMessage, null);
            }
            catch (Exception exception)
            {
                WebUtils.MessageBootstrap(this, exception.Message, null);
            }
            if (flag)
            {
                this.Context.Items.Add("TipoTransaccionGenerica", this.tg);
                base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(string.Concat("~/pages/IB/Favoritos/MenuFavoritos.aspx?sCod=", base.sCod));
        }

        private void CamposServicio()
        {
            if (!this.ddlTipoFavoritos.SelectedValue.Equals("0"))
            {
                StringBuilder stringBuilder = new StringBuilder();
                this.panelBotones.Visible = true;
                TipoFavorito tipoFavorito = this.ddlTipoFavoritos.getTipoFavorito();
                string descripcion = tipoFavorito.Descripcion;
                string str = "";
                string empty = string.Empty;
                int num = 0;
                bool accion = false;
                if (this.tg != null)
                {
                    accion = ((GAfiliacionFavorito)this.tg.ObjetoTransaccion).Accion == EnumAccionAddUpdateAfiliadoFavoritos.Update;
                }
                EnumTipoFavorito enumTipoFavorito = tipoFavorito.EnumTipoFavorito;
                if (enumTipoFavorito <= EnumTipoFavorito.TransferenciaOtrosBancosTerceros)
                {
                    switch (enumTipoFavorito)
                    {
                        case EnumTipoFavorito.PagoServicioElectricidadCaracas:
                            {
                                TableRowCollection rows = this.tblDatos.Rows;
                                string str1 = string.Concat(tipoFavorito.Titulo, ":");
                                Control[] controlArray = new Control[] { WebUtils.CreateTextBox("txtNumeroContrato", 13, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.numeroinstrumento, accion, "form-control input-sm txtNumeroContrato") };
                                rows.Add(WebUtils.BuildTableRow(str1, controlArray, ""));
                                TableRowCollection tableRowCollections = this.tblDatos.Rows;
                                Control[] controlArray1 = new Control[] { WebUtils.CreateTextBox("txtBeneficiario", 35, this.beneficiario, false, "form-control input-sm") };
                                tableRowCollections.Add(WebUtils.BuildTableRow("Beneficiario:", controlArray1, ""));
                                TableRowCollection rows1 = this.tblDatos.Rows;
                                Control[] controlArray2 = new Control[] { this.CreateComboTipoCedula("ddlTipoCedula", this.tipocedula, accion, "form-control input-sm ddlTipoCedula"), WebUtils.CreateTextBox("txtCedula", 9, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.cedula, accion, "form-control input-sm txtCedula") };
                                rows1.Add(WebUtils.BuildTableRow("Cédula/RIF Beneficiario:", controlArray2, ""));
                                this.AddDescripcionEmail();
                                return;
                            }
                        case EnumTipoFavorito.PagoServicioCANTV:
                        case EnumTipoFavorito.PagoServicioCANTVNET:
                        case EnumTipoFavorito.PagoServicioMovilnet:
                            {
                                this.servicio = true;
                                if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoServicioCANTV ? false : tipoFavorito.EnumTipoFavorito != EnumTipoFavorito.PagoServicioMovilnet))
                                {
                                    TableRowCollection rows2 = this.tblDatos.Rows;
                                    string str3 = string.Concat(tipoFavorito.Titulo, ":");
                                    Control[] controlArray4 = new Control[] { WebUtils.CreateTextBox("txtNumeroContrato", 20, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.numeroinstrumento, accion, "form-control input-sm txtNumeroContrato") };
                                    rows2.Add(WebUtils.BuildTableRow(str3, controlArray4, ""));
                                    this.AddDescripcionEmail();
                                    return;
                                }
                                else
                                {
                                    TableRowCollection tableRowCollections1 = this.tblDatos.Rows;
                                    string str2 = string.Concat(tipoFavorito.Titulo, ":");
                                    Control[] controlArray3 = new Control[] { this.CreateComboCodigoAreaTelefono("ddlArea", this.codarea, tipoFavorito.TipoFavoritoID, accion, "form-control input-sm ddlArea"), WebUtils.CreateTextBox("txtTelefono", 7, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.numeroinstrumento, accion, "form-control input-sm txtTelefono") };
                                    tableRowCollections1.Add(WebUtils.BuildTableRow(str2, controlArray3, ""));
                                    this.AddDescripcionEmail();
                                    return;
                                }
                            }
                        case EnumTipoFavorito.PagoServicioElectricidadCaracas | EnumTipoFavorito.PagoServicioCANTV:
                            {
                                this.AddDescripcionEmail();
                                return;
                            }
                        default:
                            {
                                if (enumTipoFavorito != EnumTipoFavorito.TransferenciaTercerosBAV)
                                {
                                    if ((int)enumTipoFavorito - (int)EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular > (int)EnumTipoFavorito.PagoServicioElectricidadCaracas)
                                    {
                                        this.AddDescripcionEmail();
                                        return;
                                    }
                                }
                                break;
                            }
                    }
                }
                else
                {
                    if ((enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV || enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
                    {
                        goto Label2;
                    }
                    this.AddDescripcionEmail();
                    return;
                }
                Label2:
                this.servicio = false;
                if (tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaTercerosBAV)
                {
                    TableRowCollection tableRowCollections2 = this.tblDatos.Rows;
                    Control[] controlArray5 = new Control[] { WebUtils.CreateTextBox("txtNumeroCuenta", 20, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.numeroinstrumento, accion, "form-control input-sm txtNumeroCuenta") };
                    tableRowCollections2.Add(WebUtils.BuildTableRow("Número de Cuenta:", controlArray5, ""));
                }
                if (tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV)
                {
                    TableRowCollection rows3 = this.tblDatos.Rows;
                    Control[] controlArray6 = new Control[] { WebUtils.CreateTextBox("txtNumeroTarjeta", 16, "onKeypress", "return OnlyNumeric(event,this.value);", this.numeroinstrumento, accion, "form-control input-sm txtNumeroTarjeta") };
                    rows3.Add(WebUtils.BuildTableRow("Número de Tarjeta:", controlArray6, ""));
                }
                if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular || tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosTerceros || tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
                {
                    TableRowCollection tableRowCollections3 = this.tblDatos.Rows;
                    Control[] controlArray7 = new Control[] { this.CreateComboBanco("ddlBanco", this.banco, accion, "form-control input-sm ddlBanco") };
                    tableRowCollections3.Add(WebUtils.BuildTableRow("Banco:", controlArray7, ""));
                    if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
                    {
                        TableRowCollection rows4 = this.tblDatos.Rows;
                        Control[] controlArray8 = new Control[] { this.CreateComboTipoTarjeta("ddlTipoTarjeta", this.tipotarjeta, accion, "form-control input-sm ddlTipoTarjeta") };
                        rows4.Add(WebUtils.BuildTableRow("Tipo tarjeta crédito:", controlArray8, ""));
                    }
                    if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular ? false : tipoFavorito.EnumTipoFavorito != EnumTipoFavorito.TransferenciaOtrosBancosTerceros))
                    {
                        empty = "Número de Tarjeta";
                        str = "txtNumeroTarjeta";
                        num = 16;
                    }
                    else
                    {
                        empty = "Número de Cuenta";
                        str = "txtNumeroCuenta";
                        num = 20;
                    }
                    TableRowCollection tableRowCollections4 = this.tblDatos.Rows;
                    string str4 = string.Concat(empty, ":");
                    Control[] controlArray9 = new Control[] { WebUtils.CreateTextBox(str, num, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.numeroinstrumento, accion, string.Concat("form-control input-sm ", str)) };
                    tableRowCollections4.Add(WebUtils.BuildTableRow(str4, controlArray9, ""));
                }
                TableRowCollection rows5 = this.tblDatos.Rows;
                Control[] controlArray10 = new Control[] { this.CreateComboTipoCedula("ddlTipoCedula", this.tipocedula, accion, "form-control input-sm ddlTipoCedula"), WebUtils.CreateTextBox("txtCedula", 9, "OnKeyPress", "return OnlyNumeric(event, this.value);", this.cedula, accion, "form-control input-sm txtCedula") };
                rows5.Add(WebUtils.BuildTableRow("Cédula/RIF Beneficiario:", controlArray10, ""));
                TableRowCollection tableRowCollections5 = this.tblDatos.Rows;
                Control[] controlArray11 = new Control[] { WebUtils.CreateTextBox("txtBeneficiario", 35, this.beneficiario, false, "form-control input-sm txtBeneficiario") };
                tableRowCollections5.Add(WebUtils.BuildTableRow("Beneficiario:", controlArray11, ""));
                if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular ? true : tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular))
                {
                    DropDownList control = WebUtils.GetControl(this.tblDatos, "ddlTipoCedula") as DropDownList;
                    ListItem listItem = control.Items.FindByValue(base.Afiliado.sAF_Rif.Substring(0, 1));
                    if (listItem != null)
                    {
                        listItem.Selected = true;
                    }
                    control.Enabled = false;
                    TextBox afiliado = WebUtils.GetControl(this.tblDatos, "txtCedula") as TextBox;
                    afiliado.Text = base.Afiliado.sCedula;
                    afiliado.ReadOnly = true;
                    afiliado = WebUtils.GetControl(this.tblDatos, "txtBeneficiario") as TextBox;
                    afiliado.Text = base.Afiliado.sCO_Nombres;
                    afiliado.ReadOnly = true;
                }
                this.AddDescripcionEmail();
            }
            else
            {
                WebUtils.MessageBox2005(this, "Debe seleccionar un tipo de favorito");
            }
        }

        private DropDownList CreateComboBanco(string id, string value, bool isReadOnly, string css)
        {
            TipoFavorito tipoFavorito = this.ddlTipoFavoritos.getTipoFavorito();
            DropDownList dropDownList = new DropDownList()
            {
                ID = id,
                CssClass = css
            };
            List<Banco> bancos = new List<Banco>();
            EnumTipoFavorito enumTipoFavorito = tipoFavorito.EnumTipoFavorito;
            if ((int)enumTipoFavorito - (int)EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular <= (int)EnumTipoFavorito.PagoServicioElectricidadCaracas)
            {
                bancos = HelperGlobal.BankGetByType(false, IBBAVConfiguration.TRANSFERENCIA_OTROS_BANCOS);
            }
            else if ((enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
            {
                bancos = HelperGlobal.BankGetByType(false, IBBAVConfiguration.PAGO_TARJETA_OTROS_BANCOS);
            }
            dropDownList.Items.Add(new ListItem("Seleccione", "-1"));
            foreach (Banco banco in bancos)
            {
                dropDownList.Items.Add(new ListItem(banco.BANK_Nombre, banco.BANK_ID));
            }
            if (value != string.Empty)
            {
                ListItem listItem = dropDownList.Items.FindByValue(value);
                if (listItem != null)
                {
                    listItem.Selected = true;
                }
            }
            dropDownList.Enabled = !isReadOnly;
            return dropDownList;
        }

        private DropDownList CreateComboCodigoAreaTelefono(string id, string value, int servicioId, bool isReadOnly, string css)
        {
            DropDownList dropDownList = new DropDownList()
            {
                ID = id,
                CssClass = css
            };
            IDictionaryEnumerator enumerator = HelperServicios.BDI_CodigoAreaGetBySE_ServicioId(servicioId).GetEnumerator();
            while (enumerator.MoveNext())
            {
                ListItem listItem = new ListItem(enumerator.Key.ToString(), enumerator.Value.ToString());
                dropDownList.Items.Add(listItem);
            }
            if (value != string.Empty)
            {
                ListItem listItem1 = dropDownList.Items.FindByValue(value);
                if (listItem1 != null)
                {
                    listItem1.Selected = true;
                }
            }
            dropDownList.Enabled = !isReadOnly;
            return dropDownList;
        }

        private DropDownList CreateComboTipoCedula(string id, string value, bool isReadOnly, string css)
        {
            DropDownList dropDownList = new DropDownList()
            {
                ID = id,
                CssClass = css
            };
            dropDownList.Items.Clear();
            dropDownList.Items.Add(new ListItem("-", "-1"));
            dropDownList.Items.Add(new ListItem("V", "V"));
            dropDownList.Items.Add(new ListItem("E", "E"));
            dropDownList.Items.Add(new ListItem("J", "J"));
            dropDownList.Items.Add(new ListItem("G", "G"));
            dropDownList.Items.Add(new ListItem("P", "P"));
            if (value != string.Empty)
            {
                ListItem listItem = dropDownList.Items.FindByValue(value);
                if (listItem != null)
                {
                    listItem.Selected = true;
                }
            }
            dropDownList.Enabled = !isReadOnly;
            return dropDownList;
        }

        private DropDownList CreateComboTipoTarjeta(string id, string value, bool isReadOnly, string css)
        {
            DropDownList dropDownList = new DropDownList()
            {
                ID = id,
                CssClass = css
            };
            dropDownList.Items.Clear();
            dropDownList.Items.Add(new ListItem("Selecciona Tipo de Tarjeta", "0"));
            dropDownList.Items.Add(new ListItem("VISA", "VISA"));
            dropDownList.Items.Add(new ListItem("MASTERCARD", "MASTERCARD"));
            dropDownList.Items.Add(new ListItem("DINERS CLUB", "DINERS CLUB"));
            dropDownList.Items.Add(new ListItem("AMERICAN EXPRESS", "AMERICAN EXPRESS"));
            if (value != string.Empty)
            {
                ListItem listItem = dropDownList.Items.FindByValue(value);
                if (listItem != null)
                {
                    listItem.Selected = true;
                }
            }
            dropDownList.Enabled = !isReadOnly;
            return dropDownList;
        }

        protected void ddlTipoFavoritos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelBotones.Visible = false;
            if (!this.ddlTipoFavoritos.SelectedValue.Equals("0"))
            {
                this.numeroinstrumento = string.Empty;
                this.tipocedula = string.Empty;
                this.cedula = string.Empty;
                this.codarea = string.Empty;
                this.beneficiario = string.Empty;
                this.descripcion = string.Empty;
                this.email = string.Empty;
                this.CamposServicio();
                this.panelBotones.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblDatos.EnableViewState = false;
            if (!base.IsPostBack)
            {
                try
                {
                    this.ddlTipoFavoritos.HasTextoInicial = true;
                    this.ddlTipoFavoritos.TextoInicial = "Selecciona el tipo de favorito que deseas registrar";
                    this.ddlTipoFavoritos.TipoCombo = TipoCombo.TiposFavoritos;
                    if (this.Context.Items["TipoTransaccionGenerica"] != null)
                    {
                        this.tg = this.Context.Items["TipoTransaccionGenerica"] as TipoTransaccionGenerica;
                        if (this.tg != null)
                        {
                            GAfiliacionFavorito objetoTransaccion = (GAfiliacionFavorito)this.tg.ObjetoTransaccion;
                            this.ddlTipoFavoritos.SetValue = objetoTransaccion.AfiliadoFavorito.Key;
                        }
                    }
                    this.ddlTipoFavoritos.BindCombo();
                }
                catch (IBException bException)
                {
                    WebUtils.MessageBox2005(this, bException.IBMessage);
                    return;
                }
            }
            if (this.tg != null)
            {
                GAfiliacionFavorito gAfiliacionFavorito = (GAfiliacionFavorito)this.tg.ObjetoTransaccion;
                this.ddlTipoFavoritos.Enabled = gAfiliacionFavorito.Accion != EnumAccionAddUpdateAfiliadoFavoritos.Update;
                Type type = typeof(EnumTipoFavorito);
                int tipoFavoritoId = gAfiliacionFavorito.AfiliadoFavorito.TipoFavoritoId;
                EnumTipoFavorito enumTipoFavorito = (EnumTipoFavorito)Enum.Parse(type, tipoFavoritoId.ToString());
                if ((enumTipoFavorito == EnumTipoFavorito.PagoServicioCANTV ? false : enumTipoFavorito != EnumTipoFavorito.PagoServicioMovilnet))
                {
                    this.numeroinstrumento = gAfiliacionFavorito.AfiliadoFavorito.NumeroInstrumento;
                }
                else
                {
                    this.codarea = gAfiliacionFavorito.AfiliadoFavorito.NumeroInstrumento.Substring(0, 4);
                    this.numeroinstrumento = gAfiliacionFavorito.AfiliadoFavorito.NumeroInstrumento.Substring(4, gAfiliacionFavorito.AfiliadoFavorito.NumeroInstrumento.Length - 4);
                }
                if ((enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular || enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros || enumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular ? true : enumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosTerceros))
                {
                    this.banco = gAfiliacionFavorito.AfiliadoFavorito.BankId;
                    if (gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Length > 0)
                    {
                        this.tipocedula = gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Substring(0, 1);
                        this.cedula = gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Substring(1, gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Length - 1);
                    }
                    this.tipotarjeta = gAfiliacionFavorito.AfiliadoFavorito.TipoTarjetaCredito;
                    this.beneficiario = gAfiliacionFavorito.AfiliadoFavorito.Beneficiario;
                }
                if (enumTipoFavorito == EnumTipoFavorito.PagoServicioElectricidadCaracas)
                {
                    this.tipocedula = gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Substring(0, 1);
                    this.cedula = gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Substring(1, gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Length - 1);
                    this.beneficiario = gAfiliacionFavorito.AfiliadoFavorito.Beneficiario;
                }
                if ((enumTipoFavorito == EnumTipoFavorito.TransferenciaTercerosBAV ? true : enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV))
                {
                    this.tipocedula = gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Substring(0, 1);
                    this.cedula = gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Substring(1, gAfiliacionFavorito.AfiliadoFavorito.CedulaRif.Length - 1);
                    this.beneficiario = gAfiliacionFavorito.AfiliadoFavorito.Beneficiario;
                }
                this.descripcion = gAfiliacionFavorito.AfiliadoFavorito.Descripcion;
                this.email = gAfiliacionFavorito.AfiliadoFavorito.Email;
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$ddlBanco"] != null)
            {
                this.banco = base.Request.Form["ctl00$ContentPlaceHolder1$ddlBanco"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$ddlTipoTarjeta"] != null)
            {
                this.tipotarjeta = base.Request.Form["ctl00$ContentPlaceHolder1$ddlTipoTarjeta"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtTelefono"] != null)
            {
                this.numeroinstrumento = base.Request.Form["ctl00$ContentPlaceHolder1$txtTelefono"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtNumeroContrato"] != null)
            {
                this.numeroinstrumento = base.Request.Form["ctl00$ContentPlaceHolder1$txtNumeroContrato"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtNumeroCuenta"] != null)
            {
                this.numeroinstrumento = base.Request.Form["ctl00$ContentPlaceHolder1$txtNumeroCuenta"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtNumeroTarjeta"] != null)
            {
                this.numeroinstrumento = base.Request.Form["ctl00$ContentPlaceHolder1$txtNumeroTarjeta"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$ddlTipoCedula"] != null)
            {
                this.tipocedula = base.Request.Form["ctl00$ContentPlaceHolder1$ddlTipoCedula"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtCedula"] != null)
            {
                this.cedula = base.Request.Form["ctl00$ContentPlaceHolder1$txtCedula"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$ddlArea"] != null)
            {
                this.codarea = base.Request.Form["ctl00$ContentPlaceHolder1$ddlArea"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtBeneficiario"] != null)
            {
                this.beneficiario = base.Request.Form["ctl00$ContentPlaceHolder1$txtBeneficiario"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtDescripcion"] != null)
            {
                this.descripcion = base.Request.Form["ctl00$ContentPlaceHolder1$txtDescripcion"].ToString();
            }
            if (base.Request.Form["ctl00$ContentPlaceHolder1$txtEmail"] != null)
            {
                this.email = base.Request.Form["ctl00$ContentPlaceHolder1$txtEmail"].ToString();
            }
            if ((base.IsPostBack ? false : this.ddlTipoFavoritos.SelectedIndex > 0))
            {
                this.CamposServicio();
            }
        }

        protected void tblDatos_PreRender(object sender, EventArgs e)
        {
        }

        private void ValidarCampos()
        {
            DropDownList control;
            TipoFavorito tipoFavorito = this.ddlTipoFavoritos.getTipoFavorito();
            this.encabezado = new EncabezadoTransaccion();
            this.encabezado.AddEncabezado("Tipo Favorito", tipoFavorito.Descripcion);
            EnumTipoFavorito enumTipoFavorito = tipoFavorito.EnumTipoFavorito;
            if (enumTipoFavorito <= EnumTipoFavorito.TransferenciaOtrosBancosTerceros)
            {
                switch (enumTipoFavorito)
                {
                    case EnumTipoFavorito.PagoServicioElectricidadCaracas:
                        {
                            if (this.numeroinstrumento.Length == 0)
                            {
                                throw new Exception("El número de cuenta contrato es requerida");
                            }
                            if (this.numeroinstrumento.Length < 13)
                            {
                                throw new Exception("El número de cuenta contrato debe tener 13 números");
                            }
                            if (!this.ValidarNumeroCuentaContratoEDC(this.numeroinstrumento.ToString()))
                            {
                                throw new Exception("Número de Cuenta Contrato Inválido");
                            }
                            this.encabezado.AddEncabezado("Número de Cuenta Contrato", this.numeroinstrumento);
                            control = WebUtils.GetControl(this.tblDatos, "ddlTipoCedula") as DropDownList;
                            if (control.SelectedIndex == 0)
                            {
                                throw new Exception("Debe seleccionar un tipo de cedula");
                            }
                            if (this.cedula.Length == 0)
                            {
                                throw new Exception("La cédula es requerida");
                            }
                            this.cedula = string.Concat(control.SelectedItem.Value, this.cedula);
                            this.encabezado.AddEncabezado("Cédula del beneficiario", this.cedula);
                            if (this.beneficiario.Length == 0)
                            {
                                throw new Exception("El nombre del Beneficiario es requerido");
                            }
                            this.encabezado.AddEncabezado("Nombre del beneficiario", this.beneficiario);
                            if (this.descripcion == string.Empty)
                            {
                                throw new Exception("La descripción es requerida");
                            }
                            this.encabezado.AddEncabezado("Descripción", this.descripcion);
                            if (this.email.Length != 0)
                            {
                                if (!Tools.TestEmailRegex(this.email))
                                {
                                    throw new Exception("El correo electrónico no es válido");
                                }
                                this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                            }
                            break;
                        }
                    case EnumTipoFavorito.PagoServicioCANTV:
                    case EnumTipoFavorito.PagoServicioCANTVNET:
                    case EnumTipoFavorito.PagoServicioMovilnet:
                        {
                            if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoServicioCANTV ? false : tipoFavorito.EnumTipoFavorito != EnumTipoFavorito.PagoServicioMovilnet))
                            {
                                if (this.numeroinstrumento.Length == 0)
                                {
                                    throw new Exception("El número de contrato es requerido");
                                }
                                this.encabezado.AddEncabezado("Número de Contrato", this.numeroinstrumento);
                                if (this.descripcion == string.Empty)
                                {
                                    throw new Exception("La descripción es requerida");
                                }
                                this.encabezado.AddEncabezado("Descripción", this.descripcion);
                                if (this.email.Length != 0)
                                {
                                    if (!Tools.TestEmailRegex(this.email))
                                    {
                                        throw new Exception("El correo electrónico no es válido");
                                    }
                                    this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                                }
                                break;
                            }
                            else
                            {
                                this.numeroinstrumento = string.Concat(this.codarea, this.numeroinstrumento);
                                if (this.numeroinstrumento.Length == 0)
                                {
                                    throw new Exception("El número de teléfono es requerido");
                                }
                                if (this.numeroinstrumento.Length < 10)
                                {
                                    throw new Exception("El número de teléfono debe tener 10 números");
                                }
                                this.encabezado.AddEncabezado("Número de Telefónico", this.numeroinstrumento);
                                if (this.descripcion == string.Empty)
                                {
                                    throw new Exception("La descripción es requerida");
                                }
                                this.encabezado.AddEncabezado("Descripción", this.descripcion);
                                if (this.email.Length != 0)
                                {
                                    if (!Tools.TestEmailRegex(this.email))
                                    {
                                        throw new Exception("El correo electrónico no es válido");
                                    }
                                    this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                                }
                                break;
                            }
                        }
                    case EnumTipoFavorito.PagoServicioElectricidadCaracas | EnumTipoFavorito.PagoServicioCANTV:
                        {
                            if (this.descripcion == string.Empty)
                            {
                                throw new Exception("La descripción es requerida");
                            }
                            this.encabezado.AddEncabezado("Descripción", this.descripcion);
                            if (this.email.Length != 0)
                            {
                                if (!Tools.TestEmailRegex(this.email))
                                {
                                    throw new Exception("El correo electrónico no es válido");
                                }
                                this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                            }
                            break;
                        }
                    default:
                        {
                            if (enumTipoFavorito != EnumTipoFavorito.TransferenciaTercerosBAV)
                            {
                                if ((int)enumTipoFavorito - (int)EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular > (int)EnumTipoFavorito.PagoServicioElectricidadCaracas)
                                {
                                    if (this.descripcion == string.Empty)
                                    {
                                        throw new Exception("La descripción es requerida");
                                    }
                                    this.encabezado.AddEncabezado("Descripción", this.descripcion);
                                    if (this.email.Length != 0)
                                    {
                                        if (!Tools.TestEmailRegex(this.email))
                                        {
                                            throw new Exception("El correo electrónico no es válido");
                                        }
                                        this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                                    }
                                    break;
                                }
                            }
                            goto Label0;
                        }
                }
            }
            else
            {
                if ((enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV || enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : enumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
                {
                    goto Label0;
                }
                if (this.descripcion == string.Empty)
                {
                    throw new Exception("La descripción es requerida");
                }
                this.encabezado.AddEncabezado("Descripción", this.descripcion);
                if (this.email.Length != 0)
                {
                    if (!Tools.TestEmailRegex(this.email))
                    {
                        throw new Exception("El correo electrónico no es válido");
                    }
                    this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                }
            }
            return;
            Label0:
            if (tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaTercerosBAV)
            {
                if (this.numeroinstrumento.Length == 0)
                {
                    throw new Exception("El número de cuenta es requerida");
                }
                if (this.numeroinstrumento.Length < 20)
                {
                    throw new Exception("El número de cuenta debe tener 20 números");
                }
                if (!this.numeroinstrumento.Substring(0, 4).Equals("0166"))
                {
                    throw new Exception("El número de cuenta no pertenece a la institución seleccionada");
                }
                if (!this.ValidarCuenta(this.numeroinstrumento))
                {
                    throw new Exception("El número de cuenta no es válido");
                }
                this.encabezado.AddEncabezado("Número de Cuenta", this.numeroinstrumento);
            }
            if (tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV)
            {
                if (this.numeroinstrumento.Length == 0)
                {
                    throw new Exception("El número de tarjeta es requerida");
                }
                if (this.numeroinstrumento.Length < 16)
                {
                    throw new Exception("El número de tarjeta debe tener 16 números");
                }
                if (this.ValidarTarjeta(this.numeroinstrumento, "0166") > 0)
                {
                    throw new Exception("El número de tarjeta no es válido");
                }
                this.encabezado.AddEncabezado("Número de Tarjeta", this.numeroinstrumento);
            }
            if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular || tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosTerceros || tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
            {
                control = WebUtils.GetControl(this.tblDatos, "ddlBanco") as DropDownList;
                if (control.SelectedIndex == 0)
                {
                    throw new Exception("Debe seleccionar un Banco");
                }
                this.banco = control.SelectedItem.Value;
                this.encabezado.AddEncabezado("Banco", control.SelectedItem.Text);
                if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosMismoTitular ? true : tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoOtrosBancosTerceros))
                {
                    control = WebUtils.GetControl(this.tblDatos, "ddlTipoTarjeta") as DropDownList;
                    if (control.SelectedIndex == 0)
                    {
                        throw new Exception("Debe seleccionar un Tipo de Tarjeta");
                    }
                    this.tipotarjeta = control.SelectedItem.Value;
                    this.encabezado.AddEncabezado("Tipo Tarjeta", this.tipotarjeta);
                }
                if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaOtrosBancosMismoTitular ? false : tipoFavorito.EnumTipoFavorito != EnumTipoFavorito.TransferenciaOtrosBancosTerceros))
                {
                    if (this.numeroinstrumento.Length == 0)
                    {
                        throw new Exception("El número de tarjeta es requerida");
                    }
                    if (this.numeroinstrumento.Length < 14)
                    {
                        throw new Exception("El número de tarjeta debe tener al menos 14 números");
                    }
                    if (this.ValidarTarjeta(this.numeroinstrumento, this.banco) > 0)
                    {
                        throw new Exception("El número de tarjeta no es válido");
                    }
                    this.encabezado.AddEncabezado("Número de Tarjeta", this.numeroinstrumento);
                }
                else
                {
                    if (this.numeroinstrumento.Length == 0)
                    {
                        throw new Exception("El número de cuenta es requerida");
                    }
                    if (this.numeroinstrumento.Length < 20)
                    {
                        throw new Exception("El número de cuenta debe tener 20 números");
                    }
                    if (!this.numeroinstrumento.Substring(0, 4).Equals(this.banco))
                    {
                        throw new Exception("El número de cuenta no pertenece a la institución seleccionada");
                    }
                    if (!this.ValidarCuenta(this.numeroinstrumento))
                    {
                        throw new Exception("El número de cuenta no es válido");
                    }
                    this.encabezado.AddEncabezado("Número de Cuenta", this.numeroinstrumento);
                }
            }
            control = WebUtils.GetControl(this.tblDatos, "ddlTipoCedula") as DropDownList;
            if (control.SelectedIndex == 0)
            {
                throw new Exception("Debe seleccionar un tipo de cédula");
            }
            if (this.cedula.Length == 0)
            {
                throw new Exception("La cédula es requerida");
            }
            this.cedula = string.Concat(control.SelectedItem.Value, this.cedula);
            this.encabezado.AddEncabezado("Cédula del beneficiario", this.cedula);
            if ((tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.TransferenciaTercerosBAV ? true : tipoFavorito.EnumTipoFavorito == EnumTipoFavorito.PagoTarjetaCreditoTercerosBAV))
            {
                try
                {
                    RespuestaValctadsjv respuestaValctadsjv = HelperIbs.ibsValCtaBAV(this.numeroinstrumento);
                    if (!string.Concat(respuestaValctadsjv.validarctabavdsjv.cedRif.ToString().Substring(0, 1), Convert.ToInt32(respuestaValctadsjv.validarctabavdsjv.cedRif.ToString().Substring(1)).ToString()).Equals(this.cedula))
                    {
                        throw new Exception("El Número de Cuenta / Tarjeta  a registrar no pertenece al Beneficiario, por favor verifique los datos");
                    }
                }
                catch (IBException bException)
                {
                    throw new Exception(bException.IBMessage);
                }
            }
            if (this.beneficiario.Length == 0)
            {
                throw new Exception("El nombre del Beneficiario es requerido");
            }
            this.encabezado.AddEncabezado("Nombre del beneficiario", this.beneficiario);
            if (this.descripcion == string.Empty)
            {
                throw new Exception("La descripción es requerida");
            }
            this.encabezado.AddEncabezado("Descripción", this.descripcion);
            if (this.email.Length != 0)
            {
                if (!Tools.TestEmailRegex(this.email))
                {
                    throw new Exception("El correo electrónico no es válido");
                }
                this.encabezado.AddEncabezado("Correo Electrónico", this.email);
                return;
            }
            else
            {
                return;
            }
        }

        public bool ValidarCuenta(string codigoCuentaCliente)
        {
            bool flag = true;
            string str = codigoCuentaCliente.Trim();
            if (str.Length < 20)
            {
                flag = false;
            }
            else
            {
                string str1 = str.Substring(0, 4);
                string str2 = str.Substring(4, 4);
                string str3 = str.Substring(8, 1);
                string str4 = str.Substring(9, 1);
                string str5 = str.Substring(10, 10);
                string str6 = string.Concat(str1, str2);
                int num = int.Parse(str6.Substring(0, 1)) * 3;
                int num1 = int.Parse(str6.Substring(1, 1)) * 2;
                int num2 = int.Parse(str6.Substring(2, 1)) * 7;
                int num3 = int.Parse(str6.Substring(3, 1)) * 6;
                int num4 = int.Parse(str6.Substring(4, 1)) * 5;
                int num5 = int.Parse(str6.Substring(5, 1)) * 4;
                int num6 = int.Parse(str6.Substring(6, 1)) * 3;
                int num7 = int.Parse(str6.Substring(7, 1)) * 2;
                int num8 = num + num1 + num2 + num3 + num4 + num5 + num6 + num7;
                int num9 = num8 / 11;
                int num10 = 11 - num8 % 11;
                string str7 = "";
                if (num10 < 10)
                {
                    str7 = num10.ToString();
                }
                if (num10 == 10)
                {
                    str7 = "0";
                }
                if (num10 == 11)
                {
                    str7 = "1";
                }
                if (str7 != str3)
                {
                    flag = false;
                }
                if (flag)
                {
                    str6 = string.Concat(str2, str5);
                    num = int.Parse(str6.Substring(0, 1)) * 3;
                    num1 = int.Parse(str6.Substring(1, 1)) * 2;
                    num2 = int.Parse(str6.Substring(2, 1)) * 7;
                    num3 = int.Parse(str6.Substring(3, 1)) * 6;
                    num4 = int.Parse(str6.Substring(4, 1)) * 5;
                    num5 = int.Parse(str6.Substring(5, 1)) * 4;
                    num6 = int.Parse(str6.Substring(6, 1)) * 3;
                    num7 = int.Parse(str6.Substring(7, 1)) * 2;
                    int num11 = int.Parse(str6.Substring(8, 1)) * 7;
                    int num12 = int.Parse(str6.Substring(9, 1)) * 6;
                    int num13 = int.Parse(str6.Substring(10, 1)) * 5;
                    int num14 = int.Parse(str6.Substring(11, 1)) * 4;
                    int num15 = int.Parse(str6.Substring(12, 1)) * 3;
                    int num16 = int.Parse(str6.Substring(13, 1)) * 2;
                    num8 = num + num1 + num2 + num3 + num4 + num5 + num6 + num7 + num11 + num12 + num13 + num14 + num15 + num16;
                    int num17 = num8 / 11;
                    num10 = 11 - num8 % 11;
                    string str8 = "";
                    if (num10 < 10)
                    {
                        str8 = num10.ToString();
                    }
                    if (num10 == 10)
                    {
                        str8 = "0";
                    }
                    if (num10 == 11)
                    {
                        str8 = "1";
                    }
                    if (str8 != str4)
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        private bool ValidarNumeroCuentaContratoEDC(string sNroCuentaContrato)
        {
            bool flag;
            int num = 0;
            int num1 = 7;
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; i < 12; i++)
            {
                num = num + int.Parse(sNroCuentaContrato.Substring(i, 1)) * num1;
                num1--;
                if (num1 == 1)
                {
                    num1 = 7;
                }
            }
            num2 = num % 11;
            if (num2 == 0)
            {
                num3 = 1;
            }
            else
            {
                num3 = (num2 != 1 ? 11 - num2 : 0);
            }
            flag = (int.Parse(sNroCuentaContrato.Substring(12, 1)) == num3 ? true : false);
            return flag;
        }

        public int ValidarTarjeta(string Tarjeta, string codBanco)
        {
            int num = 0;
            string str = "";
            char chr = '0';
            int num1 = 0;
            string str1 = "212121212121212";
            string tarjeta = Tarjeta;
            int length = tarjeta.Length;
            int num2 = 0;
            int num3 = 0;
            if ((tarjeta.Length >= 16 ? false : tarjeta.Length < 16))
            {
                if (Tarjeta.Substring(0, 4).Equals("3770"))
                {
                    tarjeta = tarjeta.PadLeft(16, chr);
                }
                else if (!"0105".Equals(codBanco))
                {
                    num = 102;
                }
                else if (tarjeta.Length < 14)
                {
                    num = 102;
                }
            }
            if (num == 0)
            {
                length = tarjeta.Length - 1;
                str = tarjeta.Substring(0, length);
                num1 = Convert.ToInt32(tarjeta.Substring(length, 1));
                length = str.Length;
                int num5 = 0;
                int num6 = 0;
                while (num5 < length)
                {
                    string str2 = str.Substring(num5, 1);
                    string str3 = str1.Substring(num5, 1);
                    num2 = Convert.ToInt32(str2) * Convert.ToInt32(str3);
                    if (num2 > 9)
                    {
                        num2 -= 9;
                    }
                    num6 += num2;
                    num5++;
                }
                num3 = num6 % 10;
                if (num1 != (num3 == 0 ? num3 : 10 - num3))
                {
                    num = 102;
                }
            }
            return num;
        }
    }
}