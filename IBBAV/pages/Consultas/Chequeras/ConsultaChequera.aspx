<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="ConsultaChequera.aspx.cs" Inherits="IBBAV.pages.Consultas.Chequeras.ConsultaChequera" %>

<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panelCombo" Width="100%" Visible="true">
                <br />
                <table class="table table-responsive table-bav">
                    <tr>
                        <td class="fieldTxt">Cuenta:</td>
                        <td class="fieldVal">
                            <cc1:DropDownListCuentas ID="ddlCuenta" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCuenta_SelectedIndexChanged" CssClass="form-control"></cc1:DropDownListCuentas>
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
            <asp:Panel ID="panelChequera" runat="server" Visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Si deseas obtener mayor información sobre los estados de los cheques o tu chequera, haz "Click" sobre los campos subrayados.
                    </div>
                    <div class="panel-body">
                        Chequeras emitidas para la Cuenta Nro.<asp:Literal ID="litCtaCheqra" runat="server"></asp:Literal>
                        <br />
                        <asp:Repeater ID="rptChequeras" runat="server">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <th>Serial</th>
                                        <th>Cheque Inicial</th>
                                        <th>Cheque Final</th>
                                        <th>Cant. de Cheques</th>
                                        <th>Cheques sin Utilizar</th>
                                        <th>Cheques Utilizados</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("SSerial") %></td>
                                    <td><%# Eval("SCheqIni") %></td>
                                    <td><%# Eval("SCheqFin") %></td>
                                    <td><%# Eval("SCantCheq") %></td>
                                    <td><%# Eval("SNumCheqHabil") %></td>
                                    <td><%# Eval("chqUtil") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="panelCheque" runat="server" Visible="false" Width="360px" BorderStyle="None" BorderWidth="0px">
                <table class="table table-responsive table-bav">
                    <tr>
                        <td class="tab_tit" align="center" style="height: 40px; text-align: center">Chequera Nro.<asp:Literal ID="litSerialChq" runat="server"></asp:Literal>
                            de la Cuenta <asp:Literal ID="litCtaCheq" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="rptCheques" runat="server">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>N&#250;mero de Cheque</th>
                                            <th>Estado del Cheque</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="fieldTxt"><%# Eval("id") %></td>
                                        <td class="fieldVal"><%# Eval("status") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnRegresar" runat="server" OnClick="btnRegresar_Click" Text="Regresar" CssClass="btn btn-danger" /></td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCuenta" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnRegresar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
