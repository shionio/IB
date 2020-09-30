<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="Hidrocapital.aspx.cs" Inherits="IBBAV.pages.Pagos.Servicios.Hidrocapital" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CalcularMonto() {
            var total = 0.0;
            $(".sel:checked").each(function (i, item) {
                var m = $(item).data("monto");
                total += parseFloat(m);
            });
            $("#spTotal").html(total);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid">
        <div class="well">
            
        </div>   
        <table class="table table-responsive table-bav">
            <tr>
                <td class="fieldTxt">Cuenta a Debitar:</td>
                <td class="fieldVal">
                    <cc1:DropDownListCuentas ID="ddlCtaDebitar" runat="server" CssClass="form-control cd">
                    </cc1:DropDownListCuentas>
                </td>
                <td class="fieldInf"></td>
            </tr>
            <tr>
                <td class="fieldTxt">Seleccione un Contrato:</td>
                <td class="fieldVal">
                    <cc1:DropDownListCuentas ID="ddlCtaAcreditar" runat="server" CssClass="form-control ca" OnSelectedIndexChanged="ddlCtaAcreditar_SelectedIndexChanged" AutoPostBack="true"></cc1:DropDownListCuentas>
                </td>
                <td class="fieldInf"></td>
            </tr>
            <tr id="trResultadoNombre" runat="server" visible="false">
                <td class="fieldTxt">Nombre Cliente:</td>
                <td class="fieldVal"><asp:Literal ID="liNombreCliente" runat="server"></asp:Literal></td>
                <td class="fieldInf"></td>
            </tr>
        </table>
        <asp:Panel ID="panelRecibos" runat="server" Visible="false">
            <h5>Recibos Pendientes</h5>
            <table class="table table-responsive">
                <tr>
                    <th></th>
                    <th>Nro. Recibo</th>
                    <th>Fecha</th>
                    <th>Monto</th>
                    
                </tr>
            
            <asp:Repeater ID="rptRecibos" runat="server" OnItemDataBound="rptRecibos_ItemDataBound"  >
                <ItemTemplate>
                    <tr>
                        <td><input id="chkSel" class="sel" type="checkbox" data-monto="<%# Eval("IMPTOTAL") %>" onclick="CalcularMonto()" /></td>
                        <td><%# Eval("REFPAGO") %></td>
                        <td></td>
                        <td><%# Eval("IMPTOTAL") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
                <tr>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th>Total:<span id="spTotal">0,00</span></th>
                </tr>
                </table>
            <div></div>
                <asp:Button ID="BtAceptar" runat="server" Text="Aceptar" CssClass="btn btn-danger"  OnClick="BtAceptar_Click" />
                <asp:Button ID="BtCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="BtCancelar_Click" />
            
        </asp:Panel>
         
    </div>
</asp:Content>
