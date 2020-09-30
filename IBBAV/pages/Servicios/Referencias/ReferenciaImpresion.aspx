<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReferenciaImpresion.aspx.cs" Inherits="IBBAV.pages.Servicios.Referencias.ReferenciaImpresion" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Referencia Bancaria BAV</title>
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
<body onload="window.focus();window.print();">
    <form id="form1" runat="server">
        <div id="divToPrint">
            <table width="100%" style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr id="trDirigido" runat="server">
                    <td align="center" class="td_legal">
                        <asp:Label ID="liDirigido" runat="server" CssClass="letranegrita"></asp:Label></td>
                </tr>
                <tr id="trDirigido1" runat="server">
                    <td class="td_legal">
                        <table width="100%" style="width: 100%">
                            <tr>
                                <td class="td_legal">Señores:
                                </td>
                            </tr>
                            <tr>
                                <td class="td_legal">
                                    <asp:Label ID="liDirigido1" runat="server" CssClass="letranegrita"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="td_legal">Presente,</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td_legal">
                        <br />
                        <br />
                        <br />
                        <br />
                        Hacemos constar que: el(la) Sr.(a): <asp:Label ID="liTitular" runat="server" CssClass="letranegrita"></asp:Label> Cédula de identidad N°
                        <asp:Label ID="liCedula" runat="server" CssClass="letranegrita"></asp:Label> posee una Cuenta Corriente en esta institucion distinguida con el N° <asp:Label ID="liNroCuenta" runat="server" CssClass="letranegrita"></asp:Label>
                        desde el <asp:Label ID="liFechaInicio" runat="server" CssClass="letranegrita"></asp:Label> con un saldo promedio trimestral de <asp:Label ID="liBase" runat="server" CssClass="letranegrita"></asp:Label>
                        de <asp:Label ID="liLiteral" runat="server" CssClass="letranegrita"></asp:Label> manejadas a nuestra entera satisfacción.
                        <br />
                        <br />
                        <br />
                        <br />
                        Constancia que se expide a petición de la parte interesada en Caracas, a los <asp:Label ID="liDia" runat="server" CssClass="letranegrita"></asp:Label> días del mes de <asp:Label ID="liMes" runat="server" CssClass="letranegrita"></asp:Label> de <asp:Label ID="liAnio" runat="server" CssClass="letranegrita"></asp:Label>.
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="td_legal">Atentamente,</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgFirma" ImageUrl="~/images/firma.gif" runat="server" Height="104" Width="160" />
                        <br />
                        Banco Agricola de Venezuela, C.A.
			            <br />
                        Banco Universal
			            <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="HEIGHT: 15px">Caduca a los 30 días siguientes a la fecha de emisión.
			            <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="td_legal">Esta Referencia Bancaria ha sido emitida en forma electrónica a través del servicio de BAV en Línea bajo el N°
                        <asp:Label ID="liReferencia" runat="server" CssClass="letranegrita"></asp:Label>. En caso de requerir certificación de la misma, puede contactarnos por el Centro de Atención Telefónica 0500-000.0000 a través de la opción No. X Soporte Internet Banking.</td>
                </tr>
            </table>
        </div>
        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
        <div class="divFooter" >
            <hr />
            <b><%= DateTime.Now.Year %>:Pueblo Victorioso</b>
            <br />
            Sede Principal: Avenida Francisco de Miranda, Torre CAVENDES, piso 14, Los Palos Grandes, Caracas.
            <br />
            R.I.F. G-20005795-5
        </div>
    </form>
</body>
</html>
