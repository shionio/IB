<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReciboExtraEfect.aspx.cs" Inherits="IBBAV.pages.Extraefectivo.pdf.ReciboExtraEfect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<title>Recibo Bancario BAV</title>
<head>
    <style type="text/css">
        html,body, form {
            margin: 0;
            padding:0;
            height:100%;
        }

        .letrapequena {
            font-size: 7pt;
        }

        .letranegrita {
            font-weight: bolder;
        }

        p, td, body {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333333;
            margin: 0;
            padding: 0;
        }

        .td_legal {
            font-size: 10pt;
            padding: 2px;
            text-align: justify;
        }

        div.divFooter {
            position: fixed; bottom: 0; text-align:center;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divToPrint">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br /><br />
            <table class="table table-responsive" width="100%" style="width: 100%">
            <tr>
                <td align="center">
                    <div id="encabezadoRecibo" class="container-fluid">
                        <div class="row">
                            
                        </div>
                        <div class="row">
                            <h4>
                                <asp:Label ID="lblTitlePages" runat="server"></asp:Label>
                            </h4>
                            
                            <br />
                            <table class="table table-responsive" width="100%" style="width: 100%">
                                <tr>
                                    <td align="center">
                                        <asp:Panel ID="PanelReferencia" runat="server">
                                             <table width="100%" style="width: 100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                        
                                                    </td>
                                                    <td align="left" style="text-align: right;font-size:12px">
                                                        <asp:Label ID="lblNombreUsuarioRecibo" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblFechaRecibo" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <!--<td colspan="2">&nbsp;</td>-->
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <br />
                                            <table width="100%" style="width: 100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                        
                                                    </td>
                                                    <td align="left" style="text-align: right">
                                                        <asp:Literal ID="liTextoReferencia" runat="server" Text="Número:"></asp:Literal>
                                                        <b>
                                                            <asp:Literal ID="liReferencia" runat="server"></asp:Literal></b></td>
                                                </tr>
                                                <tr>
                                                    <!--<td colspan="2">&nbsp;</td>-->
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <br />
                                        <table ID="dgDatosRecibo" runat="server" Width="100%"  CellPadding="4" CellSpacing="4" GridLines="None">
                                            
                                            <tr>
                                                <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liDebito" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValordebito" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liCredito" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValorcredito" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liConcepto" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValorConcepto" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liMonto" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValormonto" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liTotalcuotas" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValorcuotas" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liMontocuota" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValormontocuota" runat="server"></asp:Literal></b></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table class="tablatransaccion" width="100%" style="width: 100%" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td align="center">
                                                    <b>
                                                        <asp:Literal ID="liNota" runat="server" Visible="False"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Literal ID="liNota3" runat="server" Visible="False"></asp:Literal></td>
                                            </tr>
                                        </table>
                                        <br />                                       
                                        <br />
                                        <asp:Literal ID="liiFrame" runat="server" Visible="false"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table class="tablatransaccion" width="100%" style="width: 100%">
                                            <tr>
                                                <td class="td_ayudas" align="center">
                                                    <asp:Literal ID="liNota2" runat="server" Visible="False"></asp:Literal></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblResultado" runat="server" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        
        <div class="divFooter" style="margin-top:1500px">
            <hr />
            <b><%= DateTime.Now.Year %> Banco Agrícola de Venezuela, C.A.</b>
            <br />
             Banco Universal
            <br />
            R.I.F. G-20005795-5
        </div>
        </div>
    </form>
</body>
</html>
