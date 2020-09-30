<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="IBBAV.pages.Confirmacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=ResolveUrl("~/js/jquery-print.js") %>"></script>
    <script type="text/javascript">

        function validar() {
            var divbut = $(".buttonsAction");
            
            divbut.attr("disabled",true);
            var i = $(".ct");
            i.parent().removeClass("has-error");
            if (i.val() == "") {
                i.parent().addClass("has-error").focus();
                showAlert("Alerta", "La Clave de Transacciones es requerida");
                divbut.removeAttr("disabled");
                return false;
            }
            <%= ClientScript.GetPostBackEventReference(btnConfirmar,null) %>;
            
        }

        $(document).on("ready", function (e) {
            $("#btnImprimir").on("click", function () {
                $("#<%= panelRecibo.ClientID %>").print({
                    // Use Global styles
                    globalStyles: true,
                    // Add link with attrbute media=print
                    mediaPrint: false,
                    //Custom stylesheet
                    stylesheet: null,
                    //Print in a hidden iframe
                    iframe: true,
                    // Don't print this
                    noPrintSelector: ".avoid-this",
                    // Add this on top
                    append: "",
                    // Add this at bottom
                    prepend: "BAV - Banco Agrícola de Venezuela",
                    // Manually add form values
                    manuallyCopyFormValues: true,
                    // resolves after print and restructure the code for better maintainability
                    deferred: $.Deferred()
                });
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="panelMSG" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>
                    <asp:Literal ID="liTitulo1" runat="server"></asp:Literal>
                    Clave de Transacciones Temporal</span>
            </div>
            <div class="panel-body">
                <table class="table table-responsive">
                    <tr>
                        <td style="text-align: center;">
                            <img src="<%=ResolveClientUrl("~/images/message_not.png")%>" class="img-responsive" />
                        </td>
                        <td style="text-align: center;">
                            <p style="font-size: 15px;">Consulte en su <b>celular o en su correo electrónico</b> el mensaje de Banco Agrícola de Venezuela con la <b>Clave de Operaciones Especiales</b> que necesita para completar esta operación.</p>
                            <p style="font-size: 15px;">Recuerde que esa clave será válida para realizar varias transacciones en la misma conexión de BAV en Línea y vencerá sólo cuando se desconecte o cierre la sesión.</p>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table class="table table-responsive">
                                <tr>
                                    <td align="center" colspan="2" style="height: 24px">
                                        <asp:Button ID="btnContinuarSMS" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnContinuarSMS_Click" />
                                        <asp:Button ID="btnRegresarSMS" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="btnRegresar_Click" />&nbsp;
                                        <asp:Button ID="btnCancelarSMS" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelConfirmacion" runat="server" Visible="false">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>
                    <asp:Literal ID="liTitulo" runat="server"></asp:Literal></span>
            </div>
            <div class="panel-body">
                <table class="table table-responsive">
                    <tr>
                        <td>
                            <table class="table table-responsive table-bav">
                                <asp:Repeater ID="rptDatos" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="fieldTxt2"><%# Eval("TituloConfirmacion") %></td>
                                            <td class="fieldVal2"></td>
                                            <td class="fieldInf2"><%# Eval("Valor") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr id="trClaveTransacciones" runat="server">
                                    <td class="fieldTxt2">Clave de Transacciones:</td>
                                    <td class="fieldVal2"></td>
                                    <td class="fieldInf2">
                                        <asp:HiddenField ID="textP2" runat="server" EnableViewState="False"></asp:HiddenField>
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" EnableViewState="false" autocompletion="off" CssClass="form-control ct"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table class="tablatransaccion" width="100%" style="width: 100%" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td align="center" colspan="2" style="height: 24px">
                                        <asp:Button ID="btnConfirmar" runat="server" Text="Aceptar" OnClientClick="return validar();" OnClick="btnConfirmar_Click" CssClass="btn btn-danger buttonsAction" />
                                        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-danger buttonsAction" />&nbsp;
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-danger buttonsAction" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelRecibo" runat="server" Visible="false" Width="95%" EnableViewState="false">

        <table class="table table-responsive">
            <tr>
                <td align="center">
                    <div id="encabezadoRecibo" class="container-fluid">
                        <div class="row">
                            <div class="col-md-6 col-xs-6 pull-left">
                                <asp:Image ID="imagengrid" runat="server" ImageUrl="~/images/logobav2.png" Height="69px" />
                            </div>
                            <div class="col-md-6 col-xs-6 pull-right">
                                <asp:Label ID="lblNombreUsuarioRecibo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblFechaRecibo" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <h4>
                                <asp:Label ID="lblTitlePages" runat="server"></asp:Label>
                            </h4>
                            <hr />


                            <table class="table table-responsive">
                                <tr>
                                    <td align="center">
                                        <asp:Panel ID="PanelReferencia" runat="server">
                                            <table width="100%" style="width: 100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Literal ID="liTextoFecha" runat="server" Text="Fecha:"></asp:Literal>
                                                        <b>
                                                            <asp:Literal ID="liFecha" runat="server"></asp:Literal></b></td>
                                                    <td align="left" style="text-align: right">
                                                        <asp:Literal ID="liTextoReferencia" runat="server" Text="Número:"></asp:Literal>
                                                        <b>
                                                            <asp:Literal ID="liReferencia" runat="server"></asp:Literal></b></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <br />
                                        <asp:DataGrid ID="dgDatosRecibo" runat="server" Width="100%" AutoGenerateColumns="False"
                                            ShowHeader="False" CellPadding="4" CellSpacing="4" GridLines="None">
                                            <Columns>
                                                <asp:BoundColumn DataField="TituloRecibo">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Valor">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
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
                                        <asp:Panel ID="panelCheques" runat="server" Visible="false" Width="100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:DataGrid ID="dgCheques" runat="server" Width="100%">
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Cheque" HeaderText="N° Cheque"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Estatus" HeaderText="Estatus"></asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
    </asp:Panel>
    <asp:Panel ID="panelBotonImpresion" runat="server" Width="95%">
        <table class="table table-responsive">
            <tr>
                <td align="center" colspan="2" style="height: 24px">
                    <button id="btnImprimir" type="button" class="btn btn-danger"><span class="glyphicon glyphicon-print" aria-hidden="true"></span> Imprimir</button>
                    <asp:Button ID="btnImprimirRecibo" runat="server" Text="Reimprimir Referencia" Width="200px" Visible="False" CssClass="btn btn-danger" OnClick="btnImprimirRecibo_Click" />&nbsp;
                    <asp:Button ID="btnRegresar2" runat="server" OnClick="btnRegresar2_Click" Text="Regresar" CssClass="btn btn-danger" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

