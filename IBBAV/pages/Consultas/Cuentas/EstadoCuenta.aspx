<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="EstadoCuenta.aspx.cs" Inherits="IBBAV.pages.Consultas.Cuentas.EstadoCuenta" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.theme.css") %>" />

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/ui-lightness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/smoothness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.css") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="well">
            Tu estado de cuenta mes a mes y listo para imprimir
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="form-group">
                    <label for="cuenta" class="col-md-3 control-label">Seleccione una Cuenta:</label>
                    <div class="col-md-4">
                        <cc1:DropDownListCuentas ID="ddlCuenta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCuenta_SelectedIndexChanged" CssClass="form-control input-sm"></cc1:DropDownListCuentas>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>    
</asp:Content>
