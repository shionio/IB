<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="DatosFavorito.aspx.cs" Inherits="IBBAV.pages.IB.Favoritos.DatosFavorito" %>

<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.theme.css") %>" />

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/ui-lightness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/smoothness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.css") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>
    <script type='text/javascript'>
        function Validar() {
            var i = $('.form-control');
            i.parent().removeClass('has-error');

            $('#dvMensajes').hide();

            i = $(".ddlBanco"); if (i && i.val() == '-1') { i.parent().addClass('has-error'); i.focus().select(); showAlert('Alerta', 'Debe seleccionar el Banco'); return false; }
            i = $('.ddlTipoTarjeta'); if (i && i.val() == '0') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe seleccionar el Tipo de Tarjeta'); return false; }
            i = $('.txtNumeroCuenta'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Número de Cuenta'); return false; }
            i = $('.txtNumeroTarjeta'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Número de Tarjeta'); return false; }

            i = $('.txtNumeroContrato'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Número de Contrato'); return false; }
            i = $('.ddlTipoCedula'); if (i && i.val() == '0') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Tipo de Cédula / RIF del Beneficiario'); return false; }
            i = $('.txtCedula'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Número de Cédula / RIF del Beneficiario'); return false; }
            i = $('.txtBeneficiario'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Beneficiario'); return false; }
            

            
            i = $('.ddlArea'); if (i && i.val() == '-') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Código de Area'); return false; }
            i = $('.txtTelefono'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar el Número de Teléfono'); return false; }

            i = $('.txtDescripcion'); if (i && i.val() == '') { i.parent().addClass('has-error'); i.focus(); showAlert('Alerta', 'Debe colocar la Descripción'); return false; }
            i = $('.txtEmail');
            if (i) {
                if (i.val() == '') {
                    i.parent().addClass('has-error');
                    i.focus();
                    showAlert('Alerta', 'Debe colocar el Correo electrónico');
                    return false;
                }
                if (!validateEmail(i.val())) {
                    i.parent().addClass('has-error');
                    i.focus();
                    showAlert('Alerta','El formato del Correo electrónico no es válido');
                    return false;
                }
            }

            return true;
        }
        function validateEmail(email) {
            var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
            return re.test(email);
        }

        function doClick() {
            if (Validar())
            {

                console.log("text");
                <%= ClientScript.GetPostBackEventReference(btnAceptar, "")%>;
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="well">
            Seleccione el tipo de servicio o producto que desee agregar a su directorio
        </div>
        <cc1:DropDownListCuentas ID="ddlTipoFavoritos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoFavoritos_SelectedIndexChanged" CssClass="form-control"></cc1:DropDownListCuentas>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="tblDatos" runat="server" CellPadding="1" CellSpacing="1" CssClass="table"></asp:Table>
                <asp:Panel ID="panelBotones" runat="server" Visible="false">
                    <center>
                        <button id="btAceptar" class="btn btn-danger" type="button" onclick="doClick()">
                            Aceptar
                        </button>
                        <asp:LinkButton ID="btnAceptar" Visible="false" runat="server" OnClick="btnAceptar_Click" CssClass="btn btn-danger" OnClientClick="return doClick();" >
                                Aceptar    
                        </asp:LinkButton>
                        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-danger" />
                    </center>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlTipoFavoritos" EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="btnAceptar"/>
                <asp:PostBackTrigger ControlID="btnRegresar" />
            </Triggers>
        </asp:UpdatePanel>
        
    </div>
</asp:Content>
