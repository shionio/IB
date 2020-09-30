<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="ReferenciaCuenta.aspx.cs" Inherits="IBBAV.pages.Servicios.Referencias.Cuenta" %>

<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

    
    <div class="well">
        <span>Genera Referencias Bancarias de tus Cuentas sin dirigirte a la Agencia.</span> <br />
            Tu Referencia Bancaria incluye un número que permite certificar su emisión a través del Centro de Atención Telefónica 0500.XXX.XXXX
    </div>
    
            <asp:UpdatePanel ID="UpdatePanelNombreInstitucion" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <table class="table table-responsive table-bav">
                        <tr>
                            <td class="fieldTxt">Seleccione una Cuenta:</td>
                            <td class="fieldVal">
                                <cc1:DropDownListCuentas ID="ddlCuenta" runat="server"></cc1:DropDownListCuentas></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr>
                            <td class="fieldTxt">Dirigida a:</td>
                            <td class="fieldVal">
                                <asp:DropDownList ID="ddlDirigido" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDirigido_SelectedIndexChanged">
                                    <asp:ListItem Value="0">A quien pueda interesar</asp:ListItem>
                                    <asp:ListItem Value="1">Escribe a quien deseas dirigir la referencia</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fieldInf"></td>
                        </tr>

                        <tr id="trNombreInstitucion" runat="server" visible="False">
                            <td class="fieldTxt">el Nombre ó Institución:</td>
                            <td class="fieldVal">
                                <asp:TextBox ID="txtNombreInstitucion" runat="server"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr>
                            <td colspan="3" class="buttons">
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClientClick="return validar();" OnClick="btnAceptar_Click" />
                                <button id="btnCancelar" class="btn btn-danger">Cancelar</button>

                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAceptar"/>
                    <asp:AsyncPostBackTrigger ControlID="ddlDirigido" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
      </div>  
</asp:Content>
