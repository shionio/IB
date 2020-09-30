<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMasterSimple.Master" AutoEventWireup="true" CodeBehind="forcelogout.aspx.cs" Inherits="IBBAV.forcelogout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>
        setInterval(function () {
            if (window.frameElement) {
                window.parent.location = "<%= ResolveUrl("~/forcelogout.aspx")%>";
            }
            else {
                // not in frame
            }
        }, 100);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-2"></div>
    <div class="col-md-8" >
        <p style="color: #FF0000; font-size: 16pt">
            <b><i class="glyphicon glyphicon-warning-sign" style="float: left; margin-right: 20px; color: #eaaf51; font-size: 52px"></i>¡IMPORTANTE!</b> Se han detectado dos sesiones<br />
            activas en BAV en Línea
        </p>
        <table class="table" style="min-width:600px !important; max-width:800px !important;" >
            <tr>
                <td style="padding-left: 60px; text-align:justify">

                    <br>
                    Estimado cliente, se ha detectado el ingreso a otra sesión de BAV en Línea.<br />
                    Por su seguridad no permitimos esta operaci&oacute;n.
                    <br>
                    <br>
                    Le recordamos que sólo puede estar conectado, en una sola sesión de BAV en Línea. Si no posee sesiones activas le invitamos a intentar nuevamente en un lapso de 5 minutos.
                    <br>
                    <br>
                    <b>En caso de que no haya sido usted quien está realizando el otro ingreso, por favor llame de
                        inmediato al Centro de Atención de Banco Agrícola de Venezuela al número telefónico 0800-228.00.00 o al 0500-228.00.01</b>
                    <br>
                    <br>
                    <div class="col-md-6 center-block" style="  width: 100% !important;float: left !important;">
                        <center>
                            <asp:Button ID="btnLogin" CssClass="btn btn-danger" runat="server" OnClick="btnLogin_Click" Text=" Ir a BAV en Línea"></asp:Button>
                        </center>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="col-md-2"></div>

</asp:Content>
