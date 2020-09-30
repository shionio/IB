<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="consolidada.aspx.cs" Inherits="IBBAV.pages.consolidada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <asp:Panel ID="pnlCuentas" runat="server" Visible="false" CssClass="panel panel-primary">
        <!-- Default panel contents -->        
        <div class="panel-heading">Saldo de Cuentas al <asp:Literal ID="liFechaCuenta" runat="server"></asp:Literal></div>
        <div class="panel-body">
            <!-- Table -->            
            <table class="table table-striped">                 
                <asp:Repeater ID="dtgCuentas" runat="server" OnItemDataBound="dtgCuentas_ItemDataBound">
                    <HeaderTemplate>
                        <thead>
                                <th>Número de Cuenta</th>
                                <th>Tipo</th>
                                <th style="text-align: right">Saldo Total Bs.S.</th>
                                <th style="text-align: right">Saldo Disponible Bs.S.</th>
                                <!-- Agregado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <th style="text-align: right">Saldo Disponible PETRO.</th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><asp:HyperLink id="lnkCuenta" runat="server"></asp:HyperLink></td>
                                <td><%#Eval("SDescipcionproducto") %></td>
                                <td style="text-align: right"><asp:Literal ID="liContable" runat="server"></asp:Literal></td>
                                <td style="text-align: right"><asp:Literal ID="liDisponible" runat="server"></asp:Literal></td>
                                <!-- Agregado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <td style="text-align: right"><asp:Literal ID="liPetro" runat="server"></asp:Literal></td>                                
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTarjetas" runat="server" Visible="false" CssClass="panel panel-primary">
        <!-- Default panel contents -->
        <div class="panel-heading">Saldo de Tarjetas al <asp:Literal ID="liFechaTarjetas" runat="server"></asp:Literal></div>
        <div class="panel-body">
            <!-- Table -->
            <table class="table table-striped">
                <asp:Repeater ID="rptTarjetas" runat="server" OnItemDataBound="rptTarjetas_ItemDataBound">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th>Número de Tarjeta</th>
                                <th>Tipo</th>
                                <th style="text-align: right">Saldo Total Bs.S.</th>
                                <th style="text-align: right">Saldo Disponible Bs.S.</th>
                                <!-- Modificado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <th style="text-align: right">Saldo Disponible PETRO.</th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><asp:HyperLink id="lnkCuenta" runat="server"></asp:HyperLink></td>
                                <td><%#Eval("SDescipcionproducto") %></td>
                                <td style="text-align: right"><asp:Literal ID="liContable" runat="server"></asp:Literal></td>
                                <td style="text-align: right"><asp:Literal ID="liDisponible" runat="server"></asp:Literal></td>
								<!-- Agregado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <td style="text-align: right"><asp:Literal ID="liPetro" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDepositos" runat="server" Visible="false" CssClass="panel panel-primary">
        <!-- Default panel contents -->
        <div class="panel-heading">Depositos al <asp:Literal ID="liFechaDepositos" runat="server"></asp:Literal></div>
        <div class="panel-body">
            <!-- Table -->
            <table class="table table-striped">
                <asp:Repeater ID="rptDepositos" runat="server" OnItemDataBound="rptDepositos_ItemDataBound">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th>Número de Cuenta</th>
                                <th>Tipo</th>
                                <th style="text-align: right">Saldo Total Bs.S.</th>
                                <th style="text-align: right">Saldo Disponible Bs.S.</th>
                                <!-- Modificado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <th style="text-align: right">Saldo Disponible PETRO.</th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><asp:HyperLink id="lnkCuenta" runat="server"></asp:HyperLink></td>
                                <td><%#Eval("SDescipcionproducto") %></td>
                                <td style="text-align: right"><asp:Literal ID="liContable" runat="server"></asp:Literal></td>
                                <td style="text-align: right"><asp:Literal ID="liDisponible" runat="server"></asp:Literal></td>
								<!-- Agregado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <td style="text-align: right"><asp:Literal ID="liPetro" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlCreditos" runat="server" Visible="false" CssClass="panel panel-primary">
        <!-- Default panel contents -->
        <div class="panel-heading">Creditos al <asp:Literal ID="liFechaCreditos" runat="server"></asp:Literal></div>
        <div class="panel-body">
            <!-- Table -->
            <table class="table table-striped">
                <asp:Repeater ID="rptCreditos" runat="server" OnItemDataBound="rptCreditos_ItemDataBound">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th>Número de Cuenta</th>
                                <th>Tipo</th>
                                <th style="text-align: right">Saldo Total Bs.S.</th>
                                <th style="text-align: right">Saldo Disponible Bs.S.</th>
                                <!-- Modificado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <th style="text-align: right">Saldo Disponible PETRO.</th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td><asp:HyperLink id="lnkCuenta" runat="server"></asp:HyperLink></td>
                                <td><%#Eval("SDescipcionproducto") %></td>
                                <td style="text-align: right"><asp:Literal ID="liContable" runat="server"></asp:Literal></td>
                                <td style="text-align: right"><asp:Literal ID="liDisponible" runat="server"></asp:Literal></td>
								<!-- Agregado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
                                <td style="text-align: right"><asp:Literal ID="liPetro" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
