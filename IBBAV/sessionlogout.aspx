<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMasterSimple.Master" AutoEventWireup="true" CodeBehind="sessionlogout.aspx.cs" Inherits="IBBAV.sessionlogout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-2"></div>
    <div class="col-md-8" >
        <p style="color: #FF0000; font-size: 16pt">
            <b><i class="glyphicon glyphicon-warning-sign" style="float: left; margin-right: 20px; color: #eaaf51; font-size: 52px"></i>¡IMPORTANTE!</b> No puede abrir dos sesiones<br />
            simultáneas de BAV en Línea
        </p>
        <table class="table" style="min-width:600px !important; max-width:800px !important;" >
            <tr>
                <td style="padding-left: 60px; text-align:justify">

                    <br>
                    Estimado cliente, se ha detectado el ingreso a una sesión vencida o no válida en BAV en Línea.<br />
                    <br>
                    <br>
                    Le recordamos que sólo puede estar conectado con en una sesión válida de BAV en Línea.
                    <br>
                    <br>
                    <div class="col-md-6 center-block" style="  width: 50% !important;float: left !important;">
                        <asp:Button ID="btnLogin" CssClass="btn btn-danger" runat="server" OnClick="btnLogin_Click" Text=" Ir a BAV en Línea"></asp:Button>
                    </div>
                    <div class="col-md-6 center-block" style="  width: 50% !important;float: left !important;">
                        <asp:Button ID="btnCerrar" CssClass="btn btn-danger" runat="server" OnClientClick="window.close();return false;" Text="Cerrar ventana"></asp:Button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="col-md-2"></div>

</asp:Content>
